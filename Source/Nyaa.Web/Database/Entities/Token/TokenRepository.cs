using Core.Database;
using Core.Framework;

namespace Nyaa.Web.Database.Entities.Token;

[MercuryRepository]
public class TokenRepository : Repository<TokenEntity, long, NyaaDbContext>
{
    public TokenRepository(NyaaDbContext context) : base(context) { }
}
