using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface IUserTranninRepository
    {
        Task<ApiResultCode> TraniningDocUpload(Tbltranningdocument datamodel);
        Task<ApiResult<IEnumerable<TraingDocumnentsResponseModel>>> GetAllTrainingDocuments();
        Task<ApiResultCode> DeleteTraningDocumenentById(DeleteTraningDocumentById request);
    }
}
