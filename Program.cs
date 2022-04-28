using Ans.Net6.Web;
using Ans.Net6.Web.Providers;
using NLog;
using NLog.Web;

var logger = NLog.LogManager
	.Setup()
	.LoadConfigurationFromAppSettings()
	.GetCurrentClassLogger();
logger.Info("OpenSiteCore Init");

try
{

	var builder = WebApplication.CreateBuilder(args);

	builder.Logging.ClearProviders();
	builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	builder.Host.UseNLog();

	builder.Services.AddAnsServices();
	builder.Services.AddSingleton<ISiteMapProvider, SiteMap_XmlProvider>();

	builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
	builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


	var app = builder.Build();

	if (app.Environment.IsDevelopment())
	{
		//app.UseDeveloperExceptionPage();
		//app.UseStatusCodePages();
		app.UseAnsErrors();
	}
	else
	{
		app.UseAnsErrors();
		//app.UseDeveloperExceptionPage();
		//app.UseStatusCodePages();
		app.UseHsts();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();
	app.UseRouting();
	app.UseEndpoints(o =>
	{
		o.MapControllers();
		o.MapRazorPages();
	});
	app.UseAnsNodes();

	app.Run();

}
catch (Exception exception)
{
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally
{
	NLog.LogManager.Shutdown();
}