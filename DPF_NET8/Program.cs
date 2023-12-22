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
    //s.SetRedactor<ErasingRedactor>(new DataClassificationSet(DataTaxonomy.SensitiveData));

    s.SetRedactor<StarRedactor>(new DataClassificationSet(DataTaxonomy.SensitiveData));
    
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


app.MapPost("/customer", (Customer model, ILogger<Program> logger) =>
{

    //logger.LogInformation("Customer Created {model}", model);
    logger.LogCustomerCreated(model);
    return model;
});

app.Run();

