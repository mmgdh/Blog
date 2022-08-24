using Common.Commons;
using CommonInitializer;
using StreamService.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using COSXML;
using COSXML.Auth;

var builder = WebApplication.CreateBuilder(args);
var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
var CurAssembly = Assembly.GetExecutingAssembly();
// Add services to the container.
//builder.ConfigureDbConfiguration<ArticleDbContext>();
builder.Services.AddDbContext<UploadDbContext>(option => option.UseSqlServer(Environment.GetEnvironmentVariable("DefaultDB:ConnStr") ?? builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer")));
builder.ConifgureExtraService(new InitializerOptions
{
    EventBusQueueName="StreamService",
    curAssembly= CurAssembly
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

#region 腾讯云存储Cos配置
//初始化 CosXmlConfig 
string appid = "1302438275";//设置腾讯云账户的账户标识 APPID
string region = "ap-nanjing"; //设置一个默认的存储桶地域
CosXmlConfig config = new CosXmlConfig.Builder()
  .IsHttps(true)  //设置默认 HTTPS 请求
  .SetRegion(region)  //设置一个默认的存储桶地域
  .SetDebugLog(true)  //显示日志
  .Build();  //创建 CosXmlConfig 对象
string secretId = Environment.GetEnvironmentVariable("TencentCos:secretId")!; //"云 API 密钥 SecretId";
string secretKey = Environment.GetEnvironmentVariable("TencentCos:secretKey")!; //"云 API 密钥 SecretKey";
long durationSecond = 600;  //每次请求签名有效时长，单位为秒
QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
  secretId, secretKey, durationSecond);
CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
builder.Services.AddSingleton(config);
builder.Services.AddSingleton(cosCredentialProvider);
builder.Services.AddSingleton(cosXml);
#endregion

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

app.UseDefault();

app.MapControllers();

app.Run();
