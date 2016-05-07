namespace EventPublisher.Handlers
{
    using System;
    using Messages.Commands;
    using Messages.Events;

    using NServiceBus;
    public class DoSomethingCommandHandler : IHandleMessages<DoSomethingCommand>
    {
        private readonly IBus bus;

        public DoSomethingCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(DoSomethingCommand command)
        {
            Console.WriteLine("Publishing event with correlationId: {0}", command.CorrelationId);

            bus.Publish(new SomethingHappenedEvent { CorrelationId = command.CorrelationId });
        }
    }
}