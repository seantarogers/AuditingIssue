namespace Audit.Handlers
{
    using System;

    using Messages;

    using NServiceBus;
    public class AuditMessageHandler : IHandleMessages<IAuditable>
    {
        public void Handle(IAuditable message)
        {
            Console.WriteLine("Auditing message with correlationId {0}", message.CorrelationId);
        }

    }
}