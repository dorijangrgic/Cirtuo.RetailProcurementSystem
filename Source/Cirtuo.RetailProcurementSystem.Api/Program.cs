using Cirtuo.RetailProcurementSystem.Api;
using Cirtuo.RetailProcurementSystem.Application;
using Cirtuo.RetailProcurementSystem.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRestApi()
    .AddApplicationLayer()
    .AddPersistenceLayer(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

app.Run();