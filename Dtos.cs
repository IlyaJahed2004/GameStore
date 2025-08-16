using System.ComponentModel.DataAnnotations;

namespace GamesStore.Dtos;

public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string imageuri
);

public record CreateGameDto(
    [Required] [StringLength(50)] string Name,
    [Required] [MaxLength(20)] string Genre,
    [Range(1, 100)] decimal Price,
    DateTime ReleaseDate,
    [Required] [Url] string imageuri
);

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    [Required] [MaxLength(20)] string Genre,
    [Range(1, 100)] decimal Price,
    DateTime ReleaseDate,
    [Required] [Url] string imageuri
);
