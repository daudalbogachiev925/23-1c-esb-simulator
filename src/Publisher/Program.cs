using Esb.Contracts;
using MassTransit;

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("esb");
        h.Password("esb");
    });
});

await bus.StartAsync();

for (int i = 1; i <= 5; i++)
{
    var doc = new DocumentCreated(
        Guid.NewGuid(),
        "РеализацияТоваров",
        $"DOC-{i:D5}",
        DateTime.UtcNow,
        "{\"total\": 5000}");
    await bus.Publish(doc);
    Console.WriteLine($"Отправлено: {doc.Number}");
}

await bus.StopAsync();
