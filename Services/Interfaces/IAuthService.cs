using TTCTest.Models.Db;
using TTCTest.Models.DTOs.Auth;

namespace TTCTest.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken = default);
    Task<AuthResult> LoginAsync(LoginViewModel model, CancellationToken cancellationToken = default);
}
