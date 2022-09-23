using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Common.Config;
using DAL.SqlSugarOrm;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
#region ≈‰÷√∂¡»°


builder.Configuration.GetSection("Appsettings").Get<ConnectionStrings>();

#endregion


#region ≈‰÷√øÁ”Ú

builder.WebHost.UseKestrel(options =>
{
    ////øÁ”Ú
    //options.AddServerHeader = false;

    //http
    options.Listen(IPAddress.Any, 9527);
});

//builder.Services.AddCors(cor =>
//{
//    var cors = ConnectionStrings.CorsUrls.ToArray();
//    cor.AddPolicy("Cors", policy =>
//    {
//        policy.WithOrigins(cors.ToArray())
//        .AllowAnyHeader()
//        .AllowAnyMethod()
//        .AllowCredentials();
//    });
//});

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.WebHost.UseUrls("http://*:9527");

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//app.UseCors("Cors");
app.UseAuthorization();
app.MapControllers();
SyncStructureSevice.SyncTable();
app.Run();

