using MedicalRecords.Domain.Requests.User;
using MedicalRecords.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken = default);
        Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken = default);
        Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken cancellationToken = default);
    }
}
