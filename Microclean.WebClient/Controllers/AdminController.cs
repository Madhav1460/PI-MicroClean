using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Microclean.WebClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult DocumentUpload()
        {
            return View();
        }
        public IActionResult CreateFranchisee()
        {
            return View();
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
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
        public IActionResult TrainingDocUpload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TrainingDocUpload(int i)
        {
            return View();
        }

        public IActionResult FranchiseeView()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CommercialDetailView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CommercialDetailView(int i)
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddFee()
        {
            return PartialView();
        }
        public IActionResult UpdateFee()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult UpdateFranchisee()
        {
            return View();

        }  

        [HttpGet]
        public IActionResult QuestionConfigure()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QuestionDetail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult QuestionDetailPartial()
        {
            return View();
        }
        [HttpGet]
        public IActionResult QuestionEdit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult QuestionEdit(int i)
        {
            return View();
        }
        public IActionResult EmployeeList()
        {
            return View();
        }
        public IActionResult AddEmployee()
        {
            return View();
        }
        public IActionResult EmployeeView()
        {
            return View();
        }
        public IActionResult EditEmployee()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TrainingDocumentDetail()
        {
            return View();
        }
    }
}