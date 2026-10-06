using Esb.Contracts;
using MassTransit;
using Npgsql;

public class DocumentConsumer : IConsumer<DocumentCreated>
{
    private const string Dsn = "Host=localhost;Username=esb;Password=esb;Database=esb";

    public async Task Consume(ConsumeContext<DocumentCreated> context)
    {
        var msg = context.Message;
        Console.WriteLine($"[RX] {msg.Number} ({msg.DocumentType})");

        await using var conn = new NpgsqlConnection(Dsn);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(
            "INSERT INTO processed (id, number, kind, ts) VALUES (@id,@n,@k,NOW())", conn);
        cmd.Parameters.AddWithValue("id", msg.Id);
        cmd.Parameters.AddWithValue("n", msg.Number);
        cmd.Parameters.AddWithValue("k", msg.DocumentType);
        await cmd.ExecuteNonQueryAsync();

        if (msg.Number.EndsWith("3"))
            throw new InvalidOperationException("Симуляция сбоя");

        await context.Publish(new DocumentProcessed(
            msg.Id, msg.Number, true, null, DateTime.UtcNow));
    }
}
