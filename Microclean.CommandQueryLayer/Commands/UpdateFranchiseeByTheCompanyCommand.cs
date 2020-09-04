using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.QueryModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Commands
{
    public class UpdateFranchiseeByTheCompanyCommand : BaseRequest, IRequest<IActionResult>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactNo { get; set; }
        public string CompanyAddress { get; set; }
        public int CompanyZip { get; set; }
        public string ComapnyPAN { get; set; }
        public string CompanyGSTNo { get; set; }
        public string CompanyEmail { get; set; }
        public int StateId { get; set; }
        public long CityId { get; set; }
        public UserQueryModel User { get; set; }
    }
}
