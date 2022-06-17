using System.Text;
using DbaitArgue.Contexts;
using DbaitArgue.Queries;
using DbaitArgue.Services;
using DbaitArgue.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// EF
string host = "";
string database = "";
string username = "";
string password = "";

System.Console.WriteLine($"Starting in {builder.Environment.EnvironmentName}.");
if (builder.Environment.IsDevelopment())
{
    username = builder.Configuration.GetConnectionString("Username");
    password = builder.Configuration.GetConnectionString("Password");
    host = builder.Configuration.GetConnectionString("Host");
    database = builder.Configuration.GetConnectionString("Database");
}
else if (builder.Environment.IsProduction())
{
    username = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "";
    password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
    host = Environment.GetEnvironmentVariable("DB_HOST") ?? "";
    database = Environment.GetEnvironmentVariable("DB_NAME") ?? "";
}

var connectionString = $"Host={host};Database={database};Username={username};Password={password}";
System.Console.WriteLine(connectionString);
builder.Services
    .AddPooledDbContextFactory<DbaitDbContext>(o =>
        o.UseNpgsql(connectionString));

// Auth
var signingKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "")
);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = signingKey
            };
    });

builder.Services.AddAuthorization();

// Services
builder.Services
    .AddTransient<OpinionService>()
    .AddTransient<AuthorService>()
    .AddTransient<ResponseService>()
    .AddTransient<UserService>()
    .AddTransient<AuthService>();

// Utils
builder.Services.AddSingleton<IJwtUtils, JwtUtils>();

// Gql
builder.Services
    .AddGraphQLServer()
    .AllowIntrospection(builder.Environment.IsDevelopment())
    .AddAuthorization()
    .RegisterDbContext<DbaitDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddFiltering()
    .AddProjections();

// Build & run
var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();