using System;

namespace GameStore.Api.Entities;

public class Game

{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int  GenreId { get; set; }
    public Genre? Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }

    public Game(int id, string name, string genre, decimal price, DateOnly releaseDate)
    {
        Id = id;
        Name = name;
        Genre = new Genre { Name = genre };
        GenreId = id; // Assuming GenreId is the same as Id for simplicity
        Price = price;
        ReleaseDate = releaseDate;
    }

}
