using Esb.Contracts;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("esb");
            h.Password("esb");
        });
    });
});

var app = builder.Build();

app.MapPost("/documents", async (DocumentCreated doc, IBus bus) =>
{
    await bus.Publish(doc);
    return Results.Accepted($"/documents/{doc.Id}", new { doc.Id, doc.Number });
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run("http://localhost:5000");
