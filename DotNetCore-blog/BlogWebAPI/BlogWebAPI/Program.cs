using ArticleService.Infrastructure;
using Common.Commons;
using CommonInitializer;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var curAssembly = Assembly.GetExecutingAssembly();
//builder.ConfigureDbConfiguration<ArticleDbContext>();
builder.Services.AddDbContext<ArticleDbContext>(option => option.UseSqlServer(Environment.GetEnvironmentVariable("DefaultDB:ConnStr") ?? builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer")));
builder.ConifgureExtraService(new InitializerOptions
{
    EventBusQueueName = "ArticleService",
    curAssembly= curAssembly
});
//builder.Services.AddValidatorsFromAssemblyContaining<ArticleService.WebAPI.Controllers.ViewModels.RequestModel.ArticleAddRequestValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefault();

app.Run();
