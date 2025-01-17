using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Nyaa.Web.Database.Entities.Anime;
using Nyaa.Web.Database.Entities.Episode;
using Nyaa.Web.Database.Entities.Token;

namespace Nyaa.Web.Database;

public class NyaaDbContext : SourceDbContext<NyaaDbContext>
{
    public NyaaDbContext(DbContextOptions<NyaaDbContext> options) : base(options) { }

    public DbSet<AnimeEntity> Animes { get; set; }
    public DbSet<EpisodeEntity> Episodes { get; set; }
    public DbSet<TokenEntity> Tokens { get; set; }
}