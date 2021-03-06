using ArticleService.Infrastructure;
using Common.Commons;
using CommonInitializer;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
// Add services to the container.
//builder.ConfigureDbConfiguration<ArticleDbContext>();
//builder.Services.AddDbContext<ArticleDbContext>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer")));
builder.Services.AddDbContext<ArticleDbContext>(option => option.UseSqlServer(Environment.GetEnvironmentVariable("DefaultDB:ConnStr") ?? builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer")));
//builder.Services.AddDbContext<ArticleDbContext>(option=>option.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Database=Blog;Trusted_Connection=True"));
builder.ConifgureExtraService(new InitializerOptions
{
    EventBusQueueName = "ArticleService"
});

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
