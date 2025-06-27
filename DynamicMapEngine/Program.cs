using DynamicMapEngine.Handler;
using DynamicMapEngine.Interfaces;
using DynamicMapEngine.Middleware;
using DynamicMapEngine.Processor;
using Mapper;
using Mapper.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMapHandler, MapHandler>();
builder.Services.AddScoped<IMapProcessor, MapProcessor>();
builder.Services.AddSingleton<IMapperFactory, MapperFactory>();
builder.Services.AddSingleton<IModelTypeResolver, ModelTypeResolver>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
