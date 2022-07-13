using Common.Commons;
using CommonInitializer;
using FileService.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
// Add services to the container.
//builder.ConfigureDbConfiguration<ArticleDbContext>();
builder.Services.AddDbContext<UploadDbContext>(option => option.UseSqlServer(Environment.GetEnvironmentVariable("DefaultDB:ConnStr") ?? builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer")));
builder.ConifgureExtraService(new InitializerOptions
{
    EventBusQueueName="FileService"
});
//记录文件上次API的地址
builder.Services.AddSingleton(serviceProvider =>
{
    var server = serviceProvider.GetRequiredService<IServer>();
    return server.Features.Get<IServerAddressesFeature>()!;
});
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(assemblies);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.UseDefault();

app.Run();
