using BlazorSupervision.Server.Middlewares;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Host.UseSerilog();

  // Add services to the container.

  builder.Services.AddControllersWithViews();
  builder.Services.AddRazorPages();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseWebAssemblyDebugging();
  }
  else
  {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
  }

  // For our exceptions on server side
  app.UseExceptionHandling();

  app.UseHttpsRedirection();

  app.UseBlazorFrameworkFiles();
  app.UseStaticFiles();

  app.UseRouting();


  app.MapRazorPages();
  app.MapControllers();
  app.MapFallbackToFile("index.html");

  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
  Log.CloseAndFlush();
}