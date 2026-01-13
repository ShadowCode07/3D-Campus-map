
using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Mappers;
using CampusMapAPI.Repositories;
using CampusMapAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                options.EnableSensitiveDataLogging(true);
            });

            builder.Services.RegisterMapsterConfiguration();

            builder.Services.AddScoped<IHotspotRepository, HotsportRepository>();
            builder.Services.AddScoped<ISceneRepository, SceneRepository>();
            builder.Services.AddScoped<IMediaRepository, MediaRepository>();

            builder.Services.AddScoped<IHotspotService, HotspotService>();
            builder.Services.AddScoped<ISceneService, SceneService>();

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
