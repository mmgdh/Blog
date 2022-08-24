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
//��¼�ļ��ϴ�API�ĵ�ַ
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

#region ��Ѷ�ƴ洢Cos����
//��ʼ�� CosXmlConfig 
string appid = "1302438275";//������Ѷ���˻����˻���ʶ APPID
string region = "ap-nanjing"; //����һ��Ĭ�ϵĴ洢Ͱ����
CosXmlConfig config = new CosXmlConfig.Builder()
  .IsHttps(true)  //����Ĭ�� HTTPS ����
  .SetRegion(region)  //����һ��Ĭ�ϵĴ洢Ͱ����
  .SetDebugLog(true)  //��ʾ��־
  .Build();  //���� CosXmlConfig ����
string secretId = Environment.GetEnvironmentVariable("TencentCos:secretId")!; //"�� API ��Կ SecretId";
string secretKey = Environment.GetEnvironmentVariable("TencentCos:secretKey")!; //"�� API ��Կ SecretKey";
long durationSecond = 600;  //ÿ������ǩ����Чʱ������λΪ��
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
