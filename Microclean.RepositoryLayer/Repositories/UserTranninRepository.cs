using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class UserTranninRepository : IUserTranninRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public UserTranninRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }

        public async Task<ApiResultCode> TraniningDocUpload(Tbltranningdocument datamodel)
        {

            _ = _unit.GetRepository<Tbltranningdocument>().Add(datamodel);
            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Upload Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during create");
        }

        public async Task<ApiResult<IEnumerable<TraingDocumnentsResponseModel>>> GetAllTrainingDocuments()
        {
            var result = await _unit.GetRepository<Tbltranningdocument>().GetSelectedDataAsync(p => p.Status == 1, t => new TraingDocumnentsResponseModel
            {
                Id = t.Id,
                DocTypeId = t.DocType,
                Title = t.Title,
                Description = t.Description,
                DocumnetPath = t.DocumnetPath
            });
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<TraingDocumnentsResponseModel>>(new ApiResultCode(ApiResultType.Success), result.UserObject);

            return new ApiResult<IEnumerable<TraingDocumnentsResponseModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResultCode> DeleteTraningDocumenentById(DeleteTraningDocumentById request)
        {
            var usertranningdocument = new Tbltranningdocument();
            usertranningdocument.Id = request.TraningDocumnentId;
            usertranningdocument.Status = 0;
            _unit.Context.Tbltranningdocument.Attach(usertranningdocument);
            _unit.Context.Entry(usertranningdocument).Property(t => t.Status).IsModified = true;
            var result = await _unit.SaveChangesAsync();
            if (result.ResultType == ApiResultType.Success)
            {
                return new ApiResultCode(ApiResultType.Success, messageText: "Traning Documnent Deleted!");
            }
            return new ApiResultCode(ApiResultType.Error, messageText: "Can not Delete!");
        }
    }
}
