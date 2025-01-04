using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starcross.Data;

var builder = WebApplication.CreateBuilder(args);

// Get KeyVault
var keyVaultUrl = Environment.GetEnvironmentVariable("KEYVAULT_URL");

if (string.IsNullOrEmpty(keyVaultUrl))
{
    throw new InvalidOperationException("Key Vault URL is not configured in environment variables.");
}

// Create Client and Auth
var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

// Get DB Conn
KeyVaultSecret secret = client.GetSecret("connString");
string connectionString = secret.Value;

// Register DBContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Enable MVC
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

// Set Index as default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
