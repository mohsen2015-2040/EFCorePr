//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BookStoreEFCoreContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IGenerateGuideToRoutsService, GenerateGuideToRoutsService>();

builder.Services.AddSingleton<ExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
