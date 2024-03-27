using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsSystem_ASP.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

            builder.Services.AddHttpClient<TmdbApiService>(client =>
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
               
            });

            builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

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
        }
    }
}