using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ApiRisponse.ResponseUtil
{
    public class ErrorResponse
    {
        public ErrorResponse() { }
        public ErrorResponse(ErrorModel error)
        {
            Errors.Add(error);
        }
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
