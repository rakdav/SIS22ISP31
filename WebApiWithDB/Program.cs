using Microsoft.EntityFrameworkCore;
using WebApiWithDB;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("api/users",async (ModelDB db) =>await db.Users.ToListAsync());
app.MapGet("api/users/{id:int}", async (int id, ModelDB db) =>
{
    User? user=await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    if (user == null) return Results.NotFound(new { message = "User not found" });
    return Results.Json(user);
});
app.MapDelete("api/users/{id:int}", async (int id, ModelDB db) =>
{
    User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    if (user == null) return Results.NotFound(new { message = "User not found" });
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});
app.MapPost("/api/users", async (User user,ModelDB db) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});
app.MapPut("/api/users", async (User userData, ModelDB db) =>
{
    User? user=await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);
    if (user == null) return Results.NotFound(new { message = "User not found" });
    user.Age = userData.Age;
    user.Name = userData.Name;
    await db.SaveChangesAsync();
    return Results.Json(user);
});
app.Run();

