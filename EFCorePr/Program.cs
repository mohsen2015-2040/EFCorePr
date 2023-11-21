//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BookStoreEFCoreContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IGenerateGuideToRoutsService, GenerateGuideToRoutsService>();

builder.Services.AddScoped<ExceptionHandler>();

builder.Services.AddScoped<LogActionActivity>();

var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(exceptinoHandlerApp =>
    exceptinoHandlerApp.Run(async context =>
    {
        context.Response.ContentType = Text.Plain;

        var exceptionDetector = context.Features.GetRequiredFeature<IExceptionDetector>();

        await context.Response.WriteAsync("An error occured!");
    }
    ));
}

app.UseHsts();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
