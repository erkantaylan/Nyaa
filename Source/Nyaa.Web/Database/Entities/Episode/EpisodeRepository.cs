using Core.Database;
using Core.Framework;
using JetBrains.Annotations;

namespace Nyaa.Web.Database.Entities.Episode;

[MercuryRepository]
[UsedImplicitly]
public class EpisodeRepository : Repository<EpisodeEntity, long, NyaaDbContext>
{
    public EpisodeRepository(NyaaDbContext context) : base(context) { }
}