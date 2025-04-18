using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OI.API.Data;
using OI.API.Options;
using OI.API.Services;
using OI.API.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=obsidianindex.db");
});

builder.Services.Configure<IISServerOptions>(options =>
    options.MaxRequestBodySize = 104857600);

builder.Services.Configure<KestrelServerOptions>(options =>
    options.Limits.MaxRequestBodySize = 104857600);


builder.Services.Configure<MediaConfig>(
    builder.Configuration.GetSection("MediaConfig"));


builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ILinkingService, LinkingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
