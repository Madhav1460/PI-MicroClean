using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class TestQuestionQueryModel
    {
        public TestQuestionQueryModel()
        {
            Options = new List<TestQuestionOptionsQueryModel>();
        }
        public long QuestionId { get; set; }
        public string Question { get; set; }
        public short TakeActionDuration { get; set; }
        public List<TestQuestionOptionsQueryModel> Options { get; set; }
    }
}
