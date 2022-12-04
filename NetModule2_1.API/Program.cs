using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using NetModule2_1;
using NetModule2_1.API;
using NetModule2_1.DefaultBusinessLogic;
using NetModule2_1.EAL;
using NetModule2_1.LiteDb;
using NetModule2_1.RabbitMq;
using System.Reflection;

[assembly: ApiConventionType(typeof(CartServiceApiConvention))]

if (args.Contains("--data-setup"))
    Data.Setup();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule<BusinessLogicModule>();
    builder.RegisterModule<LiteDbModule>();
    builder.RegisterModule<RabbitMqModule>();
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorHandlingAttribute>();
});
builder.Services.AddApiVersioning();
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Cart service API v2",
        Version = "2.0",
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cart service API v1",
        Version = "1.0"
    });

    var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, fileName);
    options.IncludeXmlComments(xmlPath);
    options.OperationFilter<SwaggerVersinoningIntegrationFilter>();
});

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var groupName in provider.ApiVersionDescriptions.Select(d => d.GroupName).Reverse())
        {
            options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
        }
    });
}

app.Lifetime.ApplicationStarted.Register(() =>
{
    var changedItemSubscriber = app.Services.GetRequiredService<IChangedItemEventSubscriber>();
    changedItemSubscriber.Subscribe();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
