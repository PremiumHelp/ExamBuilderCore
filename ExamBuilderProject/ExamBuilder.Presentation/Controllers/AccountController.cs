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
        public AccountController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        public IActionResult Login()
        {
            var user = new User
            {
                UserName = "erdem123",
                Password = "erdem321"
            };

            _userBusiness.Add(user);
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
                HttpContext.Session.SetString("User", user.UserName);
                return RedirectToAction("Index", "Exam");
            }
            ViewBag.Message = "Kullanıcı Bulunamadı.";
            return View();
        }
    }
}