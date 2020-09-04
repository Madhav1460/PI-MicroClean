using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Queries;
using Microclean.ProviderLayer.Handlers.Queries;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CommonController(IMediator mediator, IWebHostEnvironment webHostEnvironment, IAccountRepository account)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpGet("download")]
        public IActionResult DownloadFile([FromQuery] string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return Ok("file not found.");
            }
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = "";
            path = Path.Combine(webRootPath, filePath);

            var memory = new MemoryStream();
            memory.Position = 0;
            if (System.IO.File.Exists(path))
            {
                byte[] fileBytes = GetFile(path);
                string nam = Path.GetFileName(path);
                return File(fileBytes, "application/octet-stream", nam);
            }
            return Ok();
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

        [HttpGet("getallstate")]
        public async Task<IActionResult> GetAllState()
        {
            var result = await _mediator.Send(new GetAllStateByCountryId(101));
            return result;
        }

        [HttpGet("getpaymnettype")]
        public async Task<IActionResult> GetPaymnetType()
        {
            var result = await _mediator.Send(new GetPaymentTypeQuery());
            return result;
        }
        [HttpGet("getalldistrictbyStateId")]
        public async Task<IActionResult> GetAllDistrictByStateId(int stateid)
        {
            var result = await _mediator.Send(new GetAllDistrictByStateIdQuery(stateid));
            return result;
        }
        [HttpGet("getallcitylocationbydistrictid")]
        public async Task<IActionResult> GetallCitylocationByDistrictId(long districtid)
        {
            var result = await _mediator.Send(new GetAllCityLocationByDistrictIdQuery(districtid));
            return result;
        }
        [HttpGet("getpincodebycitylocationid")]
        public async Task<IActionResult> GetPincodeBylocationId(long districtid)
        {
            var result = await _mediator.Send(new GetAllCityLocationByDistrictIdQuery(districtid));
            return result;
        }
        [HttpGet("getalltestcategory")]
        public async Task<IActionResult> GetAllTestCategory()
        {
            var result = await _mediator.Send(new GetAllCategoryQuery());
            return result;
        }
        [HttpGet("existingemailcheck")]
        public async Task<IActionResult> ExistingEmailCheck(string email)
        {
            var result = await _mediator.Send(new ExistingEmailCheckQuery(email));
            return result;
        }
        [HttpGet("existingphonecheck")]
        public async Task<IActionResult> ExistingPhoneCheck(string phone)
        {
            var result = await _mediator.Send(new ExistingPhoneCheckQuery(phone));
            return result;
        }
    }
}
