using DPF_NET8;
using DPF_NET8.Helpers;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(op => op.JsonWriterOptions = new System.Text.Json.JsonWriterOptions { Indented = true });


builder.Logging.EnableRedaction();
builder.Services.AddRedaction(s =>
{
    s.SetRedactor<ErasingRedactor>(new DataClassificationSet(DataTaxonomy.SensitiveData));

  #pragma warning disable EXTEXP0002
    s.SetHmacRedactor(options =>
    {
        options.Key = Convert.ToBase64String("SecretKeyDontHardCodeInsteadStoreAndLoadSecurely"u8);
        options.KeyId=69;
    },new DataClassificationSet(DataTaxonomy.PiiData));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});


app.MapPost("/customer", (Customer model, ILogger<Program> logger) =>
{

    //logger.LogInformation("Customer Created {model}", model);
    logger.LogCustomerCreated(model);
    return model;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
