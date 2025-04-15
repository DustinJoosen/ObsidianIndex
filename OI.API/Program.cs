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


builder.Services.Configure<MediaConfig>(
    builder.Configuration.GetSection("MediaConfig"));


builder.Services.AddScoped<IMediaService, MediaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
