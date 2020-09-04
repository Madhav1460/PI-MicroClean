using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microclean.DataModel.DataModel
{
    public class EmployeeAsignViewDataModel
    {
        [Key]
        public long? UserId { get; set; }
        public long? TestId { get; set; }
        public string FullName { get; set; }
    }
}
