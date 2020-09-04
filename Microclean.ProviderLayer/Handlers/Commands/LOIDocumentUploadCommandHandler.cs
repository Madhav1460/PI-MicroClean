using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataModel.Models;
using Microclean.ProviderLayer.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class LOIDocumentUploadCommandHandler : BaseRequest, IRequestHandler<LOIDocumentUploadCommand, IActionResult>
    {
        private readonly IAccountRepository _account;
        private string _dir;
        public LOIDocumentUploadCommandHandler(IAccountRepository account, IWebHostEnvironment env)
        {
            _account = account;
            _dir = env.WebRootPath;
        }

        public async Task<IActionResult> Handle(LOIDocumentUploadCommand request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<UplodLoiDocFileResponse>();
            if (request.LoiDocFile != null)
            {
                string strDirectory = "\\LOIDocumnetUpload" + "\\" + Regex.Replace(request.FranchiseeId.ToString(), @"\s+", "");

                Tbluserdoument documentdata = new Tbluserdoument();
                var fileResult = WriteFile(request.LoiDocFile, strDirectory, "").Result;

                if (request.DocumentId > 0)
                    documentdata.Id = request.DocumentId;

                if (fileResult.HasSuccess)
                    documentdata.DocImagePath = fileResult.UserObject.ImagePath;

                documentdata.UserId = request.FranchiseeId;
                documentdata.InsertedBy = request.CurrentUserId;
                documentdata.DocumentTypeId = 6;
                documentdata.UserTypeId = request.CurrentUserTypeId;
                documentdata.Remark = request.LoiDocFile.FileName;
                documentdata.InsertDate = DateTime.Now;
                documentdata.LastUpdateDate = DateTime.Now;
                documentdata.Status = 1;
                try
                {
                    var result = await _account.FranchiseeLoiUpload(documentdata);
                    if (result.HasSuccess)
                    {
                        _response.Status = true;
                        _response.Message = result.ResultCode.MessageText;
                        _response.Data = result.UserObject;
                        return _response.ToHttpResponse();
                    }
                    else
                    {
                        _response.Status = false;
                        _response.Message = result.ResultCode.MessageText;
                        return _response.ToHttpResponse();
                    }
                }
                catch (Exception ex)
                {
                    ErrorTrace.Logger(LogArea.ProviderLayer, ex);
                    _response.Status = false;
                    _response.Message = "Exception";
                }
                return _response.ToHttpResponse();
            }
            _response.Status = false;
            _response.Message = "Please upload file";
            return _response.ToHttpResponse();
        }
        public async Task<ApiResult<DocPathInoViewModel>> WriteFile(IFormFile file, string rootdir = null, string subdir = null)
        {
            DocPathInoViewModel imInfo = new DocPathInoViewModel();
            string fileName = string.Empty;
            string filePath = rootdir;
            string root = string.Empty;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString("N").ToUpper() + extension; //Create a new Name for the file due to security reasons.

                root = Path.Combine(_dir, "Upload\\" + rootdir + "");
                filePath = "Upload\\" + rootdir + "\\" + fileName + "";
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);

                if (!string.IsNullOrEmpty(subdir))
                {
                    filePath = "Upload\\" + rootdir + "\\" + subdir + "\\" + fileName + "";
                    root = Path.Combine(_dir + "Upload" + rootdir + subdir + "");
                    if (!Directory.Exists(root))
                        Directory.CreateDirectory(root);
                }
                var path = Path.Combine(root, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    string mimeType = file.ContentType;
                    await file.CopyToAsync(fileStream);
                }
                imInfo.ImageName = fileName;
                imInfo.ImagePath = filePath;
                return new ApiResult<DocPathInoViewModel>((new ApiResultCode(ApiResultType.Success, 2)), imInfo);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.DataAccessLayer, ex);
                return new ApiResult<DocPathInoViewModel>((new ApiResultCode(ApiResultType.Error, 2, "Exception during saveing")));
            }
        }
    }
}
