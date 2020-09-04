using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class LonginCommandHandler : IRequestHandler<LoginCommand, IActionResult>
    {
        private readonly IAccountRepository _account;
        private readonly AppSettings _appSettings;

        public LonginCommandHandler(IAccountRepository account, IOptions<AppSettings> appSettings)
        {
            _account = account;
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            LoginResponse model = new LoginResponse();
            var _response = new SingleResponse<LoginResponse>();
            request.Password = Utilities.Utility.EncryptionLibrary.EncryptText(request.Password);
            var result = await _account.AuthenticateUsers(request);
            if (!result.HasSuccess)
            {
                _response.Message = "Invalid userid password or account is not activated";
                _response.Status = false;
            }
            else
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                                        new Claim("UserId", result.UserObject.UserId.ToString()),
                                        new Claim("UserTypeId", result.UserObject.UserTypeId.HasValue ? result.UserObject.UserTypeId.ToString() : string.Empty),
                                        new Claim("UserType", result.UserObject.UserType ?? string.Empty),
                                        new Claim("UserName", result.UserObject.UserName ?? string.Empty),
                                        new Claim("Email", result.UserObject.Email ?? string.Empty),
                                        new Claim("Phone", result.UserObject.Phone ?? string.Empty),
                                        new Claim("RoleId", result.UserObject.RoleId.HasValue ? result.UserObject.RoleId.ToString() : string.Empty),
                                        new Claim("Role", result.UserObject.Role?? string.Empty),
                                         new Claim("ClinetId", result.UserObject.ClinetId.HasValue ? result.UserObject.ClinetId.ToString() : string.Empty),
                                        new Claim("ClinetName", result.UserObject.ClinetName?? string.Empty)
                    }),
                    Expires = DateTime.UtcNow.AddDays(180),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                model.Token = tokenHandler.WriteToken(token);
                model.UserRole = result.UserObject.Role ?? string.Empty;
                _response.Data = model;
                _response.Message = "OTP verifed successfully";
                _response.Status = true;

            }
            return _response.ToHttpResponse();
        }
    }
}
