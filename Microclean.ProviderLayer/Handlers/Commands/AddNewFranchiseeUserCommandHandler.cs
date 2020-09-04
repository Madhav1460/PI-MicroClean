using AutoMapper;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class AddNewFranchiseeUserCommandHandler : BaseRequest, IRequestHandler<AddNewFranchiseeUserCommand, IActionResult>
    {
        private readonly IAccountRepository _account;
        private readonly ICompanyRepository _company;

        public AddNewFranchiseeUserCommandHandler(IAccountRepository account, ICompanyRepository company)
        {
            _account = account;
            _company = company;
        }

        public async Task<IActionResult> Handle(AddNewFranchiseeUserCommand request, CancellationToken cancellationToken)
        {
            Tblclient clientobj = new Tblclient();
            var _response = new SingleResponse<CompanyDetailQueryModel>();
            try
            {
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
                clientobj.Name = request.CompanyName;
                clientobj.InsertedDate = DateTime.Now;
                clientobj.Status = 1;
                clientobj.UpdatedDate = DateTime.Now;
                clientobj.CityLocationid = request.CityLocationId <= 0 ? request.Address.CityLocationId : request.CityLocationId;
                clientobj.Email = request.CompanyEmail;
                clientobj.PhoneNo = request.CompanyPhone;
                clientobj.FullAddress = request.CompanyAddress;
                clientobj.ZipCode = request.Pincode;
                var data = new Tbluser();
                data.FirstName = request.FirstName;
                data.MiddleName = request.MiddleName;
                data.LastName = request.LastName;
                data.Email = request.Email;
                data.PhoneNumber = request.PhoneNumber;
                data.UserTypeId = request.UserTypeId;
                data.PasswordHash = request.PasswordHash;
                data.IsGuestUser = 0;
                data.PasswordHash = Utilities.Utility.EncryptionLibrary.EncryptText(data.PasswordHash);
                data.Status = 1;
                data.InsertDate = DateTime.Now;
                data.LastUpdateDate = DateTime.Now;
                data.UserTypeId = request.UserTypeId;
                var tblfeedetail = new List<Tblfeedetail>();

                if (request.FranchiseeFee != null)
                {
                    tblfeedetail.Add(new Tblfeedetail
                    {
                        FeeValue = Convert.ToDecimal(request.FranchiseeFee.FranchiseeFee),
                        FeeTypeId = request.FranchiseeFee.FranchiseeFeeId,
                        TotalFee = request.FranchiseeFee.TotalAmmount,
                        PaidAmmout = Convert.ToDecimal(request.FranchiseeFee.FranchiseeFeePaidAmout),
                        PaymentTerms = request.FranchiseeFee.PaymentTerms,
                        PaymentDueDate = !string.IsNullOrEmpty(request.FranchiseeFee.PaymentDueDate) ? DateTime.ParseExact(request.FranchiseeFee.PaymentDueDate, "dd/MM/yyyy", null) : default
                    });

                    tblfeedetail.Add(new Tblfeedetail
                    {
                        FeeValue = Convert.ToDecimal(request.FranchiseeFee.OtherFee),
                        FeeTypeId = request.FranchiseeFee.OtherFeeId,
                        TotalFee = request.FranchiseeFee.TotalAmmount,
                        PaidAmmout = Convert.ToDecimal(request.FranchiseeFee.OtherFeePaidAmout),
                        PaymentTerms = request.FranchiseeFee.PaymentTerms,
                        PaymentDueDate = !string.IsNullOrEmpty(request.FranchiseeFee.PaymentDueDate) ? DateTime.ParseExact(request.FranchiseeFee.PaymentDueDate, "dd/MM/yyyy", null) : default
                    });
                    tblfeedetail.Add(new Tblfeedetail
                    {
                        FeeValue = Convert.ToDecimal(request.FranchiseeFee.FixedMonthlyFee),
                        FeeTypeId = request.FranchiseeFee.FixedMonthlyFeeId,
                        TotalFee = request.FranchiseeFee.TotalAmmount,
                        PaidAmmout = Convert.ToDecimal(request.FranchiseeFee.FixedMonthlyFeePaidAmout),
                        PaymentTerms = request.FranchiseeFee.PaymentTerms,
                        PaymentDueDate = !string.IsNullOrEmpty(request.FranchiseeFee.PaymentDueDate) ? DateTime.ParseExact(request.FranchiseeFee.PaymentDueDate, "dd/MM/yyyy", null) : default
                    });
                    tblfeedetail.Add(new Tblfeedetail
                    {
                        FeeValue = Convert.ToDecimal(request.FranchiseeFee.ThresholdAmount),
                        FeeTypeId = request.FranchiseeFee.ThresholdAmountId,
                        TotalFee = request.FranchiseeFee.TotalAmmount,
                        PaidAmmout = Convert.ToDecimal(request.FranchiseeFee.ThresholdAmountPaidAmount),
                        PaymentTerms = request.FranchiseeFee.PaymentTerms,
                        PaymentDueDate = !string.IsNullOrEmpty(request.FranchiseeFee.PaymentDueDate) ? DateTime.ParseExact(request.FranchiseeFee.PaymentDueDate, "dd/MM/yyyy", null) : default
                    });
                    data.Tblfeedetail = tblfeedetail;
                }
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
                                InsertedBy = request.CurrentUserId,
                                InsertDate = DateTime.Now
                            },
                            UserId = data.Id
                        });
                    }
                }
                data.Tbluserdetail.Add(new Tbluserdetail
                {
                    FullName = string.Format("{0} {1} {2}", data.FirstName, data.MiddleName, data.LastName),
                    AlternateEmail = request.AlternateEmail,
                    AlternateNumber = request.AlternatePhone,
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
                clientobj.Tbluser.Add(data);
                var result = await _company.AddNewFranchiseeByCompany(clientobj);
                if (result.HasSuccess)
                {
                    _response.Status = true;
                    _response.Data = result.UserObject;
                    _response.Message = result.ResultCode.MessageText;
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
                return _response.ToHttpResponse();
            }
        }
    }
}
