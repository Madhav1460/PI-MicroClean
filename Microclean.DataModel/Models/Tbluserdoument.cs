using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbluserdoument
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public long? UserId { get; set; }
        public short? DocumentTypeId { get; set; }
        public string DocImagePath { get; set; }
        public string Remark { get; set; }
        public short? UserTypeId { get; set; }
        public byte? IsApproved { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblclient Client { get; set; }
        public virtual Tbldocumenttype DocumentType { get; set; }
        public virtual Tbluser User { get; set; }
    }
}
