namespace Esb.Contracts;

public static class Queues
{
    public const string Documents = "esb.documents";
    public const string DocumentsRetry = "esb.documents.retry";
    public const string DocumentsDlq = "esb.documents.dlq";
    public const string DocumentsFailed = "esb.documents.failed";
}
