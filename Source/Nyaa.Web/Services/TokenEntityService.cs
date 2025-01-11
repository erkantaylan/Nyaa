using Nyaa.Web.Database.Entities.Token;

namespace Nyaa.Web.Services;

public class TokenEntityService
{
    public TokenEntity Create()
    {
        string token = GenerateToken();

        return new TokenEntity
        {
            Token = token,
            ExpiredAt = DateTimeOffset.UtcNow.AddDays(7)
        };
    }

    private string GenerateToken()
    {
        return Guid.NewGuid().ToString();
    }
}
