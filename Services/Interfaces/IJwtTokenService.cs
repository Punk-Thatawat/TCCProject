using TTCTest.Models.Db;

namespace TTCTest.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
