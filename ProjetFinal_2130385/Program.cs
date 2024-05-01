using Microsoft.EntityFrameworkCore;
using ProjetFinal_2130385.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DronesDatabaseContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DronesDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Drones}/{action=Index}/{id?}"
        );
});
app.MapRazorPages();

app.Run();
