using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;

namespace GameStore.Api.EndPoints;

public static class GamesEndpoints
{
    const string GetGameById = "GetGameById";

    private static readonly List<GameSummaryDto> Games = new List<GameSummaryDto>
{
    new GameSummaryDto(1, "Game1", "Action", 59.99m, DateOnly.FromDateTime(new DateTime(2022, 10, 15))),
    new GameSummaryDto(2, "Game2", "Adventure", 49.99m, DateOnly.FromDateTime(new DateTime(2021, 5, 20))),
    new GameSummaryDto(3, "Game3", "RPG", 39.99m, DateOnly.FromDateTime(new DateTime(2020, 3, 10)))
};
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");
        //GET endpoints for games
        group.MapGet("/", () => Results.Ok(Games));

        // GET endpoint for a specific game by ID
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);
            return game is not null ? Results.Ok(game.ToGameDetailsDto) : Results.NotFound();
        });

        // POST endpoint to add a new game
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameById, new { id = game.Id }, game.ToGameDetailsDto());
        }).WithName(GetGameById);

        // PUT endpoint to update an existing game
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = Games.FindIndex(g => g.Id == id);
            if (index == -1) return Results.NotFound();

            Games[index] = new GameSummaryDto(
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
