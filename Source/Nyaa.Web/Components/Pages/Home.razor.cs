using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Nyaa.Web.Database;
using Nyaa.Web.Database.Entities.Anime;
using Nyaa.Web.Database.Entities.Episode;

namespace Nyaa.Web.Components.Pages;

public partial class Home
{
    private List<PageAnime> animes = [];

    private string newAnimeName = string.Empty;

    [Inject]
    public required NyaaUnitOfWork Nyaa { get; set; }

    protected override async Task OnInitializedAsync()
    {
        animes.Clear();
        List<AnimeEntity> entities = await Nyaa.Anime.Search().ToListAsync();
        foreach (AnimeEntity entity in entities)
        {
            animes.Add(new PageAnime(entity));
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //var offsetInMinutes = await JsRuntime.InvokeAsync<int>("new Date().getTimezoneOffset", new object());
        //timeZoneOffset = TimeSpan.FromMinutes(-offsetInMinutes); // Negative because getTimezoneOffset returns opposite of what we need

        // List<AnimeEntity> animeEntities = await Nyaa.Anime
        //                                             .Search()
        //                                             .AsNoTracking()
        //                                             .Include(dto => dto.Episodes)
        //                                             .ToListAsync();
    }

    private async Task<string> FormatDateTime(DateTimeOffset date)
    {
        var localDate = await JsRuntime.InvokeAsync<LocalDateResult>("convertToLocalDateTime", [date.ToString("O")]);

        return $"{localDate.Date} {localDate.Time}";
    }

    private async Task AddAnimeAsync()
    {
        var entity = new AnimeEntity
        {
            Name = newAnimeName
        };

        await Nyaa.Anime.InsertAsync(entity);

        newAnimeName = string.Empty;
        var pageAnime = new PageAnime(entity);
        animes.Add(pageAnime);
    }

    private async Task AddEpisode(PageAnime episode)
    {
        EpisodeEntity entity = episode.Create();
        await Nyaa.Episode.InsertAsync(entity);
        episode.Input.Clear();
    }

    private class LocalDateResult
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }

    private class PageAnime
    {
        public PageAnime(AnimeEntity anime)
        {
            Anime = anime;
            Input = new EpisodeInput();
        }

        public AnimeEntity Anime { get; }
        public EpisodeInput Input { get; }

        public EpisodeEntity Create()
        {
            return new EpisodeEntity
            {
                Name = Input.Name,
                Source = Input.Source,
                AnimeId = Anime.Id,
                EpisodeNumber = Input.EpisodeNumber
            };
        }
    }

    private class EpisodeInput
    {
        public string Name { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public int EpisodeNumber { get; set; }
        public bool IsWatched { get; set; }

        public void Clear()
        {
            Name = string.Empty;
            Source = string.Empty;
            EpisodeNumber = 0;
            IsWatched = false;
        }
    }
}