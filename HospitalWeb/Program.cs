using HospitalWeb.Components;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using HospitalData.Services;
using HospitalData.DTOs;
using MudBlazor.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();

// Modulo Base de Datos
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HospitalDB"))
);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var mongoConn = config.GetConnectionString("MongoConnection");
    return new MongoClient(mongoConn);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var client = sp.GetRequiredService<IMongoClient>();    
    var dbName = config["MongoDbSettings:DatabaseName"] ?? "HospitalLabDB";
    return client.GetDatabase(dbName);
});

builder.Services.AddMudServices();


// Modulo Autenticacion
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<UserSessionService>();


//Modulo de Servicios de Dominio
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(HospitalData.Models.HospitalDbContext).Assembly));


//Modulo de Servicios Transversales
builder.Services.AddScoped<IAppointmentManagementService, AppointmentManagementService>();

builder.Services.AddScoped<IPrescriptionManagementService, PrescriptionManagementService>();

builder.Services.AddScoped<IUserAccountService, UserAccountService>();

builder.Services.AddScoped<HospitalData.Factories.IUserEntityFactory, HospitalData.Factories.UserEntityFactory>();

builder.Services.AddTransient<HospitalData.Builders.ILabResultBuilder, HospitalData.Builders.LabResultBuilder>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); 

app.Run();
