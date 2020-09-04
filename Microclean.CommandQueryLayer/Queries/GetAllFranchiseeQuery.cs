using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetAllFranchiseeQuery : IRequest<IActionResult>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public bool IsApproved { get; set; }
        public string ApproveStatus { get; set; }
    }
}
