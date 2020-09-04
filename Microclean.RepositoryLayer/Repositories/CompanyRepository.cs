using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public CompanyRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }

        public async Task<ApiResult<IEnumerable<GetAllAdminEmployeeQuery>>> GetAllAdminEmployee()
        {
            var result = await _unit.GetRepository<Tbluser>().GetSelectedDataAsync(p => p.Status == 1 && p.UserTypeId == 3, t => new GetAllAdminEmployeeQuery
            {
                Id = t.Id,
                FullName = string.Format("{0} {1} {2}", t.FirstName, t.MiddleName, t.LastName),
                EmailId = t.Email,
                PhoneNo = t.PhoneNumber,
                IsApproved = Convert.ToBoolean(t.IsApproved),
                ApproveStatus = t.IsApproved == 1 ? "Approve" : "Disapprove"
            });
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<GetAllAdminEmployeeQuery>>(new ApiResultCode(ApiResultType.Success), result.UserObject);

            return new ApiResult<IEnumerable<GetAllAdminEmployeeQuery>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResultCode> AllocateSuperviser(AllocateEmployeeCommand datamodel)
        {
            var tbluser = new Tbluser();
            tbluser.Id = datamodel.UserId;
            tbluser.ContactPersonId = datamodel.EmployeeId;

            tbluser.LastUpdatedBy = datamodel.CurrentUserId;
            tbluser.LastUpdateDate = DateTime.Now;

            _unit.Context.Tbluser.Attach(tbluser);
            _unit.Context.Entry(tbluser).Property(t => t.ContactPersonId).IsModified = true;
            //_unit.Context.Entry(tbluser).Property(t => t.IsApproved).IsModified = true;
            _unit.Context.Entry(tbluser).Property(t => t.LastUpdatedBy).IsModified = true;
            _unit.Context.Entry(tbluser).Property(t => t.LastUpdateDate).IsModified = true;

            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
            {
                return new ApiResultCode(ApiResultType.Success, messageText: "Update Successfully");
            }
            else
            {
                return new ApiResultCode(ApiResultType.Error, messageText: "Error during update");
            }
        }

        public async Task<ApiResult<CompanyDetailQueryModel>> GetUserDetailByUserIdAsync(long id)
        {
            var query = await _unit.Context.Tbluser.Include(ud => ud.Tbluserdetail).Include(t => t.Client).ThenInclude(t => t.CityLocation).ThenInclude(t => t.District).ThenInclude(t => t.State).Include(d => d.Tbluserdoument).Include(t => t.Tblfeedetail).Include(t => t.Tbluseraddressmapping).ThenInclude(d => d.Address).ThenInclude(t => t.CityLocation).ThenInclude(t => t.District).ThenInclude(t => t.State).Where(p => p.Id == id).FirstOrDefaultAsync();
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
                            DocFilePath = doc.DocImagePath
                        }).ToList() : new List<DocumentQueryModel>(),
                        FeeDetails = query.Tblfeedetail.Count > 0 ? query.Tblfeedetail.Select(t => new FeeDetailResponseModel
                        {
                            FeeType = _unit.Context.Tblfeetype.Where(p => p.Id == t.FeeTypeId).Select(k => k.Name).FirstOrDefault(),
                            FeeValue = t.FeeValue.HasValue ? t.FeeValue.Value : 0
                        }).ToList() : new List<FeeDetailResponseModel>(),
                        TotalFee = query.Tblfeedetail.Sum(t => t.FeeValue.HasValue ? t.FeeValue.Value : 0),
                        Addresses = query.Tbluseraddressmapping.Count > 0 ? query.Tbluseraddressmapping.Select(t => new UserAddressQueryModel
                        {
                            AddressId = t.Address.Id,
                            FullAddress = t.Address.FullAddress,
                            UserZip = t.Address.ZipCode.HasValue ? t.Address.ZipCode.Value : 0,
                            StateId = t.Address.CityLocation != null ? t.Address.CityLocation.District.StateId.Value : 0,
                            DistrictId = t.Address.CityLocation != null ? t.Address.CityLocation.DistrictId.Value : 0,
                            CityLocationId = t.Address.CityLocationId.HasValue ? t.Address.CityLocationId.Value : 0,
                        }).ToList() : new List<UserAddressQueryModel>()
                    }
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

        public async Task<ApiResult<CompanyDetailQueryModel>> AddNewFranchiseeByCompany(Tblclient datamodel)
        {
            _ = _unit.GetRepository<Tblclient>().Add(datamodel);
            var result = await _unit.SaveChangesAsync();
            if (result.ResultType == ApiResultType.Success)
            {
                long userid = datamodel.Tbluser.Select(t => t.Id).FirstOrDefault();
                var resultdata = await this.GetUserDetailByUserIdAsync(userid);
                if (resultdata.HasSuccess)
                    return new ApiResult<CompanyDetailQueryModel>(new ApiResultCode(ApiResultType.Success, messageText: "Register Successfully"),resultdata.UserObject);
            }
            return new ApiResult<CompanyDetailQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "Error during create"));
        }
    }
}
