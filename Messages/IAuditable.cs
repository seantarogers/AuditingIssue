namespace Messages
{
    using System;

    public interface IAuditable
    {
        Guid CorrelationId { get; set; }
        Guid MessageId { get; }
    }
}