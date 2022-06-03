using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Group4DbAzure"));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");


#region Games CRUD

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
    //game.Digit1 = inputGame.Digit1;
    //game.Digit2 = inputGame.Digit2;
    //game.Digit3 = inputGame.Digit3;
    //game.Digit4 = inputGame.Digit4;
    //game.Digit5 = inputGame.Digit5;
    //game.Digit6 = inputGame.Digit6;

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

#endregion



#region Combination CRUD

app.MapGet("/combinationitems", async (GameDb db) =>
    await db.Combinations.ToListAsync());


app.MapPost("/combinationitems", async (Combination combination, GameDb db) =>
{
    db.Combinations.Add(combination);
    await db.SaveChangesAsync();

    return Results.Created($"/combinationitems/{combination.GameID}", combination);
});

app.MapPut("/combinationitems/{id}", async (int id, Combination combinationinput, GameDb db) =>
{
    var combination = await db.Combinations.FindAsync(id);

    if (combination is null) return Results.NotFound();

    combination.GameID = combinationinput.GameID;
    combination.Digit1 = combinationinput.Digit1;
    combination.Digit2 = combinationinput.Digit2;
    combination.Digit3 = combinationinput.Digit3;
    combination.Digit4 = combinationinput.Digit4;
    combination.Digit5 = combinationinput.Digit5;
    combination.Digit6 = combinationinput.Digit6;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/combinationitems/{id}", async (int id, GameDb db) =>
{
    if (await db.Combinations.FindAsync(id) is Combination combination)
    {
        db.Combinations.Remove(combination);
        await db.SaveChangesAsync();
        return Results.Ok(combination);
    }
    return Results.NotFound();
});


#endregion

#region Frequency

app.MapGet("/frequencyviewitems", async (GameDb db) =>

    await db.Combinations.FromSqlRaw("Select * FROM FrequencyViews").ToListAsync());  

#endregion


app.Run();


class GameDb : DbContext
{
    public GameDb(DbContextOptions<GameDb> options)
        : base(options) { }

    public DbSet<Game> Games { get; set; }
    public DbSet<Combination> Combinations { get; set; }       
    
}
class Game
{
    public int Id { get; set; }
    public int GameID { get; set; }
}

class Combination
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

