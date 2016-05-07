namespace Audit
{
    using Autofac;

    using NServiceBus;
    using NServiceBus.Features;


    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("Audit");
            configuration.UseSerialization<JsonSerializer>();
            configuration.DisableFeature<Audit>();
            configuration.UsePersistence<NHibernatePersistence>();

            var conventions = configuration.Conventions();
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Commands"));
            configuration.AssembliesToScan(AllAssemblies.Matching("NServiceBus").And("Audit").And("Messages"));
            configuration.EnableInstallers();
            
            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(CreateContainer()));
        }
        
        private static IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            return containerBuilder.Build();
        }
    }
}

