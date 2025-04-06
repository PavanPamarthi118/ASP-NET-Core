using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate);
// This DTO is used for updating an existing game. It does not include the Id property, as it is not needed for the update operation.
