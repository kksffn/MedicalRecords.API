using MedicalRecords.Domain.Repositories;
using MedicalRecords.Domain.Requests.User;
using MedicalRecords.Domain.Responses;
using MedicalRecords.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    class UserService : IUserService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IOptions<AuthenticationSettings> authenticationSettings)
        {
            _userRepository = userRepository;
            _authenticationSettings = authenticationSettings.Value;
        }

        public async Task<UserResponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            return new UserResponse { UserNickname = response.UserNickname, Email = response.Email };
        }

        public async Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken)
        {
            var user = new Entities.User { Email = request.Email, UserName = request.Email, UserNickname = request.UserNickname };
            bool isCreated = await _userRepository.SignUpAsync(user, request.Password, cancellationToken);

            return !isCreated ? null : new UserResponse { UserNickname = request.UserNickname, Email = request.Email };
        }

        public async Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken cancellationToken)
        {
            bool isAuthenticated = await _userRepository.AuthenticateAsync(request.Email, request.Password, cancellationToken);

            return !isAuthenticated ? null : new TokenResponse { Token = GenerateSecurityToken(request) };
        }

        private string GenerateSecurityToken(SignInRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, request.Email)
                }),
                Expires = DateTime.Now.AddDays(_authenticationSettings.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}