using TTCTest.Models.Db;
using TTCTest.Models.DTOs.Auth;
using TTCTest.Repositories.Interfaces;
using TTCTest.Services.Interfaces;

namespace TTCTest.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<AuthResult> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken = default)
    {
        var username = model.Username.Trim();

        if (await _userRepository.UsernameExistsAsync(username, cancellationToken))
        {
            return AuthResult.Fail("Username already exists.");
        }

        var user = new User
        {
            Username = username,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _passwordService.HashPassword(user, model.Password);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return AuthResult.Success("Register completed successfully.", user);
    }

    public async Task<AuthResult> LoginAsync(LoginViewModel model, CancellationToken cancellationToken = default)
    {
        var username = model.Username.Trim();
        var user = await _userRepository.GetByUsernameAsync(username, cancellationToken);

        if (user is null)
        {
            return AuthResult.Fail("Invalid username or password.");
        }

        var isPasswordValid = _passwordService.VerifyPassword(user, user.PasswordHash, model.Password);

        if (!isPasswordValid)
        {
            return AuthResult.Fail("Invalid username or password.");
        }

        return AuthResult.Success("Login completed successfully.", user);
    }
}
