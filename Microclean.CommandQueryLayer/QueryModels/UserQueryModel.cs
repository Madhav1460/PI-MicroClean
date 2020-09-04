using Microclean.CommandQueryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class UserQueryModel
    {
        public UserQueryModel()
        {
            UserDetail = new UserDetailQueryModel();
            UserDocument = new List<DocumentQueryModel>();
            FeeDetails = new List<FeeDetailResponseModel>();
            Addresses = new List<UserAddressQueryModel>();
            Supervisor = new SupervisorDetail();
    }
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public decimal? TotalFee { get; set; }
        public UserDetailQueryModel UserDetail { get; set; }
        public List<UserAddressQueryModel> Addresses { get; set; }
        public List<DocumentQueryModel> UserDocument { get; set; }
        public List<FeeDetailResponseModel> FeeDetails { get; set; }
        public SupervisorDetail Supervisor { get; set; }
    }
}
