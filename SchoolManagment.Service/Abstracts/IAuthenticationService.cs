using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Results;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolManagment.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthResult> GetJWTToken(User user);
        JwtSecurityToken ReadJWTToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        Task<string> ValidateToken(string AccessToken);
        Task<string> ConfirmEmail(string userId, string code);
    }
}
