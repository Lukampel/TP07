var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSession(); 


builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseSession();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
