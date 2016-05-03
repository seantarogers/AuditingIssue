namespace Saga
{
    using System;

    using NServiceBus.Saga;

    public class MySagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }

        public virtual Guid CorrelationId { get; set; }
    }
}