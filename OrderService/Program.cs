
using Microsoft.EntityFrameworkCore;
using OrderService.Client;
using OrderService.Clients;
using OrderService.Data;
using OrderService.Interfaces;
using OrderService.Repository;
using System.Text.Json.Serialization;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddJsonOptions(optins => optins.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHttpClient<IProductClient, ProductClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5002/"); // Product Service
            });
            builder.Services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5004/"); // Product Service
            });
            builder.Services.AddHttpClient<IInventoryClient, InventoryClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5003/"); // Inventory Service
            });

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
