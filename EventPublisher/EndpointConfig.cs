namespace EventPublisher
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("EventPublisher");
            configuration.UseSerialization<JsonSerializer>();

            configuration.UsePersistence<NHibernatePersistence>();

            var conventions = configuration.Conventions();
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Commands"));
        }
    }
}
