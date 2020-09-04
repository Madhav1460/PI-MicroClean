using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.WebClient.Controllers
{
    public class FranchiseeUserController : Controller
    {
        public IActionResult Index()
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
        [Route("test")]
        [HttpGet]
        public IActionResult GetTest()
        {
            return View();
        }
        [Route("quiz")]
        [HttpGet]
        public IActionResult GetQuestionOptionForTestPartial(long testid)
        {
            if (testid > 0)
            {
                ViewBag.testid = testid;
            }
            return View();
        }
        [Route("result")]
        [HttpGet]
        public IActionResult GetResult(long testid)
        {
            if (testid > 0)
            {
                ViewBag.testid = testid;
            }
            return View();
        }
    }
}