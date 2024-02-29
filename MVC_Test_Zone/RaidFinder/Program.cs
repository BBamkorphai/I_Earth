var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();
<<<<<<< HEAD
=======


app.UseStaticFiles();

>>>>>>> 2fe2428906b6ef933664773b6651173750af153a
app.UseRouting();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
