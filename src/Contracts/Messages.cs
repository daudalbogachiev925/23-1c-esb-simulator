namespace Esb.Contracts;

public record DocumentCreated(
    Guid Id,
    string DocumentType,
    string Number,
    DateTime CreatedAt,
    string Payload);

public record DocumentProcessed(
    Guid Id,
    string Number,
    bool Success,
    string? Error,
    DateTime ProcessedAt);

public record DocumentFailed(
    Guid Id,
    string Number,
    string Reason,
    DateTime FailedAt);
