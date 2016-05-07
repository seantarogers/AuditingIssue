namespace Saga
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("Saga");
            configuration.UseSerialization<JsonSerializer>();

            configuration.UsePersistence<NHibernatePersistence>();
                
            configuration.EnableInstallers();

            var conventions = configuration.Conventions();
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Commands"));
        }        
    }
}
