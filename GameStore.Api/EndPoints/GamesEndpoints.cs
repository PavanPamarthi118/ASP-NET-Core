using GameStore.Api.Dtos;

namespace GameStore.Api.EndPoints;

public static class GamesEndpoints
{
    const string GetGameById = "GetGameById";

    private static readonly List<GameDto> Games = new List<GameDto>
{
    new GameDto(1, "Game1", "Action", 59.99m, DateOnly.FromDateTime(new DateTime(2022, 10, 15))),
    new GameDto(2, "Game2", "Adventure", 49.99m, DateOnly.FromDateTime(new DateTime(2021, 5, 20))),
    new GameDto(3, "Game3", "RPG", 39.99m, DateOnly.FromDateTime(new DateTime(2020, 3, 10)))
};
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");
        //GET endpoints for games
        group.MapGet("/", () => Results.Ok(Games));

        // GET endpoint for a specific game by ID
        group.MapGet("/{id}", (int id) =>
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        });

        // POST endpoint to add a new game
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(Games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
            // Simulate ID generation by using the count of existing games + 1
            Games.Add(game);
            return Results.CreatedAtRoute(GetGameById, new { id = game.Id }, game);
        }).WithName(GetGameById);

        // PUT endpoint to update an existing game
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = Games.FindIndex(g => g.Id == id);
            if (index == -1) return Results.NotFound();

            Games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate);
            // Update the game in the list
            return Results.NoContent();
        });

        // DELETE endpoint to remove a game by ID
        group.MapDelete("/{id}", (int id) =>
        {
            var index = Games.FindIndex(g => g.Id == id);
            if (index == -1) return Results.NotFound();

            Games.RemoveAt(index);
            return Results.NoContent();
        });

        return group;

    }
}
