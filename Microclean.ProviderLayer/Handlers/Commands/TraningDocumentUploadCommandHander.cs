using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.DataModel.Models;
using Microclean.ProviderLayer.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class TraningDocumentUploadCommandHander : IRequestHandler<TraningDocumentUploadCommand, IActionResult>
    {
        private readonly IUserTranninRepository _usertraing;
        private string _dir;

        public TraningDocumentUploadCommandHander(IUserTranninRepository usertraing, IWebHostEnvironment env)
        {
            _usertraing = usertraing;
            _dir = env.WebRootPath;
        }

        public async Task<IActionResult> Handle(TraningDocumentUploadCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            if (request.DocFile != null)
            {
                string strDirectory = "\\OnLineTraingDocument" + "\\" + Regex.Replace(request.DocType.ToString(), @"\s+", "");

                Tbltranningdocument documentdata = new Tbltranningdocument();
                var fileResult = WriteFile(request.DocFile, strDirectory, "").Result;


                documentdata.DocType = request.DocType;
                documentdata.Title = request.Title;
                documentdata.Description = request.Description;
                documentdata.Status = Convert.ToByte(true);
                documentdata.DocumnetPath = fileResult.UserObject.ImagePath;
                try
                {
                    var result = await _usertraing.TraniningDocUpload(documentdata);
                    if (result.ResultType == ApiResultType.Success)
                    {
                        _response.Status = true;
                        _response.Message = result.MessageText;
                        return _response.ToHttpResponse();
                    }
                    else
                    {
                        _response.Status = false;
                        _response.Message = result.MessageText;
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
