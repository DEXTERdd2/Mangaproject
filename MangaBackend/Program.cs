using MangaBackend.Domain.Interfaces.ITb_UserService;
using MangaBackend.Infrastructure.Auth.JWT;
using MangaBackend.Infrastructure.Data;
using MangaBackend_Infrastructure.Auth.JWT;
//using MangaBackend_Infrastructure.Services.Tb_UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Driver;
using MangaBackend_Infrastructure.MongoConfrguration;
using MangaBackend.Infrastructure.Services.Tb_UserServices;
using MangaBackend.Domain.Interfaces.IAuthService;
using MangaBackend.Infrastructure.Services;
using MangaBackend_Infrastructure.CorsConfiguration;
using MangaBackend.Domain.Interfaces.ITb_CommentService;
using MangaBackend_Infrastructure.Services.Tb_CommentService;
using MangaBackend.Domain.Interfaces.IMangaService;
using MangaBackend_Infrastructure.Services.MangaService;

var builder = WebApplication.CreateBuilder(args);

// ? MUST be added for controllers to work
builder.Services.AddControllers();

// ? Swagger setup (if needed)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Manga API", Version = "v1" });

    //  Define JWT Bearer scheme
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs...",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    //  Require JWT token in all endpoints (unless [AllowAnonymous])
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "bearer",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new string[] {}
        }
    });
});


//// ? Database
//builder.Services.AddDbContext<MangaBackendDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ? Mongodb setup
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoDbSettings")
        .Get<MongoDbSettings>();

    return new MongoClient(settings.ConnectionString);
});

// ? JWT Settings
var jwtSettingsSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JWTSettings>(jwtSettingsSection);

var secretKey = jwtSettingsSection["SecretKey"];
if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 16)
{
    throw new ArgumentException("Secret key is invalid. Please ensure it is at least 16 characters long.");
}

// ? JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddCorsPolicies();


// Register MongoDB access layer
builder.Services.AddScoped<MongoUserAccess>();

// Register service layer
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IMangaService, MangaService>();
builder.Services.AddSingleton<MongoConnectionTest>();

var app = builder.Build();

// ? Middleware
//app.UseCors("defaultPolicy");
app.UseCors("AllowSpecificOrigin");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html", permanent: false);
    return Task.CompletedTask;
});
app.Run();
