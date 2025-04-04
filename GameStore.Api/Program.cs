using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameById = "GetGameById";

var games = new List<GameDto>
{
    new(1, "Game1", "Action", 59.99m, new(2022, 10, 15)),
    new(2, "Game2", "Adventure", 49.99m, new(2021, 5, 20)),
    new(3, "Game3", "RPG", 39.99m, new(2020, 3, 10))
};

//GET endpoints for games
app.MapGet("/games", () => games);

// GET endpoint for a specific game by ID
app.MapGet("/games/{id}", (int id) => 
{
    var game = games.FirstOrDefault(g => g.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
});

// POST endpoint to add a new game
app.MapPost("/games", (CreateGameDto newGame) => 
{
    GameDto game = new(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
    // Simulate ID generation by using the count of existing games + 1
    games.Add(game);
    return Results.CreatedAtRoute(GetGameById, new {id = game.Id}, game);
}).WithName(GetGameById);

// PUT endpoint to update an existing game
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) => 
{
    var index = games.FindIndex(g => g.Id == id);
    if (index == -1) return Results.NotFound();
    
    games[index] = new GameDto(
        id, 
        updatedGame.Name, 
        updatedGame.Genre, 
        updatedGame.Price, 
        updatedGame.ReleaseDate);
    // Update the game in the list
    return Results.NoContent();
});

// DELETE endpoint to remove a game by ID
app.MapDelete("/games/{id}", (int id) => 
{
    var index = games.FindIndex(g => g.Id == id);
    if (index == -1) return Results.NotFound();
    
    games.RemoveAt(index);
    return Results.NoContent();
});

app.Run();
