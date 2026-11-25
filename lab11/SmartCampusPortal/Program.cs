using SmartCampusPortal.DAL;

var builder = WebApplication.CreateBuilder(args);

// MVC (Controllers + Views) and API controllers
builder.Services.AddControllersWithViews();

// Data Access Layer (ADO.NET) registered for dependency injection
builder.Services.AddScoped<ServiceRequestDAL>();

var app = builder.Build();

// Create database + table + sample rows on first run
DatabaseInitializer.Initialize(app.Configuration.GetConnectionString("DefaultConnection"));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ServiceRequest/Index");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// MVC route (API controller uses attribute routing: api/ServiceApi)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ServiceRequest}/{action=Index}/{id?}");

app.Run();
