namespace Saga
{
    using System;
    using Messages.Events;
    using NServiceBus.Saga;

    public class MySaga : Saga<MySagaData>, IAmStartedByMessages<SomethingHappenedEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<SomethingHappenedEvent>(s => s.CorrelationId)
                .ToSaga(m => m.CorrelationId);
        }

        public void Handle(SomethingHappenedEvent message)
        {
            Console.WriteLine("Saga has received event with correlationId: {0}", message.CorrelationId);
        }
    }
}