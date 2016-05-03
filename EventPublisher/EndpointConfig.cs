namespace EventPublisher
{
    using Autofac;

    using NServiceBus;
    using NServiceBus.Features;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("EventPublisher");
            configuration.UseSerialization<JsonSerializer>();

            //this did not resolve the issue...but did stop auditing working
            configuration.DisableFeature<AutoSubscribe>();
            configuration.UsePersistence<NHibernatePersistence>();
            configuration.AssembliesToScan(AllAssemblies.Matching("NServiceBus").And("EventPublisher").And("Messages"));
            configuration.EnableInstallers();

            var conventions = configuration.Conventions();
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Commands"));
            
            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(CreateContainer()));
        }
        
        private static IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            return containerBuilder.Build();
        }
    }
}
