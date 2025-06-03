using SecurityEducationApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using SecurityEducationApi.Services.Interface;
using SecurityEducationApi.Services;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Repositories;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
	.AddUserSecrets<Program>(optional: true) 
	.AddEnvironmentVariables();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
	ServiceLifetime.Scoped
);

// services
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IReadingMaterialService, ReadingMaterialService>();
builder.Services.AddScoped<IReadingMaterialRepository, ReadingMaterialRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IAnswerService, AnswerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwagger"))
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
