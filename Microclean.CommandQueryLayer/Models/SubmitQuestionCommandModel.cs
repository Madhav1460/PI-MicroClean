using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class SubmitQuestionCommandModel
    {
        public SubmitQuestionCommandModel()
        {
            OptionsCommand = new List<SubmitOptionCommandModel>();
        }
        public long QuestionId { get; set; }
        public List<SubmitOptionCommandModel> OptionsCommand { get; set; }
    }
}
