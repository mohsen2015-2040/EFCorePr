//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation;
using EFCorePr.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using EFCorePr.ViewModels.Customer.Create;
using EFCorePr.ViewModels.Book.Create;
using EFCorePr.ViewModels.Publisher.Create;
using EFCorePr.ViewModels.Customer.Update;
using EFCorePr.ViewModels.Publisher.Update;
using EFCorePr.ViewModels.Book.Update;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EFCorePr.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddDbContext<BookStoreContext>(x =>
x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

var jwtSettings = builder.Configuration.GetSection("JWTSettings");

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = jwtSettings["Audience"],
        ValidIssuer = jwtSettings["Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/auth/login";
        options.SlidingExpiration = true;
    });

builder.Services.AddScoped<ExceptionHandler>();

builder.Services.AddScoped<LogActionActivity>();

builder.Services.AddScoped<JWTTokenGenerator>();

builder.Services.AddScoped<IValidator<CreateCustomerViewModel>, CreateCustomerValidator>();
builder.Services.AddScoped<IValidator<CreateBookViewModel>, CreateBookValidator>();
builder.Services.AddScoped<IValidator<CreatePublisherViewModel>, CreatePublisherValidator>();
builder.Services.AddScoped<IValidator<UpdateCustomerViewModel>, UpdateCustomerValidator>();
builder.Services.AddScoped<IValidator<UpdatePublisherViewmodel>, UpdatePublisherValidator>();
builder.Services.AddScoped<IValidator<UpdateBookViewModel>, UpdateBookValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
