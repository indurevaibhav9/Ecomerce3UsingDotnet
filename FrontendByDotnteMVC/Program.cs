namespace FrontendByDotnteMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = new Uri("http://localhost:5000/"));
            builder.Services.AddSession();
            builder.Services.AddScoped<Services.ProductService>();
            builder.Services.AddScoped<Services.CategoryService>();
            builder.Services.AddScoped<Services.CustomerService>();
            builder.Services.AddScoped<Services.OrderService>();
            builder.Services.AddScoped<Services.AuthService>();
            builder.Services.AddScoped<Services.InventoryService>();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddEventSourceLogger();
            var app = builder.Build();
            app.UseSession();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
