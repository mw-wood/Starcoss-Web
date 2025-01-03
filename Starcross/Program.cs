using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starcross.Data;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUrl = Environment.GetEnvironmentVariable("KEYVAULT_URL");

if (string.IsNullOrEmpty(keyVaultUrl))
{
    throw new InvalidOperationException("Key Vault URL is not configured in environment variables.");
}

// Create a SecretClient to access Key Vault
var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

// Retrieve the connection string from Key Vault (replace with your secret name)
//KeyVaultSecret secret = client.GetSecret("connString");
//string connectionString = secret.Value;
string connectionString = "Server=tcp:starcross.database.windows.net,1433;Initial Catalog=starcross;Persist Security Info=False;User ID=StarcrossAdmin;Password=GDfkABX5j5B9wuZ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

// Register DbContext with the SQL Server connection string retrieved from Key Vault
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add other services to the container, e.g., MVC controllers
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
