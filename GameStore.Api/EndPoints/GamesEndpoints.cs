using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

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
        // GET endpoints for games
        group.MapGet("/", async () => Results.Ok(Games));

        // GET endpoint for a specific game by ID
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            return game is not null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
        });

        // POST endpoint to add a new game
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameById, new { id = game.Id }, game.ToGameDetailsDto());
        }).WithName(GetGameById);

        // PUT endpoint to update an existing game
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            if (game is null) return Results.NotFound();

            game.Name = updatedGame.Name;
            game.Genre = updatedGame.Genre;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;

            dbContext.Games.Update(game);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE endpoint to remove a game by ID
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            if (game is null) return Results.NotFound();

            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
