using BAGeocoding.Api;
using BAGeocoding.Api.Extensions;
using BAGeocoding.Api.Models;
using Elasticsearch.Net;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<GeoDatabaseSettings>(
    builder.Configuration.GetSection("GeoDatabaseSettings"));

var elasticSettings = new ElasticSettings();
builder.Configuration.GetSection("Elastic").Bind(elasticSettings);

//var urls = new Urls();
string uri = elasticSettings.EsEndPoint;
//var pool = new SingleNodeConnectionPool(new Uri("http://10.0.10.146:9200"));
//var pool = new SingleNodeConnectionPool(new Uri("http://10.0.10.133:9200"));
var pool = new SingleNodeConnectionPool(new Uri(uri));
var settings = new ConnectionSettings(pool);

var elasticClient = new ElasticClient(settings.RequestTimeout(TimeSpan.FromSeconds(10)).PingTimeout(TimeSpan.FromSeconds(10)));

// Add services to the container.
builder.Services.AddSingleton(elasticClient);

builder.Services.RegisterService();

builder.Services.AddHostedService<BackgroundWorkerService>();

// Add services to the container.
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());// dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

// Add Core 
builder.Services.AddCore();

var app = builder.Build();
if (app.Environment.IsProduction())
{
    app.Urls.Add("http://10.0.10.133:5001");// Ubuntu publish
    //app.Urls.Add("http://10.0.10.146:5002");// Ubuntu publish
}

app.UseStaticFiles();

app.UseAuthorization();

app.UseRouting();

// Core
app.UseSwaggerWithVersioning();

app.MapControllers();

app.Run();
