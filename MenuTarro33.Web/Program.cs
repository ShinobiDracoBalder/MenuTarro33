using MenuTarro33.Common.Application.Repositories;
using MenuTarro33.Common.DataBase;
using MenuTarro33.Common.SExplMappers;
using MenuTarro33.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Vereyon.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
// Add services to the container.
builder.Services.AddControllersWithViews()
     .AddRazorRuntimeCompilation()
     .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(SpExplorationMapper)); ;
builder.Services.AddFlashMessage();
builder.Services.AddScoped<IImageVideoHelper, ImageVideoHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
