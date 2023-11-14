using Travel_Agency_Api;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var appSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = appSettings.Build();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDataBase(configuration);
builder.Services.AddAllServices();
builder.Services.AddMyAuthentication(configuration);
builder.Services.ConfigureOdata();
builder.Services.ConfigurePolicies();

var app = builder.Build();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();