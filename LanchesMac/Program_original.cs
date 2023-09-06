using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac
{
    /// <summary>
    /// Essa classe seria o Program.cs original, antes de ser refatorado para criar o startup, normalmente utilizamos só essa
    /// OBS: Mudei o nome de Program para Program_Original pq só pode haver 1 classe com o nome Program
    /// </summary>
    public class Program_Original
    {
        public static void Main_Original(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddTransient<ILancheRepository, LancheRepository>();
            builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
            builder.Services.AddControllersWithViews();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}