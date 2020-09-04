using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.WebClient.Controllers
{
    public class AdminEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}