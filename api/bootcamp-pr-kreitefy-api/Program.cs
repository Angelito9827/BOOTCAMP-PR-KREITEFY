using bootcamp_pr_kreitefy_api.Application.Mapping;
using bootcamp_pr_kreitefy_api.Application.Services;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.ExternalServices;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persitence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddAutoMapper(typeof(RoleMapperProfile));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(UserMapperProfile));
builder.Services.AddScoped<IStyleService, StyleService>();
builder.Services.AddScoped<IStyleRepository, StyleRepository>();
builder.Services.AddAutoMapper(typeof(StyleMapperProfile));
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddAutoMapper(typeof(AlbumMapperProfile));
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddAutoMapper(typeof(ArtistMapperProfile));
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddAutoMapper(typeof(SongMapperProfile));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddAutoMapper(typeof(HistoryMapperProfile));
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddAutoMapper(typeof(ScoreMapperProfile));

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "DefaultInMemoryDatabase";
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseInMemoryDatabase(connectionString));
}

builder.Services.AddAuthorization();

var app = builder.Build();

ConfigureExceptionHandler(app);

app.UseCors("AllowFrontEnd");

if (builder.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationContext>();
    DataLoader dataLoader = new(context);
    dataLoader.LoadData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();

static void ConfigureExceptionHandler(WebApplication app)
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            if (exceptionHandlerPathFeature?.Error != null)
            {
                logger.LogError(exceptionHandlerPathFeature.Error, "An unhandled exception ocurred while processing the request.");
            }
            else
            {
                logger.LogError("An unhadled exception ocurred whiñe processing the request.");
            }

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An error ocurred while processing your request.");
        });
    });
}
