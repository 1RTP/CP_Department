using AspNet.Demo.Data;
using AspNet.Demo.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddViewLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddFastReport(options => { options.CacheOptions.UseLegacyWebReportCache = false; });

builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IReportFileProvider, ReportFileProvider>();
builder.Services.AddTransient<IWebReportInfoService, WebReportInfoService>();

var app = builder.Build();

LocalizationConfig.SetCulture(app.Configuration);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UsePathBase("/net-core");

app.UseStaticFiles();

app.UseRouting();

app.UseFastReport();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();