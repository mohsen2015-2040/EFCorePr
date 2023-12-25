//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation;
using EFCorePr.Validations;
using FluentValidation.AspNetCore;
using EFCorePr.ViewModels.Book;
using EFCorePr.ViewModels.Customer;
using EFCorePr.ViewModels.Publisher;
using EFCorePr.ViewModels.Rent;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFastEndpoints();

builder.Services.AddDbContext<BookStoreEFCoreContext>(x =>
x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IGenerateGuideToRoutsService, GenerateGuideToRoutsService>();

builder.Services.AddScoped<ExceptionHandler>();

builder.Services.AddScoped<LogActionActivity>();

builder.Services.AddScoped<IValidator<CustomerViewData>, UserValidator>();
builder.Services.AddScoped<IValidator<BookViewData>, BookValidator>();
builder.Services.AddScoped<IValidator<PublisherViewData>, PublisherValidator>();
builder.Services.AddScoped<IValidator<RentViewData>, RentValidator>();

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

app.UseFastEndpoints();

app.UseHsts();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
