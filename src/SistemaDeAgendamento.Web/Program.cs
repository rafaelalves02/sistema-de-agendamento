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
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// Configuração do banco de dados
// DatabaseProvider pode estar no appsettings.json (não é informação sensível)
var databaseProvider = builder.Configuration["DatabaseProvider"]?.ToLower() ?? "mysql";

// Connection strings devem ser configuradas via variáveis de ambiente por segurança
var mysqlConnectionString = Environment.GetEnvironmentVariable("SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING");
var sqlServerConnectionString = Environment.GetEnvironmentVariable("SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING");

// Registrar repositórios baseado no DatabaseProvider
if (databaseProvider == "sqlserver")
{
    if (string.IsNullOrEmpty(sqlServerConnectionString))
    {
        throw new InvalidOperationException(
            "ConnectionString para SQL Server não configurada. " +
            "Configure a variável de ambiente 'SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING'");
    }

    builder.Services.AddScoped<IClientRepository, ClientRepositorySqlServer>(c => new ClientRepositorySqlServer(sqlServerConnectionString));
    builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepositorySqlServer>(c => new AvailabilityRepositorySqlServer(sqlServerConnectionString));
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositorySqlServer>(c => new EmployeeRepositorySqlServer(sqlServerConnectionString));
    builder.Services.AddScoped<IUserRepository, UserRepositorySqlServer>(c => new UserRepositorySqlServer(sqlServerConnectionString));
    builder.Services.AddScoped<IServiceRepository, ServiceRepositorySqlServer>(c => new ServiceRepositorySqlServer(sqlServerConnectionString));
    builder.Services.AddScoped<IAppointmentRepository, AppointmentRepositorySqlServer>(c => new AppointmentRepositorySqlServer(sqlServerConnectionString));
}
else // mysql (padrão)
{
    if (string.IsNullOrEmpty(mysqlConnectionString))
    {
        throw new InvalidOperationException(
            "ConnectionString para MySQL não configurada. " +
            "Configure a variável de ambiente 'SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING'");
    }

    builder.Services.AddScoped<IClientRepository, ClientRepository>(c => new ClientRepository(mysqlConnectionString));
    builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>(c => new AvailabilityRepository(mysqlConnectionString));
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(c => new EmployeeRepository(mysqlConnectionString));
    builder.Services.AddScoped<IUserRepository, UserRepository>(c => new UserRepository(mysqlConnectionString));
    builder.Services.AddScoped<IServiceRepository, ServiceRepository>(c => new ServiceRepository(mysqlConnectionString));
    builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>(c => new AppointmentRepository(mysqlConnectionString));
}
    

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
