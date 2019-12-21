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
    public class AccountController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private IHttpContextAccessor HttpContextAccessor;
        public AccountController(IUserBusiness userBusiness, IHttpContextAccessor httpContextAccessor)
        {
            _userBusiness = userBusiness;
            HttpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("Index", "Exam");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (!ModelState.IsValid)
                return View(user);
            var matchedUser = _userBusiness.GetBy(user.UserName, user.Password);
            if (matchedUser != null && matchedUser.IsActive)
            {
                HttpContextAccessor.HttpContext.Session.SetInt32("User", matchedUser.Id);
                return RedirectToAction("Index", "Exam");
            }
            ViewBag.Message = "Kullanıcı Bulunamadı.";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
                return View("Login", user);

            var processResult = _userBusiness.Add(user);
            if (!processResult.IsSuccess)
            {
                ViewBag.Message = "Kayıt işlemi başarısız.";
                return View("Login", user);
            }
            ViewBag.Message = "Kayıt işlemi başarılı! Lütfen Giriş yapınız";
            return RedirectToAction("Login");
        }
    }
}