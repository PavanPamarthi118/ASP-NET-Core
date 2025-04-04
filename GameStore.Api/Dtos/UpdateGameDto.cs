namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate);
// This DTO is used for updating an existing game. It does not include the Id property, as it is not needed for the update operation.
