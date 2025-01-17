using System.Linq.Expressions;
using Core.Database;
using Core.Framework;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Nyaa.Web.Database.Entities.Anime;

[MercuryRepository]
[UsedImplicitly]
public class AnimeRepository : Repository<AnimeEntity, long, NyaaDbContext>
{
    public AnimeRepository(NyaaDbContext context) : base(context) { }

    public override IQueryable<AnimeEntity> Search(
        Expression<Func<AnimeEntity, bool>>? filter = null,
        Func<IQueryable<AnimeEntity>, IOrderedQueryable<AnimeEntity>>? orderBy = null)
    {
        return base
               .Search(filter, orderBy)
               .AsNoTracking()
               .Include(o => o.Episodes);
    }
}