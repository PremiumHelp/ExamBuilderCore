using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamBuilder.Business;
using ExamBuilder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamBuilder.Presentation.Controllers
{
    public class ExamController : Controller
    {
        private readonly IWiredBusiness _wiredBusiness;
        private readonly IExamBusiness _examBusiness;
        public ExamController(IWiredBusiness wiredBusiness, IExamBusiness examBusiness)
        {
            _examBusiness = examBusiness;
            _wiredBusiness = wiredBusiness;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("User") == null)
                return RedirectToAction("Login", "Account");

            var userId = HttpContext.Session.GetInt32("User");
            ViewBag.Exams = _examBusiness.GetAll().Where(e => e.UserId == userId && e.IsActive);
            return View();
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("User") == null)
                return RedirectToAction("Login", "Account");

            ViewBag.UserId = HttpContext.Session.GetInt32("User");
            ViewBag.Wireds = _wiredBusiness.GetContents();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Exam exam)
        {
            if (HttpContext.Session.GetInt32("User") == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                return View();
            }
            var processResult = _examBusiness.Add(exam);
            if (!processResult.IsSuccess)
            {
                ViewBag.Message = "Kayıt işlemi sırasında bir hata meydana geldi";
                return View();
            }
            return RedirectToAction("Take");
        }

        public IActionResult Take()
        {
            if (HttpContext.Session.GetInt32("User") == null)
                return RedirectToAction("Login", "Account");

            var exam = _examBusiness
                .GetAll()
                .OrderByDescending(e => e.Id)
                .FirstOrDefault();

            return View(exam);
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetInt32("User") == null)
                return RedirectToAction("Login", "Account");

            var processResult = _examBusiness.Delete(id);
            if (!processResult.IsSuccess)
                ViewBag.Message = "Silme işlemi sırasında bir hata meydana geldi.";

            ViewBag.Message = "Silme işlemi başarılı";

            return RedirectToAction("Index");
        }
    }
}