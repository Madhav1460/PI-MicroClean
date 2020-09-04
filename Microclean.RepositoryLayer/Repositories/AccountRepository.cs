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
using Utilities.Results;
using Utilities.Logger;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;

namespace Microclean.RepositoryLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public AccountRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }
        public async Task<ApiResultCode> UserRegisterAsync(Tblclient datamodel)
        {

            _ = _unit.GetRepository<Tblclient>().Add(datamodel);
            var result = await _unit.SaveChangesAsync();
            

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Register Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during create");
        }

        public async Task<ApiResultCode> UserUpdateAsync(Tbluser datamodel)
        {

            _unit.Context.Tbluser.Attach(datamodel);
            _unit.Context.Entry(datamodel).Property(t => t.FirstName).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.MiddleName).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.LastName).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.LastUpdateDate).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.PhoneNumber).IsModified = true;

            foreach (var item in datamodel.Tbluserdetail)
            {
                if (item.Id > 0)
                {
                    _unit.Context.Tbluserdetail.Attach(item);
                    _unit.Context.Entry(item).Property(t => t.AlternateNumber).IsModified = true;
                    _unit.Context.Entry(item).Property(t => t.AlternateEmail).IsModified = true;
                    _unit.Context.Entry(item).Property(t => t.LastUpdateDate).IsModified = true;
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

            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Updated Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during Update");
        }

        public async Task<ApiResult<IList<DocumentTypeQueryModel>>> GetAllDocumentTypeAsync()
        {
            try
            {
                var query = await _unit.Context.Tbldocumenttype.Where(dt => dt.Status == 1)
                    .Select(t => new DocumentTypeQueryModel
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToListAsync();
                if (query != null)
                    return new ApiResult<IList<DocumentTypeQueryModel>>(new ApiResultCode(ApiResultType.Success), query);

                return new ApiResult<IList<DocumentTypeQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "data not found ! "));
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IList<DocumentTypeQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }

        }

        public async Task<ApiResult<UserClameResponse>> AuthenticateUsers(LoginCommand request)
        {
            UserClameResponse userobj = null;
            try
            {
                userobj = await _unit.Context.Tbluser.Where(t => (t.Email == request.UserName || t.PhoneNumber == request.UserName) && t.PasswordHash == request.Password && t.IsApproved == 1)
                    .Select(t => new UserClameResponse
                    {
                        UserId = t.Id,
                        Email = t.Email,
                        Phone = t.PhoneNumber,
                        RoleId = t.Tbluserrolemapping.Where(p => p.UserId == t.Id).Select(t => t.RoleId).FirstOrDefault(),
                        Role = t.Tbluserrolemapping.Where(p => p.UserId == t.Id).Select(t => t.Role.Name).FirstOrDefault(),
                        UserName = t.NormalizedUserName,
                        UserTypeId = t.UserType.Id,
                        UserType = t.UserType.Name,
                        ClinetId = t.Client.Id,
                        ClinetName = t.Client.Name

                    }).FirstOrDefaultAsync();
                return new ApiResult<UserClameResponse>(new ApiResultCode(ApiResultType.Success), userobj);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<UserClameResponse>(new ApiResultCode(ApiResultType.Error, messageText: "Error during Update"), userobj);
            }

        }

        public async Task<ApiResult<IEnumerable<GetAllFranchiseeQuery>>> GetAllFranchisee()
        {
            var result = await _unit.GetRepository<Tbluser>().GetSelectedDataAsync(p => p.Status == 1 && p.UserTypeId == 2, t => new GetAllFranchiseeQuery
            {
                Id = t.Id,
                FullName = t.Client.Name,
                EmailId = t.Client.Email,
                PhoneNo = t.Client.PhoneNo,
                IsApproved = Convert.ToBoolean(t.IsApproved),
                ApproveStatus = t.IsApproved == 1 ? "Approve" : "Disapprove"
            });
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<GetAllFranchiseeQuery>>(new ApiResultCode(ApiResultType.Success), result.UserObject);

            return new ApiResult<IEnumerable<GetAllFranchiseeQuery>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResult<FranchiseeApprovalResonse>> FranchiseeApproveAsync(ClientApproveCommand datamodel)
        {
            var tbluser = new Tbluser();
            tbluser.Id = datamodel.Id;
            if (datamodel.IsApproved == true)
                tbluser.IsApproved = 1;

            if (datamodel.IsApproved == false)
                tbluser.IsApproved = 0;

            tbluser.LastUpdatedBy = datamodel.CurrentUserId;
            tbluser.LastUpdateDate = DateTime.Now;

            _unit.Context.Tbluser.Attach(tbluser);
            _unit.Context.Entry(tbluser).Property(t => t.IsApproved).IsModified = true;
            _unit.Context.Entry(tbluser).Property(t => t.LastUpdatedBy).IsModified = true;
            _unit.Context.Entry(tbluser).Property(t => t.LastUpdateDate).IsModified = true;

            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
            {
                FranchiseeApprovalResonse resonse = new FranchiseeApprovalResonse();
                if (datamodel.IsApproved == true)
                    resonse.ApproveStatus = "Approve";

                if (datamodel.IsApproved == false)
                    resonse.ApproveStatus = "Disapprove ";

                return new ApiResult<FranchiseeApprovalResonse>(new ApiResultCode(ApiResultType.Success, messageText: "Update Successfully"), resonse);
            }

            return new ApiResult<FranchiseeApprovalResonse>(new ApiResultCode(ApiResultType.Error, messageText: "Error during create"));
        }

        public async Task<ApiResult<UplodLoiDocFileResponse>> FranchiseeLoiUpload(Tbluserdoument datamodel)
        {
            UplodLoiDocFileResponse uplodLoi = new UplodLoiDocFileResponse();
            uplodLoi.FileName = datamodel.Remark;

            if (datamodel.Id > 0)
            {
                _ = _unit.Context.Tbluserdoument.Attach(datamodel);
                _unit.Context.Entry(datamodel).Property(t => t.DocImagePath).IsModified = true;
            }
            else
            {
                _ = _unit.GetRepository<Tbluserdoument>().Add(datamodel);
            }
            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResult<UplodLoiDocFileResponse>(new ApiResultCode(ApiResultType.Success, messageText: "File uploaded Successfully"), uplodLoi);

            return new ApiResult<UplodLoiDocFileResponse>(new ApiResultCode(ApiResultType.Error, messageText: "Error during Update"));
        }

        public async Task<ApiResult<LoiDocumentResponseModel>> GetDocumetByFranchiseeId(GetLOIDocumentByUserIdQuery request)
        {
            if (request.FranchiseeId > 0)
            {
                var result = await _unit.Context.Tbluserdoument.Where(p => p.Status == 1 && p.UserId == request.FranchiseeId && p.DocumentTypeId == 7).Select(t => new LoiDocumentResponseModel
                {
                    DocId = t.Id,
                    DocPath = t.DocImagePath,
                    DocName = t.Remark,
                    DocumentType = t.DocumentTypeId

                }).FirstOrDefaultAsync();
                if (result != null)
                    return new ApiResult<LoiDocumentResponseModel>(new ApiResultCode(ApiResultType.Success), result);
            }
            else
            {
                var result = await _unit.Context.Tbluserdoument.Where(p => p.Status == 1 && p.UserId == request.CurrentUserId && p.DocumentTypeId == 6).Select(t => new LoiDocumentResponseModel
                {
                    DocId = t.Id,
                    DocPath = t.DocImagePath,
                    DocName = t.Remark,
                    DocumentType = t.DocumentTypeId

                }).FirstOrDefaultAsync();
                if (result != null)
                    return new ApiResult<LoiDocumentResponseModel>(new ApiResultCode(ApiResultType.Success), result);
            }

            return new ApiResult<LoiDocumentResponseModel>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }
        public async Task<ApiResultCode> CreateUserByFranchiseeAsync(Tbluser datamodel)
        {
            _ = _unit.GetRepository<Tbluser>().Add(datamodel);
            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Register Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during create");
        }
        public async Task<ApiResult<IEnumerable<UserResponseQueryModel>>> GetAllUserForFranchisee(GetAllUserQuery filter)
        {
            var result = await _unit.GetRepository<Tbluser>().GetSelectedDataAsync(p => p.Status == 1 && p.UserTypeId == 3 && p.InsertedBy == filter.CurrentUserId && p.ClientId == filter.CurrentCientId, t => new UserResponseQueryModel
            {
                Id = t.Id,
                FullName = string.Format("{0} {1} {2}", t.FirstName, t.MiddleName, t.LastName),
                EmailId = t.Email,
                PhoneNo = t.PhoneNumber,
                IsApproved = Convert.ToBoolean(t.IsApproved),
                ApproveStatus = t.IsApproved == 1 ? "Approve" : "Disapprove"
            });
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<UserResponseQueryModel>>(new ApiResultCode(ApiResultType.Success), result.UserObject);

            return new ApiResult<IEnumerable<UserResponseQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResultCode> ChangePasswordAsync(ChangePasswordCommand datamodel)
        {
            var tbluser = _unit.GetRepository<Tbluser>().GetByID(datamodel.CurrentUserId.Value).Result.UserObject;
            var oldpass = Utilities.Utility.EncryptionLibrary.DecryptText(tbluser.PasswordHash);
            if (oldpass == datamodel.OldPassword)
            {
                tbluser.PasswordHash = Utilities.Utility.EncryptionLibrary.EncryptText(datamodel.NewPassword);
                tbluser.LastUpdateDate = DateTime.Now;
                _unit.Context.Attach(tbluser);
                _unit.Context.Entry(tbluser).State = EntityState.Modified;
                var result = await _unit.SaveChangesAsync();

                if (result.ResultType == ApiResultType.Success)
                    return new ApiResultCode(ApiResultType.Success, messageText: "Updated Successfully");

            }


            return new ApiResultCode(ApiResultType.Error, messageText: "Old Password Doesn't match-Error during Update");
        }

        public async Task<ApiResult<List<KycDocumetResposneModel>>> GetKycDocumetsByFranchiseeId(long franchiseeId)
        {
            List<short> doctype = new List<short> {
                1,2,3,4,5,6,7
            };
            var kycdoc = await _unit.Context.Tbluserdoument.Where(p => p.Status == 1 && p.UserId == franchiseeId && doctype.Contains(p.DocumentTypeId.Value)).Select(t => new KycDocumetResposneModel
            {
                DocId = t.Id,
                DocPath = t.DocImagePath,
                DocName = t.Remark,
                DocumentType = t.DocumentTypeId

            }).ToListAsync();
            if (kycdoc != null)
                return new ApiResult<List<KycDocumetResposneModel>>(new ApiResultCode(ApiResultType.Success), kycdoc);

            return new ApiResult<List<KycDocumetResposneModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResultCode> UserAddressUpdateAsync(Tbladdress datamodel)
        {
            _unit.Context.Tbladdress.Attach(datamodel);
            _unit.Context.Entry(datamodel).Property(t => t.FullAddress).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.StateId).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.CityId).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.CityLocationId).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.LandMark).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.LastUpdateDate).IsModified = true;
            _unit.Context.Entry(datamodel).Property(t => t.LastUpdatedBy).IsModified = true;
            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Updated Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during Update");
        }
        public async Task<ApiResult<bool>> IsPhoneExist(string phoneno)
        {
            try
            {
                var userobj = await _unit.Context.Tbluser.AnyAsync(t => t.PhoneNumber == phoneno);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), userobj);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, messageText: "Error during Update"));
            }

        }
        public async Task<ApiResult<bool>> IsEmailExist(string email)
        {
            try
            {
                var userobj = await _unit.Context.Tbluser.AnyAsync(t => t.Email == email);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), userobj);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, messageText: "Error during Update"));
            }

        }
        //public async Task<ApiResult<IEnumerable<SupervisorResponseQueryModel>>> GetAllSupervisorForAdmin(long currentUserid)GetAllComapanyUserQuery
        public async Task<ApiResult<IEnumerable<SupervisorResponseQueryModel>>> GetAllSupervisorForAdmin(GetAllComapanyUserQuery filter)
        {
            var result = await _unit.GetRepository<Tbluser>().GetSelectedDataAsync(p => p.Status == 1 && p.UserTypeId == 4 && p.InsertedBy == filter.CurrentUserId.Value && p.ClientId==filter.CurrentCientId, t => new SupervisorResponseQueryModel
            {
                Id = t.Id,
                FullName = string.Format("{0} {1} {2}", t.FirstName, t.MiddleName, t.LastName),
                EmailId = t.Email,
                PhoneNo = t.PhoneNumber,
                IsApproved = Convert.ToBoolean(t.IsApproved),
                ApproveStatus = t.IsApproved == 1 ? "Approve" : "Disapprove"
            });
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<SupervisorResponseQueryModel>>(new ApiResultCode(ApiResultType.Success), result.UserObject);

            return new ApiResult<IEnumerable<SupervisorResponseQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }
    }
}
