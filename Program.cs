using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cinema4.Data;
namespace Cinema4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Cinema4Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local") ?? throw new InvalidOperationException("Connection string 'Cinema4Context' not found.")));
            // "Azure" "Azure2"
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<Cinema4Context>();
                //context.Database.EnsureCreated();
                DbInitializer.Initialize(context);//можна одразу їх наповнити наперед заготовленим змістом
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
