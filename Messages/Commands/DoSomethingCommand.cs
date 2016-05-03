namespace Messages.Commands
{
    using System;

    public class DoSomethingCommand :  IAuditable
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }

        public DoSomethingCommand()
        {
            MessageId = Guid.NewGuid();
        }
    }
}