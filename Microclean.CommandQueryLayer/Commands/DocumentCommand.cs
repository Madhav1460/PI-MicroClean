using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Commands
{
    public class DocumentCommand
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Remark { get; set; }
        public short DocumentType { get; set; }
        public IFormFile DocFile { get; set; }
    }
}
