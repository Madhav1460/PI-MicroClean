using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.WebClient.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserRegistration()
        {
            return View();
        }
        public IActionResult UserUpdate()
        {
            return View();
        }
       
        public IActionResult EditUser()
        {
            return View();
        }
        public IActionResult FranchiseeUserList()
        {
            return View();
        }
        public IActionResult AddFranchiseeUser()
        {
            return View();
        }
        public IActionResult EditFranchiseeUser()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewFranchiseeUser()
        {
            return View();
        }
        public IActionResult GetUserDetail()
        {
            return View();
        }       
        public IActionResult DocumentView()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(int i)
        {
            return View();
        }
        [HttpGet]
        public IActionResult TestConfigure()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TestConfigure(int i)
        {
            return View();
        }

        [HttpGet]
        public IActionResult TestDetail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TestDetailPartial()
        {
            return View();
        }
    }
}
