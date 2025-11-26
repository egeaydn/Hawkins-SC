using Hawkins.DataAccess.Context;
using Hawkins_SC.Concrate;
using Hawkins_SC_DataAccess.UnitOfWork;
using Hawkins_SC_WebUI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using FluentValidation;
using Hawkins_SC_Bussines.Mapping;
using Hawkins_SC_Bussines.Validators;
using Hawkins_SC_Bussines.Extension; // AddBusiness extension burada tanýmlýysa

var builder = WebApplication.CreateBuilder(args);

// Configuration: connection string appsettings.json içinde "HawkinsDb" olarak tanýmlý olmalý
var connectionString = builder.Configuration.GetConnectionString("HawkinsSC")
					   ?? @"Server=DESKTOP-L027AII\SQLEXPRESS;Database=HawkinsSC;User Id=sa;Password=1;TrustServerCertificate=True";

// DbContext
builder.Services.AddDbContext<HawkinsDbContext>(options =>
	options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("Hawkins-SC DataAccess")));

// Identity (eðer kullanacaksanýz)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<HawkinsDbContext>()
	.AddDefaultTokenProviders();

// Register UnitOfWork (DataAccess içinde UnitOfWork implementasyonunuz varsa)
builder.Services.AddScoped<IUnitOfWork, IUnitOfWork>();

// Business layer registrations (extension should add services like IStudentService)
builder.Services.AddBusiness();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation - tüm validator'larý tarar
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

// MVC + Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Swagger (opsiyonel, API testleri için)
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Development pipeline
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
