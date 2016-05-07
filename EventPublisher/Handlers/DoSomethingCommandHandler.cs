namespace EventPublisher.Handlers
{
    using System;
    using Messages.Commands;
    using Messages.Events;

    using NServiceBus;
    public class DoSomethingCommandHandler : IHandleMessages<DoSomethingCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(DoSomethingCommand command)
        {
            Console.WriteLine("Publishing event with correlationId: {0}", command.CorrelationId);

            Bus.Publish(new SomethingHappenedEvent { CorrelationId = command.CorrelationId });
        }
    }
}