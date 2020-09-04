using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class TestQuestionOptionsQueryModel
    {
        public long SubmmitedAnswerId { get; set; }
        public long OptionId { get; set; }
        public string Option { get; set; }
        public byte? IsSelect { get; set; }
        public byte? IsCorrect { get; set; }
    }
}
