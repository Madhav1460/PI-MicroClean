using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class DocumentQueryModel
    {
        public int? DocId { get; set; }
        public int? ClientId { get; set; }
        public long? UserId { get; set; }
        public short? DocumentTypeId { get; set; }
        public string DocFilePath { get; set; }
        public string DocumentName { get; set; }
        public string Remark { get; set; }
        public string DocumentUploadDate { get; set; }
    }
}
