var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<EmployeeTimeSheet.Services.EmployeeService>();
builder.Services.AddSingleton<EmployeeTimeSheet.Services.TimeEntryService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TimeSheet}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=TimeSheet}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "timeEntry",
    pattern: "{controller=TimeEntry}/{action=Add}/{id?}");  

app.Run();
