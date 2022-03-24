using Ans.Net6.Web;
using Ans.Net6.Web.Services;
using NLog;
using NLog.Web;

var logger = NLog.LogManager
	.Setup()
	.LoadConfigurationFromAppSettings()
	.GetCurrentClassLogger();
logger.Debug("Program Init");

try
{

	// builder
	var builder = WebApplication.CreateBuilder(args);

	builder.Logging.ClearProviders();
	builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	builder.Host.UseNLog();

	builder.Services.AddAnsNet6Web();
	builder.Services.AddSingleton<INavProviderService, JsonNavProviderService>();

	builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
	builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

	// app
	var app = builder.Build();
	if (app.Environment.IsDevelopment())
	{
		//app.UseAnsNet6WebErrors();
		app.UseDeveloperExceptionPage();
		app.UseStatusCodePages();
	}
	else
	{
		app.UseAnsNet6WebErrors();
		app.UseHsts();
	}
	app.UseHttpsRedirection();
	app.UseStaticFiles();
	app.UseRouting();
	app.UseEndpoints(o =>
	{
		o.MapControllers();
		o.MapRazorPages();
		o.MapControllerRoute("Nodes", "{*path}",
			new { controller = "Nodes", action = "Index", path = "start" });
	});
	app.Run();

}
catch (Exception exception)
{
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally { NLog.LogManager.Shutdown(); }