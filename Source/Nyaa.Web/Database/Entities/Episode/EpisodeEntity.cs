using System.ComponentModel.DataAnnotations;
using Core.Database.Models;
using Nyaa.Web.Database.Entities.Anime;

namespace Nyaa.Web.Database.Entities.Episode;

public sealed class EpisodeEntity : EntityBase<long>
{
    public required long AnimeId { get; set; }

    [StringLength(512)]
    public required string Name { get; set; }

    [StringLength(512)]
    public required string Source { get; set; }

    public required int EpisodeNumber { get; set; }

    public AnimeEntity? Anime { get; set; }
}