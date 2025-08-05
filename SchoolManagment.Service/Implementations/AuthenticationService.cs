using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Helper;
using SchoolManagment.Data.Results;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagment.Service.Implementations
{
    public class AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository,
        UserManager<User> userManager, IEmailService emailService, AppDbContext context) : IAuthenticationService
    {
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                RefreshToken = refreshToken.Token,
                Token = accessToken,
                UserId = user.Id
            };
            await refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken = accessToken;
            return response;
        }

        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                Token = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.Token = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { jwtSettings.Issuer },
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidAudience = jwtSettings.Audience,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await refreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == userId);
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpireAt < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpireAt;
            return (userId, expirydate);
        }
        public async Task<string> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return "ErrorWhenConfirmEmail";
            var user = await userManager.FindByIdAsync(userId);
            var confirmEmail = await userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }

        public async Task<string> SendResetPasswordCode(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return "UserNotFound";
            Random generateRandomNumber = new Random();
            var code = generateRandomNumber.Next(100000, 999999).ToString("D6");
            user.Code = code;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return "ErrorWhenSendResetPasswordCode";
            }
            var message = $"Your reset password code is {code}.";
            await emailService.SendEmail(user.Email, message, "Reset Password Code");
            return "Success";
        }

        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);

            if (user == null)
                return "UserNotFound";

            var userCode = user.Code;

            if (userCode == Code) return "Success";
            return "Failed";
        }
        public async Task<string> ResetPassword(string Email, string Password)
        {
            var trans = await context.Database.BeginTransactionAsync();
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user == null)
                    return "UserNotFound";
                await userManager.RemovePasswordAsync(user);
                if (!await userManager.HasPasswordAsync(user))
                {
                    await userManager.AddPasswordAsync(user, Password);
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
    }
}
