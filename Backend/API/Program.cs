global using Infrastructure.Entities.Sessions;


using API;
using API.Middlewares;
using API.Swagger;
using Infrastructure;
using Persistence;
using Services;
using API.Utils;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithConfig(builder.Environment.EnvironmentName);


// Add layers
builder.Services
    .AddPersistenceLayer(builder.Configuration)
    .AddInfrastructureLayer()
    .AddServicesLayer();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.InDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}


//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
//    RequestPath = new PathString("/Images")
//});



app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<Authentication>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();