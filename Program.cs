using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ScheduleWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddDbContext<EventDb>(opt => opt.UseInMemoryDatabase("db"))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new() { Title = "Schedule API", Version = "v1" });
        opt.EnableAnnotations();
        opt.SupportNonNullableReferenceTypes();
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
