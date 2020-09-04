using AutoMapper;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class FranchiseeItSelfRegistrationCommandHandler : IRequestHandler<FranchiseeItSelfRegistrationCommand, IActionResult>
    {
        private readonly IAccountRepository _account;
        private readonly IMapper _mapper;

        public FranchiseeItSelfRegistrationCommandHandler(IAccountRepository account, IMapper mapper)
        {
            _account = account;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(FranchiseeItSelfRegistrationCommand request, CancellationToken cancellationToken)
        {
            Tblclient clientobj = new Tblclient();
            var _response = new Response();
            try
            {
                clientobj.Name = request.CompanyName;
                clientobj.InsertedDate = DateTime.Now;
                clientobj.Status = 1;
                clientobj.UpdatedDate = DateTime.Now;
                //clientobj.CityLocationid = request.CityId;
                clientobj.ZipCode = request.Pincode;
                var data = _mapper.Map<Tbluser>(request);
                data.IsGuestUser = 0;
                data.PasswordHash = Utilities.Utility.EncryptionLibrary.EncryptText(data.PasswordHash);
                data.Status = 1;
                data.InsertDate = DateTime.Now;
                data.LastUpdateDate = DateTime.Now;
                if (_account.IsEmailExist(request.Email).Result.UserObject)
                {
                    _response.Status = false;
                    _response.Message = "Email already in use.";
                    _response.ErrorTypeCode = (int)ErrorMessage.Email;
                    return _response.ToHttpResponse();
                }
                if (_account.IsPhoneExist(request.PhoneNumber).Result.UserObject)
                {
                    _response.Status = false;
                    _response.Message = "Phone no. already in use.";
                    _response.ErrorTypeCode = (int)ErrorMessage.Phone;
                    return _response.ToHttpResponse();
                }
                data.UserTypeId = request.UserTypeId;
                data.Tbluserdetail.Add(new Tbluserdetail
                {
                    FullName = string.Format("{0} {1} {2}", data.FirstName, data.MiddleName, data.LastName),
                    Status = 1,
                    InsertDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    InsertedBy = data.Id,
                    LastUpdatedBy = data.Id
                });
                data.Tbluserrolemapping.Add(new Tbluserrolemapping
                {
                    RoleId = 2
                });
                if (request.Address != null)
                {
                    if (request.Address.CityLocationId > 0)
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
                                //InsertedBy = request.CurrentUserId,
                                InsertDate = DateTime.Now
                            },
                            UserId = data.Id
                        });
                    }
                }
                clientobj.Tbluser.Add(data);
                var result = await _account.UserRegisterAsync(clientobj);
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
                return _response.ToHttpResponse();
            }
        }
    }
}
