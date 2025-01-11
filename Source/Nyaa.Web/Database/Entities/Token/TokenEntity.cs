using System.ComponentModel.DataAnnotations;
using Core.Database.Models;

namespace Nyaa.Web.Database.Entities.Token;

public class TokenEntity : EntityBase<long>
{
    [StringLength(512)]
    public required string Token { get; set; }
    public DateTimeOffset ExpiredAt { get; set; }
}
