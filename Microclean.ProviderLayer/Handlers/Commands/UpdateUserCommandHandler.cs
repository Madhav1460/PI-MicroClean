using AutoMapper;
using MediatR;
using Microclean.DataModel.Models;
using Microclean.DataAccessLayer.Core;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;
using Microclean.DataModel;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microclean.ProviderLayer.Models;
using System.Collections.Generic;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Org.BouncyCastle.Ocsp;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class UpdateUserCommandHandler : BaseRequest, IRequestHandler<UpdateUserCommand, IActionResult>
    {
        private readonly IAccountRepository _account;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;
        private string _dir;

        public UpdateUserCommandHandler(IAccountRepository account, IMapper mapper, IUnitOfWork<MicrocleanDbContext> unit, IWebHostEnvironment env)
        {
            _account = account;
            _mapper = mapper;
            _unit = unit;
            _dir = env.WebRootPath;
        }

        public async Task<IActionResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            Tbluser data = null;
            List<Tbluserdoument> docdto = new List<Tbluserdoument>();
            try
            {
                string strDirectory = string.Empty;
                if (!string.IsNullOrEmpty(request.FirstName) && (request.CurrentUserId.HasValue && request.CurrentUserId > 0))
                {
                    strDirectory = "\\" + Regex.Replace(request.CurrentUserId.ToString(), @"\s+", "") + "\\" + Regex.Replace(request.FirstName.ToString(), @"\s+", "");
                }
                if (request.CurrentUserId.HasValue)
                {
                    data = new Tbluser();
                    data.FirstName = request.FirstName;
                    data.MiddleName = request.MiddleName;
                    data.LastName = request.LastName;
                    data.Email = request.Email;
                    data.PhoneNumber = request.PhoneNumber;
                    data.Id = request.UserId > 0 ? request.UserId : request.CurrentUserId.Value;
                    data.LastUpdateDate = DateTime.Now;
                    data.Tbluserdetail.Add(new Tbluserdetail
                    {
                        Id = request.UserDetail.Id,
                        AlternateNumber = request.UserDetail.AlternateNumber,
                        AlternateEmail = request.UserDetail.AlternateEmail
                    });
                    Tbluserdoument documentdata = null;
                    if (request.UserDocumentCommands.Any(t => t.DocFile != null))
                    {
                        foreach (var item in request.UserDocumentCommands)
                        {
                            documentdata = new Tbluserdoument();
                            if (item.DocFile != null)
                            {
                                var fileResult = WriteFile(item.DocFile, strDirectory, "").Result;
                                if (item.Id > 0)
                                {
                                    documentdata.Id = item.Id;
                                    documentdata.LastUpdateDate = DateTime.Now;
                                    documentdata.LastUpdatedBy = request.CurrentUserId;
                                    documentdata.DocumentTypeId = item.DocumentType;
                                    documentdata.UserTypeId = CurrentUserTypeId;
                                }
                                else
                                {
                                    documentdata.UserTypeId = CurrentUserTypeId;
                                    documentdata.Remark = item.Remark;
                                    documentdata.InsertedBy = request.CurrentUserId;
                                    documentdata.InsertDate = DateTime.Now;
                                    documentdata.DocumentTypeId = item.DocumentType;
                                    documentdata.Status = 1;
                                }
                                if (fileResult.HasSuccess)
                                    documentdata.DocImagePath = fileResult.UserObject.ImagePath;

                                docdto.Add(documentdata);
                            }
                        }
                    }
                    if (request.LoiFile != null)
                    {
                        documentdata = new Tbluserdoument();
                        strDirectory = "\\" + Regex.Replace(request.CurrentUserId.ToString(), @"\s+", "") + "\\" + Regex.Replace(request.FirstName.ToString(), @"\s+", "" + "\\Loidocument");
                        var fileResult = WriteFile(request.LoiFile, strDirectory, "");
                        documentdata.UserTypeId = CurrentUserTypeId;
                        documentdata.InsertedBy = request.CurrentUserId;
                        documentdata.InsertDate = DateTime.Now;
                        documentdata.DocumentTypeId = 4;
                        documentdata.Status = 1;
                        docdto.Add(documentdata);
                    }
                    if (request.Address != null)
                    {
                        if (request.Address.AddressId > 0)
                        {
                            var address = new Tbladdress
                            {
                                Id = request.Address.AddressId,
                                FullAddress = request.Address.FullAdrrss,
                                LandMark = request.Address.LandMark,
                                ZipCode = request.Address.ZipCode,
                                CountryId = 101,
                                CityLocationId = request.Address.CityLocationId,
                                LastUpdatedBy = request.CurrentUserId,
                                LastUpdateDate = DateTime.Now
                            };
                           var updateaddressresult = await _account.UserAddressUpdateAsync(address);
                        }
                        else
                        {
                            data.Tbluseraddressmapping.Add(new Tbluseraddressmapping
                            {
                                Address = new Tbladdress
                                {
                                    Id = request.Address.AddressId,
                                    FullAddress = request.Address.FullAdrrss,
                                    LandMark = request.Address.LandMark,
                                    ZipCode = request.Address.ZipCode,
                                    CountryId = 101,
                                    CityLocationId = request.Address.CityLocationId,
                                    InsertedBy = request.CurrentUserId,
                                    InsertDate = DateTime.Now
                                },
                                UserId = request.CurrentUserId
                            });
                        }

                    }
                    data.Tbluserdoument = docdto;
                    var result = await _account.UserUpdateAsync(data);

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
                return _response.ToHttpResponse();
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.ProviderLayer, ex);
                _response.Status = false;
                _response.Message = "Exception";
                return _response.ToHttpResponse();
            }
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
