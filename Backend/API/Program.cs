global using Infrastructure.Entities.Sessions;
global using Infrastructure.Utils;
using API;
using API.Middlewares;
using API.Swagger;
using API.Utils;
using Infrastructure;
using Persistence.SQL;
using Services;

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


builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationErrorFilter>(); // Add the custom filter here
});


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
    c.EnableFilter();
    c.EnablePersistAuthorization();
    c.EnableTryItOutByDefault();
    c.DefaultModelsExpandDepth(-1);
    //c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
});





app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<Authentication>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();