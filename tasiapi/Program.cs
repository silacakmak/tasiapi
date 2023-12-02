using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using tasiapi.Data;
using tasiapi.Interfaces;
 using AutoMapper;
using tasiapi.Models;
using tasiapi.Dtos;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);




var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
//var key = GetSection("Appsettings:Token");

//builder.Services.AddMvc().AddJsonOptions(opt =>
//{
//    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceHandler.ReferanceLoopHandling.Ignore;
//}
//    );

builder.Services.AddScoped<IAppRepository, tasiapi.Data.AppRepository>();




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                      });
});



//// Servisleri burada tanýmlayýn
builder.Services.AddTransient<IAppRepository, tasiapi.Data.AppRepository>();
//builder.Services.AddSingleton<IAppRepository, tasiapi.Data.AppRepository>();
builder.Services.AddScoped<IAppRepository, tasiapi.Data.AppRepository>();
//builder.Services.AddScoped<IAppRepository, tasiapi.Data.AppRepository>();

builder.Services.AddControllers();



    DatabaseHelper databaseHelper = new DatabaseHelper();
    databaseHelper.TestConnection();

    builder.Services.AddDbContext<TasinmazDbContext>(options =>
    {
        // PostgreSQL baðlantý dizesini burada 
        var connectionString = "Host=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234;";

        options.UseNpgsql(connectionString, npgsqlOptions =>
        {
            // PostgreSQL için isteðe baðlý yapýlandýrmalarý burada 
            npgsqlOptions.MigrationsAssembly("tasiapi");
        });
    });

var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);
builder.Services.AddMvc().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false

    };

}
);
static void Main()

{
    string connectionString = "Host=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234";

    using (var connection = new NpgsqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            Console.WriteLine("PostgreSQL veritabanýna baþarýyla baðlandýnýz.");

            // Veritabaný iþlemlerini burada gerçekleþtirebilirsiniz
            // Örneðin, sorgularý çalýþtýrabilir veya veri alýþveriþi yapabilirsiniz

            connection.Close();
            Console.WriteLine("PostgreSQL baðlantýsý baþarýlý bir þekilde kapatýldý.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Bir hata oluþtu: " + ex.Message);
        }

    }
}







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

    }

//void ConfigureServices(IServiceCollection services)
// {
//     //var key = "gizli anahtar";
//     //var keyBytes = Encoding.ASCII.GetBytes(key);
//     //// Servisleri burada tanýmlayýn
//     services.AddTransient<IAppRepository, tasiapi.Data.AppRepository>();
//     //services.AddScoped<IMyScopedService, MyScopedService>();
//     services.AddSingleton<IAppRepository, tasiapi.Data.AppRepository>();
//     services.AddScoped<IAppRepository, tasiapi.Data.AppRepository>();
//     builder.Services.AddScoped<IAppRepository, tasiapi.Data.AppRepository>();

//     services.AddControllers();
//     //services.AddAutoMapper(typeof(Program));

// }




app.UseAuthentication();
   
    app.UseHttpsRedirection();

    app.UseAuthorization();
       app.UseCors("AllowOrigin");

app.MapControllers();
    app.Run();
    


