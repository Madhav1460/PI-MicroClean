using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class TestWIthQuestionListQuery
    {
        public long Id { get; set; }
        public string Test { get; set; }
        public string Question { get; set; }
        public int? NoOfQuestion { get; set; }
        public int CreatedQuestionCount { get; set; }
    }
    public class QuestionWithListQuery
    {
        public long Id { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
        public byte? Status { get; set; }
    }
}
