using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaDeAgendamento.Repositories;
using SistemaDeAgendamento.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/login";
        options.AccessDeniedPath = "/User/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(40);
    });

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var connectionString = builder.Configuration.GetConnectionString("SistemaDeAgendamentoConnectionString");

builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>(c => new AvailabilityRepository(connectionString!));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(c => new EmployeeRepository(connectionString!));
builder.Services.AddScoped<IUserRepository,UserRepository>(c => new UserRepository(connectionString!));
builder.Services.AddScoped<IServiceRepository, ServiceRepository>(c => new ServiceRepository(connectionString!));
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>(c => new AppointmentRepository(connectionString!));
    

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
