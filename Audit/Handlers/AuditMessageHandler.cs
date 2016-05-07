namespace Audit.Handlers
{
    using System;
    using Messages;

    using NServiceBus;
    public class AuditMessageHandler : IHandleMessages<IAuditable>
    {
        public void Handle(IAuditable message)
        {
            string type = message.GetType().FullName;

            Console.WriteLine("Auditing message with correlationId {0}, of type: {1}", message.CorrelationId, type);
        }

    }
}