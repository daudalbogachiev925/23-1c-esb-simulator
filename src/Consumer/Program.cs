using Esb.Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<DocumentConsumer>();

        x.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("esb");
                h.Password("esb");
            });

            cfg.ReceiveEndpoint(Queues.Documents, e =>
            {
                e.UseMessageRetry(r => r.Exponential(5,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(30),
                    TimeSpan.FromSeconds(5)));

                e.UseDelayedRedelivery(r => r.Intervals(
                    TimeSpan.FromMinutes(1),
                    TimeSpan.FromMinutes(5),
                    TimeSpan.FromMinutes(15)));

                e.ConfigureConsumer<DocumentConsumer>(ctx);
            });
        });
    });
});

var host = builder.Build();
await host.RunAsync();
