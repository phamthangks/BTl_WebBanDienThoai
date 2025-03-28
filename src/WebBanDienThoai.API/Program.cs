
using BTLW_BDT.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanDienThoai.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var corsPolicy = "AllowFrontend";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicy,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7244") // URL frontend
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            // Đăng ký DbContext trong DI container
            builder.Services.AddDbContext<BtlLtwQlbdtContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BtlLtwQlbdtContext"))
                .EnableDetailedErrors());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });


            var app = builder.Build();
            app.UseCors(corsPolicy);
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
