using EFCorePr.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation;
using FluentValidation.AspNetCore;
using FastEndpoints;
using EFCorePr.FastEndpoints.Book.Create;
using EFCorePr.FasteEndpoints.Book.Update;
using EFCorePr.FasteEndpoints.Publisher.Create;
using EFCorePr.FasteEndpoints.Publisher.Update;
using EFCorePr.FasteEndpoints.Rent.Create;
using EFCorePr.FasteEndpoints.Rent.Update;
using EFCorePr.FasteEndpoints.User.Create;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFastEndpoints();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<BookStoreEFCoreContext>(x =>
x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IValidator<CreateBookViewModel>, CreateBookValidator>();
builder.Services.AddScoped<IValidator<UpdateBookViewModel>, UpdateBookValidator>();

builder.Services.AddScoped<IValidator<CreatePublisherViewModel>, CreatePublisherValidator>();
builder.Services.AddScoped<IValidator<UpdatePublisherViewModel>, UpdatePublisherValidator>();

builder.Services.AddScoped<IValidator<CreateRentViewModel>, CreateRentValidator>();
builder.Services.AddScoped<IValidator<UpdateRentViewModel>, UpdateRentValidator>();

builder.Services.AddScoped<IValidator<CreateUserViewModel>, CreateUserValidator>();
builder.Services.AddScoped<IValidator<CreateUserViewModel>, CreateUserValidator>();

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

app.UseFastEndpoints().UseDefaultExceptionHandler();

app.UseHsts();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
