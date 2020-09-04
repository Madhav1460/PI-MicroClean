using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataModel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Results;


namespace Microclean.RepositoryLayer.IRepositories
{
    public interface IAccountRepository
    {
        Task<ApiResultCode> UserRegisterAsync(Tblclient datamodel);
        Task<ApiResultCode> UserUpdateAsync(Tbluser datamodel);
        Task<ApiResult<IList<DocumentTypeQueryModel>>> GetAllDocumentTypeAsync();
        Task<ApiResult<UserClameResponse>> AuthenticateUsers(LoginCommand request);
        Task<ApiResult<IEnumerable<GetAllFranchiseeQuery>>> GetAllFranchisee();
        Task<ApiResult<FranchiseeApprovalResonse>> FranchiseeApproveAsync(ClientApproveCommand datamodel);
        Task<ApiResult<UplodLoiDocFileResponse>> FranchiseeLoiUpload(Tbluserdoument datamodel);
        Task<ApiResult<LoiDocumentResponseModel>> GetDocumetByFranchiseeId(GetLOIDocumentByUserIdQuery request);
        Task<ApiResultCode> CreateUserByFranchiseeAsync(Tbluser datamodel);
        Task<ApiResult<IEnumerable<UserResponseQueryModel>>> GetAllUserForFranchisee(GetAllUserQuery filter);
        Task<ApiResultCode> ChangePasswordAsync(ChangePasswordCommand datamodel);
        Task<ApiResult<List<KycDocumetResposneModel>>> GetKycDocumetsByFranchiseeId(long franchiseeId);
        Task<ApiResultCode> UserAddressUpdateAsync(Tbladdress datamodel);
        Task<ApiResult<bool>> IsPhoneExist(string phoneno);
        Task<ApiResult<bool>> IsEmailExist(string email);
        Task<ApiResult<IEnumerable<SupervisorResponseQueryModel>>> GetAllSupervisorForAdmin(GetAllComapanyUserQuery filter);
    }
}
