using HelixLaserWorks.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddApplicationServices();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseExceptionHandler("/Error/InternalServerError");
}
else
{
	app.UseExceptionHandler("/Error/InternalServerError");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseErrorHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "PageNotFound",
		pattern: "pagenotfound",
		defaults: new { controller = "Error", action = "PageNotFound" });

	endpoints.MapControllerRoute(
		name: "Error",
		pattern: "error",
		defaults: new { controller = "Error", action = "InternalServerError" });
});


app.MapRazorPages();

app.Run();
