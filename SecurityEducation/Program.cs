using QuestPDF.Infrastructure;
using SecurityEducation.Models;
using SecurityEducation.Services;
using SecurityEducation.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
	.AddUserSecrets<Program>() // <-- Viktigt
	.AddEnvironmentVariables(); 
QuestPDF.Settings.License = LicenseType.Community;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<IReadingMaterialService, ReadingMaterialService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IOverviewService, OverviewService>();

builder.Services.Configure<xApiSettings>(builder.Configuration.GetSection("xApi"));

builder.Services.AddHttpClient<ApiEngine>();

var app = builder.Build();

Console.WriteLine($"Current environment: {builder.Environment.EnvironmentName}");
Console.WriteLine("xApi endpoint: " + builder.Configuration["xApi:Endpoint"]);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
