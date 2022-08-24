using RESTfulAPI.Logging;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                            .WriteTo.File("log.VillaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerillog();
// Add services to the container.

builder.Services.AddControllers(option => { option.ReturnHttpNotAcceptable = true; })
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, LoggingV2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
