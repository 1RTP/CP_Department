using FastReport.Blazor.Demo.Data;
using FastReport.Blazor.Demo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddFastReport(options =>
{
    options.CacheOptions.UseLegacyWebReportCache = false;
    options.CacheOptions.UseCircuitScope = true;
});
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IReportFileProvider, ReportFileProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UsePathBase("/blazor");

app.UseStaticFiles();

app.UseRouting();

LocalizationConfig.SetCulture(app.Configuration);

app.UseFastReport();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
