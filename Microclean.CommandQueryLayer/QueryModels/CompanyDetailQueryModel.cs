using System.Collections;
using System.Collections.Generic;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class CompanyDetailQueryModel
    {
        public CompanyDetailQueryModel()
        {
            User = new UserQueryModel();
            PaymentDetails = new List<PaymentDetailQueryModel>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactNo { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyZip { get; set; }
        public string ComapnyPAN { get; set; }
        public string CompanyGSTNo { get; set; }
        public string CompanyEmail { get; set; }
        public int StateId { get; set; }
        public long DistrictId { get; set; }
        public long CityLocationId { get; set; }
        public UserQueryModel User { get; set; }
        public List<PaymentDetailQueryModel> PaymentDetails { get; set; }
    }
}
