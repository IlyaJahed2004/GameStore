using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;

public class Game
{
    public int Id { get; set; }

    [Required] // Must have a value (null/empty rejected)
    [MaxLength(50)] // String length ≤ 50 chars
    public required string Name { get; set; }

    [Required] // Must have a value
    [MaxLength(20)] // String length ≤ 20 chars
    public required string Genre { get; set; }

    [Range(1, 100)] // Decimal value must be between 1 and 100
    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }

    [Required] // Must have a value
    [Url] // Must be a valid URL format (http/https)
    public required string imageuri { get; set; }
}
