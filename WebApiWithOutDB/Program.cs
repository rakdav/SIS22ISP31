using WebApiWithOutDB;

List<User> users=new List<User>()
{
    new User{Id=Guid.NewGuid().ToString(),Name="Vasia",Age=27},
    new User{Id=Guid.NewGuid().ToString(),Name="Alex",Age=22},
    new User{Id=Guid.NewGuid().ToString(),Name="Boris",Age=31}
};
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("/api/users", () => users);
app.MapGet("/api/userid/{id}", (string id) =>
{
    User? user = users.FirstOrDefault(x => x.Id == id);
    if (user == null) return Results.NotFound(new { message = "User not found" });
    return Results.Json(user);
});
app.MapGet("/api/username/{name}", (string name) =>
{
    List<User> userList = users.Where(x => x.Name == name).ToList();
    return userList;
});
app.MapDelete("/api/users/{id}", (string id) =>
{
    User? user = users.FirstOrDefault(x => x.Id == id);
    if (user == null) return Results.NotFound(new { message = "User not found" });
    users.Remove(user);
    return Results.Json(user);
});
app.MapPost("/api/users", (User user) => {
    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});
app.MapPut("/api/users", (User u) =>
{
    User? user = users.FirstOrDefault(x => x.Id == u.Id);
    if (user == null) return Results.NotFound(new { message = "User not found" });
    user.Age = u.Age;
    user.Name = u.Name;
    return Results.Json(user);
});
app.Run();

