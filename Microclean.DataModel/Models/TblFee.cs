using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.DataModel.Models
{
    public partial class TblFee
    {
        public long Id { get; set; }
        public long? FranchiseId { get; set; }
        public decimal FranchiseFee { get; set; }
        public decimal OtherFee { get; set; }
        public decimal ManpowerFee { get; set; }
        public byte? Status { get; set; }
        public DateTime InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }
    }
}
