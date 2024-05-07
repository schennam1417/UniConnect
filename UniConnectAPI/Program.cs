using UniConnectAPI;
using UniConnectAPI.logging;

//using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Log.Logger=new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/studentlogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();
// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers(option => { //option.ReturnHttpNotAcceptable = true; 
        })
    .AddNewtonsoftJson().AddXmlSerializerFormatters();
builder.Services.AddScoped<ILogging, Logging>();
//builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
