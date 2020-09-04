using AutoMapper;
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
    public class FranchiseeItSelfProfileUpdateCommandHandler : BaseRequest, IRequestHandler<FranchiseeItSelfProfileUpdateCommand, IActionResult>
    {
        private string _dir;
        private readonly IFranchiseeRepository _franchisee;
        private readonly IAccountRepository _account;
        private readonly IMapper _mapper;

        public FranchiseeItSelfProfileUpdateCommandHandler(IWebHostEnvironment env, IFranchiseeRepository franchisee, IAccountRepository account, IMapper mapper)
        {
            _dir = env.WebRootPath;
            _franchisee = franchisee;
            _account = account;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(FranchiseeItSelfProfileUpdateCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            Tblclient clientobj = new Tblclient();
            Tbluser data = null;
            List<Tbluserdoument> docdto = new List<Tbluserdoument>();
            try
            {
                string strDirectory = string.Empty;
                if (!string.IsNullOrEmpty(request.FirstName) && (request.CurrentUserId.HasValue && request.CurrentUserId > 0))
                {
                    strDirectory = "\\" + Regex.Replace(request.CurrentUserId.ToString(), @"\s+", "") + "\\" + Regex.Replace(request.FirstName.ToString(), @"\s+", "");
                }
                clientobj.Id = request.CompanyId > 0 ? request.CompanyId : request.CurrentCientId;
                clientobj.Name = request.CompayName;
                clientobj.InsertedDate = DateTime.Now;
                clientobj.Status = 1;
                clientobj.UpdatedDate = DateTime.Now;
                clientobj.FullAddress = request.CompanyAddress;
                clientobj.CityLocationid = request.CompanyAddressCity;
                clientobj.ZipCode = request.CompanyPincode;
                clientobj.CompanyPanCardNo = request.CompanyPANCardNo;
                clientobj.CompanyGstNo = request.CompanyGSTNo;
                clientobj.Email = request.CompayEmail;
                clientobj.PhoneNo = request.CompayPhone;

                if (request.CurrentUserId.HasValue)
                {
                    data = new Tbluser();
                    data.FirstName = request.FirstName;
                    data.MiddleName = request.MiddleName;
                    data.LastName = request.LastName;
                    //data.Email = request.Email;
                    data.PhoneNumber = request.PhoneNumber;
                    data.Id = request.UserId > 0 ? request.UserId : request.CurrentUserId.Value;
                    data.LastUpdateDate = DateTime.Now;
                    data.Tbluserdetail.Add(new Tbluserdetail
                    {
                        Id = request.UserDetail.Id,
                        AlternateNumber = request.UserDetail.AlternateNumber,
                        AlternateEmail = request.UserDetail.AlternateEmail,
                        OwnersAadharCardNo = request.UserDetail.OwnersAadharCardNo,
                        OwnerPancardNo = request.UserDetail.OwnerPANCardNo,
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
                                FileInfo fi = new FileInfo(item.DocFile.FileName);
                                if (item.Id > 0)
                                {
                                    documentdata.Id = item.Id;
                                    documentdata.LastUpdateDate = DateTime.Now;
                                    documentdata.Remark = fi.Name;
                                    documentdata.LastUpdatedBy = request.CurrentUserId;
                                    documentdata.DocumentTypeId = item.DocumentType;
                                    documentdata.UserTypeId = request.CurrentUserTypeId;
                                }
                                else
                                {
                                    documentdata.UserId = request.CurrentUserId;
                                    documentdata.UserTypeId = request.CurrentUserTypeId;
                                    documentdata.Remark = fi.Name;
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
                    List<Tblfeedetail> tblfeedetails = new List<Tblfeedetail>();
                    Tblfeedetail tblfeedetail = null;
                    if (request.FranchiseeFeeCommands.Count > 0)
                    {
                        foreach (var item in request.FranchiseeFeeCommands)
                        {
                            tblfeedetail = new Tblfeedetail
                            {
                                Id = item.FeeId,
                                ClientId = request.CompanyId,
                                UserId = request.UserId,
                                FeeValue = Convert.ToDecimal(item.FeeValue),
                                FeeTypeId = item.FeeTypeId,
                                TotalFee = item.TotalFee,
                                PaymentTerms = item.PaymentTerms,
                                UpdatedBy = request.CurrentUserId,
                                PaymentDueDate = !string.IsNullOrEmpty(item.PaymentDueDate) ? DateTime.ParseExact(item.PaymentDueDate, "dd/MM/yyyy", null) : default
                            };
                            tblfeedetails.Add(tblfeedetail);
                        }
                    }
                    clientobj.Tbluser.Add(data);
                    clientobj.Tbluserdoument = docdto;
                    clientobj.Tblfeedetail = tblfeedetails;
                    var result = await _franchisee.FranchiseeUpdateItSelfAsync(clientobj);

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
