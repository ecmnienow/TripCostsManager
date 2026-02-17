using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripCostsManager.Components;
using TripCostsManager.Domain.Database.DataAccess;
using TripCostsManager.Domain.Database.Interfaces;
using TripCostsManager.Domain.Database.Services;
using TripCostsManager.Models;
using TripCostsManager.Models.Validators;
using TripCostsManager.Services;

namespace TripCostsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IDataAccess, DatabaseContext>();

            builder.Services.AddSingleton<RecordsDbService>();
            builder.Services.AddSingleton<RecordsService>();
            builder.Services.AddSingleton<ItemTypesDbService>();
            builder.Services.AddSingleton<ItemTypesService>();

            builder.Services.AddTransient<IValidator<RecordModel>, RecordModelValidator>();
            builder.Services.AddTransient<IValidator<ItemTypeModel>, ItemTypeModelValidator>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
