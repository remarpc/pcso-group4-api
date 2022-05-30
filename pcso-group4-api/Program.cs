using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameDb>(opt => opt.UseInMemoryDatabase("GameList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();



app.MapGet("/", () => "Hello World!");

app.MapGet("/gameitems", async (GameDb db) =>
    await db.Games.ToListAsync());

app.MapPost("/gameitems", async (Game game, GameDb db) =>
{
    db.Games.Add(game);
    await db.SaveChangesAsync();

    return Results.Created($"/gameitems/{game.GameID}", game);
});

app.MapPut("/gameitems/{id}", async (int id, Game inputGame, GameDb db) =>
{
    var game = await db.Games.FindAsync(id);

    if (game is null) return Results.NotFound();

    game.GameID = inputGame.GameID;
    game.Digit1 = inputGame.Digit1;
    game.Digit2 = inputGame.Digit2;
    game.Digit3 = inputGame.Digit3;
    game.Digit4 = inputGame.Digit4;
    game.Digit5 = inputGame.Digit5;
    game.Digit6 = inputGame.Digit6;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/gameitems/{id}", async (int id, GameDb db) =>
{
    if (await db.Games.FindAsync(id) is Game game)
    {
        db.Games.Remove(game);
        await db.SaveChangesAsync();
        return Results.Ok(game);
    }
    return Results.NotFound();
});

app.MapDelete("/gameitems", async (GameDb db) =>
{
    await db.Database.EnsureDeletedAsync();
    await db.SaveChangesAsync();
    return Results.Ok(null);
});

app.Run();

class Game
{
    public int Id { get; set; }
    public int GameID { get; set; }
    public int Digit1 { get; set; }
    public int Digit2 { get; set; }
    public int Digit3 { get; set; }
    public int Digit4 { get; set; }
    public int Digit5 { get; set; }
    public int Digit6 { get; set; }
}

class GameDb : DbContext
{
    public GameDb(DbContextOptions<GameDb> options)
        : base(options) { }

    public DbSet<Game> Games => Set<Game>();
}

