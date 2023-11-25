using Lab3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async (User loginData,ModelDB db) =>
{
    User? person =await db.Users.FirstOrDefaultAsync(p => p.Email == loginData.Email && p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email!) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encodedJwt,
        username = person.Email
    };

    return Results.Json(response);
});
app.MapGet("api/pricelist", [Authorize] async (ModelDB db) => await db.PriceList.ToListAsync());
app.MapGet( "api/pricelist/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    PriceList? priceList = await db.PriceList.FirstOrDefaultAsync(x => x.Id == id);
    if (priceList == null) return Results.NotFound(new { message = "PriceList not found" });
    return Results.Json(priceList);
});
app.MapGet("api/sales", [Authorize] async (ModelDB db) => await db.Sales.ToListAsync());
app.MapGet("api/sales/select/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Sales? sales = await db.Sales.FirstOrDefaultAsync(x => x.Id == id);
    if (sales == null) return Results.NotFound(new { message = "Sales not found" });
    return Results.Json(sales);
});
app.MapGet("api/sales/select/{data}", [Authorize] async (string data, ModelDB db) =>
{
    List<Sales> sales = await db.Sales.Where(x => x.DateSale == DateTime.Parse(data)).ToListAsync();
    return sales;
});
app.MapPost("/api/sales", [Authorize] async (Sales sale, ModelDB db) =>
{
    await db.Sales.AddAsync(sale);
    await db.SaveChangesAsync();
    return sale;
});
app.MapPost("/api/pricelist", [Authorize] async (PriceList price, ModelDB db) =>
{
    await db.PriceList.AddAsync(price);
    await db.SaveChangesAsync();
    return price;
});
app.MapDelete("api/sale/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Sales? sale = await db.Sales.FirstOrDefaultAsync(x => x.Id == id);
    if (sale == null) return Results.NotFound(new { message = "Sale not found" });
    db.Sales.Remove(sale);
    await db.SaveChangesAsync();
    return Results.Json(sale);
});
app.MapDelete("api/pricelist/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    PriceList? price = await db.PriceList.FirstOrDefaultAsync(x => x.Id == id);
    if (price == null) return Results.NotFound(new { message = "User not found" });
    db.PriceList.Remove(price);
    await db.SaveChangesAsync();
    return Results.Json(price);
});
app.MapPut("/api/sale", [Authorize] async (Sales saleData, ModelDB db) =>
{
    Sales? sale = await db.Sales.FirstOrDefaultAsync(u => u.Id == saleData.Id);
    if (sale == null) return Results.NotFound(new { message = "Sale not found" });
    sale.FIO = saleData.FIO;
    sale.TotalPrice = saleData.TotalPrice;
    sale.DateSale = saleData.DateSale;
    sale.Discount = saleData.Discount;
    sale.Complect = saleData.Complect;
    await db.SaveChangesAsync();
    return Results.Json(sale);
});
app.MapPut("/api/pricelist", [Authorize] async (PriceList priceData, ModelDB db) =>
{
    PriceList? price = await db.PriceList.FirstOrDefaultAsync(u => u.Id == priceData.Id);
    if (price == null) return Results.NotFound(new { message = "Price not found" });
    price.Marka = priceData.Marka;
    price.Price = priceData.Price;
    price.Complect= priceData.Complect;
    await db.SaveChangesAsync();
    return Results.Json(price);
});
app.Run();