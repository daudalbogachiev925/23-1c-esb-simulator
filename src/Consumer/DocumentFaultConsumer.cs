using Esb.Contracts;
using MassTransit;

public class DocumentFaultConsumer : IConsumer<Fault<DocumentCreated>>
{
    public async Task Consume(ConsumeContext<Fault<DocumentCreated>> context)
    {
        var original = context.Message.Message;
        var reason = string.Join(" | ",
            context.Message.Exceptions.Select(e => e.Message));

        Console.WriteLine($"[DLQ] {original.Number}: {reason}");

        await context.Publish(new DocumentFailed(
            original.Id, original.Number, reason, DateTime.UtcNow));
    }
}
