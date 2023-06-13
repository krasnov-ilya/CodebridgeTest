using CodebridgeTest.Domain.Options;
using CodebridgeTest.Infrastructure.IoC;
using CodebridgeTest.Persistence.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<ServiceInfo>()
    .BindConfiguration(nameof(ServiceInfo))
    .ValidateOnStart();

builder.Services.RegisterPersistence()
    .RegisterInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();