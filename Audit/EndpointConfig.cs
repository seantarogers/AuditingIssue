namespace Audit
{
    using NServiceBus;
    
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("auditingissue");
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<NHibernatePersistence>();

            // stop processing incoming subscription control messages
            configuration.Pipeline.Remove("ProcessSubscriptionRequests");

            var conventions = configuration.Conventions();

            conventions.DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Messages"));
        }
    }
}

