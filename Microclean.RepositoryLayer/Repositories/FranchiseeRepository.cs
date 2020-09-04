using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class FranchiseeRepository : IFranchiseeRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public FranchiseeRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }
        public async Task<ApiResultCode> FranchiseeUpdateItSelfAsync(Tblclient datamodel)
        {
            if (datamodel.Tbluser != null)
            {
                _unit.Context.Tblclient.Attach(datamodel);
                _unit.Context.Entry(datamodel).Property(t => t.CompanyPanCardNo).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.CompanyGstNo).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.FullAddress).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.Name).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.PhoneNo).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.Email).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.CityLocationid).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.ZipCode).IsModified = true;
                _unit.Context.Entry(datamodel).Property(t => t.UpdatedDate).IsModified = true;

                foreach (var client in datamodel.Tbluser)
                {
                    _unit.Context.Tbluser.Attach(client);
                    _unit.Context.Entry(client).Property(t => t.FirstName).IsModified = true;
                    _unit.Context.Entry(client).Property(t => t.MiddleName).IsModified = true;
                    _unit.Context.Entry(client).Property(t => t.LastName).IsModified = true;
                    _unit.Context.Entry(client).Property(t => t.LastUpdateDate).IsModified = true;
                    foreach (var item in client.Tbluserdetail)
                    {
                        if (item.Id > 0)
                        {
                            _unit.Context.Tbluserdetail.Attach(item);
                            _unit.Context.Entry(item).Property(t => t.AlternateNumber).IsModified = true;
                            _unit.Context.Entry(item).Property(t => t.OwnerPancardNo).IsModified = true;
                            _unit.Context.Entry(item).Property(t => t.OwnersAadharCardNo).IsModified = true;
                            _unit.Context.Entry(item).Property(t => t.AlternateEmail).IsModified = true;
                            _unit.Context.Entry(item).Property(t => t.LastUpdateDate).IsModified = true;
                        }
                    }
                }
            }
            foreach (var item in datamodel.Tbluserdoument)
            {
                if (item.Id > 0)
                {
                    _unit.Context.Tbluserdoument.Attach(item);
                    _unit.Context.Entry(item).Property(t => t.DocImagePath).IsModified = true;
                    _unit.Context.Entry(item).Property(t => t.DocumentTypeId).IsModified = true;
                    _unit.Context.Entry(item).Property(t => t.Remark).IsModified = true;
                    _unit.Context.Entry(item).Property(t => t.LastUpdateDate).IsModified = true;
                }
                else
                {
                    _unit.Context.Tbluserdoument.Add(item);
                }
                item.Status = 1;
                item.InsertDate = DateTime.Now;
                item.LastUpdateDate = DateTime.Now;
                item.DocumentTypeId = item.DocumentTypeId;
                item.Remark = item.Remark;

            }
            if (datamodel.Tblfeedetail.Count > 0)
            {
                foreach (var item in datamodel.Tblfeedetail)
                {
                    if (item.Id > 0)
                    {
                        _unit.Context.Tblfeedetail.Attach(item);
                        _unit.Context.Entry(item).Property(t => t.FeeValue).IsModified = true;
                        _unit.Context.Entry(item).Property(t => t.TotalFee).IsModified = true;
                        _unit.Context.Entry(item).Property(t => t.PaymentTerms).IsModified = true;
                        _unit.Context.Entry(item).Property(t => t.PaymentDueDate).IsModified = true;
                        _unit.Context.Entry(item).Property(t => t.Updatedate).IsModified = true;
                        _unit.Context.Entry(item).Property(t => t.UpdatedBy).IsModified = true;
                    }
                    else
                    {
                        _unit.Context.Tblfeedetail.Add(item);
                    }
                }
            }
            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Updated Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during Update");
        }
        public async Task<ApiResult<CompanyDetailQueryModel>> GetUserDetailByUserIdAsync(long id)
        {
            var query = await _unit.Context.Tbluser.Include(ud => ud.Tbluserdetail).Include(t => t.Client).ThenInclude(t => t.CityLocation).ThenInclude(t => t.District).ThenInclude(t => t.State).Include(d => d.Tbluserdoument).Include(t => t.Tblfeedetail).Include(t => t.Tbluseraddressmapping).ThenInclude(d => d.Address).ThenInclude(t => t.CityLocation).ThenInclude(t => t.District).ThenInclude(t => t.State).Where(p => p.Id == id).FirstOrDefaultAsync();
            var paymentquery = _unit.Context.Tblfeedetail.Include(t => t.Tblfranchiseepayments).ThenInclude(t => t.PaymentType).Where(t => t.UserId == id).AsQueryable();

            if (query == null)
                return new ApiResult<CompanyDetailQueryModel>(new ApiResultCode(ApiResultType.Success, messageText: "data not found for selected UserId! "));
            try
            {
                var result = new CompanyDetailQueryModel
                {
                    CompanyId = query.Client != null ? query.Client.Id : 0,
                    CompanyName = query.Client != null ? query.Client.Name : string.Empty,
                    CompanyContactNo = query.Client != null ? query.Client.PhoneNo : string.Empty,
                    CompanyAddress = query.Client != null ? query.Client.FullAddress : string.Empty,
                    CompanyZip = query.Client != null ? Convert.ToString(query.Client.ZipCode) : string.Empty,
                    ComapnyPAN = query.Client != null ? query.Client.CompanyPanCardNo : string.Empty,
                    CompanyGSTNo = query.Client != null ? query.Client.CompanyGstNo : string.Empty,
                    CompanyEmail = query.Client != null ? query.Client.Email : string.Empty,
                    StateId = query.Client.CityLocation != null ? (query.Client.CityLocation.District.StateId.HasValue ? query.Client.CityLocation.District.StateId.Value : 0) : 0,
                    DistrictId = query.Client.CityLocation != null ? ((query.Client.CityLocation.DistrictId.HasValue) ? query.Client.CityLocation.DistrictId.Value : 0) : 0,
                    CityLocationId = query.Client != null ? query.Client.CityLocationid.HasValue ? query.Client.CityLocationid.Value : 0 : 0,
                    User = new UserQueryModel
                    {
                        Id = query.Id,
                        FirstName = query.FirstName,
                        MiddleName = query.MiddleName,
                        LastName = query.LastName,
                        Email = query.Email,
                        PhoneNumber = query.PhoneNumber,
                        PasswordHash = query.PasswordHash,
                        UserDetail = query.Tbluserdetail.Count > 0 ? query.Tbluserdetail.Select(ud => new UserDetailQueryModel
                        {
                            Id = ud.Id,
                            FullName = ud.FullName,
                            AlternateEmail = ud.AlternateEmail,
                            AlternateNumber = ud.AlternateNumber,
                            ImagePath = ud.Image,
                            UserAdharCardNo = ud.OwnersAadharCardNo,
                            UserPAN = ud.OwnerPancardNo
                        }).FirstOrDefault() : new UserDetailQueryModel(),
                        UserDocument = query.Tbluserdetail.Count > 0 ? query.Tbluserdoument.Select(doc => new DocumentQueryModel
                        {
                            DocId = doc.Id,
                            UserId = doc.UserId,
                            ClientId = doc.ClientId,
                            DocumentTypeId = doc.DocumentTypeId,
                            DocumentName = _unit.Context.Tbldocumenttype.Where(t => t.Id == doc.DocumentTypeId).Select(s => s.Name).FirstOrDefault(),
                            Remark = doc.Remark,
                            DocFilePath = doc.DocImagePath,
                            DocumentUploadDate = doc.InsertDate.HasValue? doc.InsertDate.Value.ToString("dd/MM/yyyy") : string.Empty
                        }).ToList() : new List<DocumentQueryModel>(),
                        FeeDetails = query.Tblfeedetail.Count > 0 ? query.Tblfeedetail.Select(t => new FeeDetailResponseModel
                        {
                            FeeId = t.Id,
                            FeeType = _unit.Context.Tblfeetype.Where(p => p.Id == t.FeeTypeId).Select(k => k.Name).FirstOrDefault(),
                            FeeValue = t.FeeValue.HasValue ? t.FeeValue.Value : 0,                            
                            PaymentTerms = t.PaymentTerms,
                            PaymentDueDate = t.PaymentDueDate.HasValue ? t.PaymentDueDate.Value.ToString("dd/MM/yyyy"): string.Empty,
                        }).ToList() : new List<FeeDetailResponseModel>(),
                        TotalFee = query.Tblfeedetail.Select(t => t.TotalFee).FirstOrDefault(),
                        Addresses = query.Tbluseraddressmapping.Count > 0 ? query.Tbluseraddressmapping.Select(t => new UserAddressQueryModel
                        {
                            AddressId = t.Address.Id,
                            FullAddress = t.Address.FullAddress,
                            UserZip = t.Address.ZipCode.HasValue ? t.Address.ZipCode.Value : 0,
                            StateId = t.Address.CityLocation != null ? t.Address.CityLocation.District.StateId.Value : 0,
                            DistrictId = t.Address.CityLocation != null ? t.Address.CityLocation.DistrictId.Value : 0,
                            CityLocationId = t.Address.CityLocationId.HasValue ? t.Address.CityLocationId.Value : 0,
                        }).ToList() : new List<UserAddressQueryModel>(),
                        Supervisor = _unit.Context.Tbluser.Where(p => p.Id == query.ContactPersonId).Select(k => new SupervisorDetail
                        {
                            Id = k.Id,
                            Name = string.Format("{0} {1} {2}", k.FirstName, k.MiddleName, k.LastName),
                            ContactNo = k.PhoneNumber,
                            Email = k.Email
                        }).FirstOrDefault()
                    },
                    PaymentDetails = paymentquery.Select(t => t.Tblfranchiseepayments.Select(k => new PaymentDetailQueryModel
                    {
                        FeeType = k.FeeDetail.FeeType.Name,
                        PaymentRef = k.PaymentReference,
                        PaymentDate = k.PaymentDate.HasValue ? k.PaymentDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                        PaymentType = k.PaymentType.PaymentType,
                        PaidAmout = k.TotalAmountPaid.HasValue ? k.TotalAmountPaid.Value : 0
                    }).ToList()).FirstOrDefault()
                };
                if (result != null)
                    return new ApiResult<CompanyDetailQueryModel>(new ApiResultCode(ApiResultType.Success), result);

                return new ApiResult<CompanyDetailQueryModel>(new ApiResultCode(ApiResultType.Success, messageText: "data not found for selected UserId! "));
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<CompanyDetailQueryModel>(new ApiResultCode(ApiResultType.Success, messageText: "data not found for selected UserId! "));
            }
        }

        public async Task<ApiResult<bool>> IsTestNameExist(string testname)
        {
            try
            {
                var userobj = await _unit.Context.Tbltest.AnyAsync(t => t.Name == testname);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), userobj);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, messageText: "Invalid Data cannot search!"));
            }
        }
    }
}
