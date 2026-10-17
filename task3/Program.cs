using Microsoft.Data.SqlClient;
using task3.Data;
using task3.Data.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ADOdotNET>();
builder.Services.AddScoped<DeviceMapper>();
builder.Services.AddScoped<ClientMapper>();
builder.Services.AddScoped<PhoneNumberMapper>();
builder.Services.AddScoped<PhoneNumberMapperdto>();

builder.Services.AddScoped<SqlConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var connectionString =
        configuration.GetConnectionString("DefaultConnection");

    return new SqlConnection(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Devices}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();