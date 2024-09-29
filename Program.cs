namespace ResturangFrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            var app = builder.Build();

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

            // Configure routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Routes for CRUD operations
            app.MapControllerRoute(
                name: "bookings",
                pattern: "api/Bookings/{action=Index}/{id?}",
                defaults: new { controller = "Bookings" });

            app.MapControllerRoute(
                name: "customers",
                pattern: "api/Customers/{action=Index}/{id?}",
                defaults: new { controller = "Customers" });

            app.MapControllerRoute(
                name: "tables",
                pattern: "api/Tables/{action=Index}/{id?}",
                defaults: new { controller = "Tables" });

            app.MapControllerRoute(
                name: "menus",
                pattern: "api/Menus/{action=Index}/{id?}",
                defaults: new { controller = "Menus" });

            app.MapControllerRoute(
                name: "menuitems",
                pattern: "MenuItem/{action=Index}/{id?}",
                defaults: new { controller = "MenuItems" });

            app.Run();
        }
    }
}
