using TTCTest.Models.Db;

namespace TTCTest.Services;

public class AuthResult
{
    public bool Succeeded { get; init; }
    public string Message { get; init; } = string.Empty;
    public User? User { get; init; }

    public static AuthResult Success(string message, User user)
    {
        return new AuthResult
        {
            Succeeded = true,
            Message = message,
            User = user
        };
    }

    public static AuthResult Fail(string message)
    {
        return new AuthResult
        {
            Succeeded = false,
            Message = message
        };
    }
}
