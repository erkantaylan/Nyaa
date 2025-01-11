using System.ComponentModel.DataAnnotations;
using Core.Database.Models;
using Nyaa.Web.Database.Entities.Episode;

namespace Nyaa.Web.Database.Entities.Anime;

public sealed class AnimeEntity : EntityBase<long>
{
    [StringLength(512)]
    public required string Name { get; set; }

    public ICollection<EpisodeEntity>? Episodes { get; set; }
}