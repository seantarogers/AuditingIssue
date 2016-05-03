namespace Messages.Events
{
    using System;

    public class SomethingHappenedEvent :  IAuditable
    {
        public SomethingHappenedEvent()
        {
            MessageId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
    }
}
