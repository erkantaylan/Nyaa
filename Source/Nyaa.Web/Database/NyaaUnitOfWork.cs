using Nyaa.Web.Database.Entities.Anime;
using Nyaa.Web.Database.Entities.Episode;
using Nyaa.Web.Database.Entities.Token;

namespace Nyaa.Web.Database;

public class NyaaUnitOfWork
{
    public NyaaUnitOfWork(
        NyaaDbContext context,
        AnimeRepository anime,
        EpisodeRepository episode,
        TokenRepository token
    )
    {
        Context = context;
        Anime = anime;
        Episode = episode;
        Token = token;
    }

    public NyaaDbContext Context { get; }
    public AnimeRepository Anime { get; }
    public EpisodeRepository Episode { get; }
    public TokenRepository Token { get; }
}
