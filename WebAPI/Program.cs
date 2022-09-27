using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Common.Config;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.DAL;
using Service;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    {
        var assmbly = Assembly.GetAssembly(typeof(SqlSugarDAL));
        var assmbly2 = Assembly.GetAssembly(typeof(BaseService));
        builder.RegisterAssemblyTypes(assmbly2).Where(t => t.Name.EndsWith("Service")).AsSelf().InstancePerDependency();
    });
}
#region 配置读取


builder.Configuration.GetSection("Appsettings").Get<ConnectionStrings>();

#endregion


#region 配置跨域

//builder.WebHost.UseKestrel(options =>
//{
//    ////跨域
//    //options.AddServerHeader = false;

//    //http
//    options.Listen(IPAddress.Any, 9527);
//});

//允许一个或多个来源可以跨域
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("Cors", policy =>
//    {
//        // 设定允许跨域的来源，有多个可以用','隔开
//        policy.WithOrigins("http://localhost:21632")
//        .AllowAnyHeader()
//        .AllowAnyMethod()
//        .AllowCredentials();
//    });
//});

builder.Services.AddCors(cor =>
{
    var cors = ConnectionStrings.CorsUrls.ToArray();
    cor.AddPolicy("Cors", policy =>
    {
        policy.WithOrigins(cors.ToArray())
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
    //配置ip一种方法
    //builder.WebHost.UseUrls("http://*:9527");

});

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//设置跨域需要注释当前下面代码
//app.UseHttpsRedirection();
app.UseCors("Cors");
app.UseAuthorization();
app.MapControllers();
SyncStructureSevice.FreeSqlSyncTable();
app.Run("http://*:9527");

