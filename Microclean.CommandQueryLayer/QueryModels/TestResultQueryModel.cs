using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class TestResultQueryModel
    {
        public TestResultQueryModel()
        {
            Questions = new List<TestQuestionQueryModel>();
        }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string TestName { get; set; }
        public long TestId { get; set; }
        public int TotalQuestion { get; set; }
        public int TotalAtemptQuestion { get; set; }
        public int TotalCorrenctAnswer { get; set; }
        public int TotalWorngAnswer { get; set; }
        public List<TestQuestionQueryModel> Questions { get; set; }
    }
}
