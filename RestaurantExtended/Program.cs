using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantExtended.Data;
using Microsoft.AspNetCore.Identity;
using RestaurantExtended.Models;
using RestaurantExtended.Services;
using RestaurantExtended.Services.implementations;
using RestaurantExtended.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestaurantExtended.Repositories.implementation;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using ServiceStack;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context2Connection") ?? throw new InvalidOperationException("Connection string 'RestaurantExtendedContext' not found.")));

builder.Services.AddDbContext<Context2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context2Connection") ?? throw new InvalidOperationException("Connection string 'RestaurantExtendedContext' not found.")));




builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<Context2>()
    .AddDefaultTokenProviders();






builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.

    options.Password.RequiredLength = 6;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

   // options.LoginPath = new PathString("/Login");
   // options.LogoutPath = new PathString("/Logout");
    //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Add services to the container.

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IComenziService, ComenziService>();
builder.Services.AddTransient<IComenziService, ComenziService>();


builder.Services.AddTransient<IComenziRepository, ComenziRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IProdusRepository, ProdusRepository>();


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();




builder.Services.AddRazorPages();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(
    opt =>
    {
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).
    AddCookie()
    .AddJwtBearer(options =>
    {


        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidIssuer = "https://localhost:7094",
            ValidateIssuer = true,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Alexandru_site12345678910111213141516117181920212223242526272829303132")),
            TokenDecryptionKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Alexandru_site12345678910111213141516117181920212223242526272829303132")),
            RequireSignedTokens = true, // False because I'm encrypting the token instead
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true

        };
    });
    


builder.Services.AddAuthorization(
    options=>
    {
        options.AddPolicy("OneRolePolicy",p=>
        {

            p.RequireClaim(ClaimTypes.Role,"Admin");


        }
        
  
        );

    }
    
    
    
    
    );




var app = builder.Build();


app.UseSession();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseRouting();




app.UseHttpsRedirection();



var RoleManager=app.Services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await RoleManager.CreateAsync(new IdentityRole("Admin"));




app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();




app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});




app.Run();







    