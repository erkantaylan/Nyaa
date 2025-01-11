using Core.Database;
using Core.Framework;
using JetBrains.Annotations;

namespace Nyaa.Web.Database.Entities.Anime;

[MercuryRepository]
[UsedImplicitly]
public class AnimeRepository : Repository<AnimeEntity, long, NyaaDbContext>
{
    public AnimeRepository(NyaaDbContext context) : base(context) { }
}