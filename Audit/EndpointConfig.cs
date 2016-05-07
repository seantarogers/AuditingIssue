namespace Audit
{
    //using Autofac;

    using NHibernate.Proxy;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Persistence.NHibernate;
    using NServiceBus.Pipeline;

    //using NServiceBus.Features;


    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("auditingissue");
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<NHibernatePersistence>();

            configuration.Pipeline.Remove("ProcessSubscriptionRequests");

            var conventions = configuration.Conventions();
            //conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Events"));
            //conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Commands"));
            conventions.DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Messages"));
            //configuration.AssembliesToScan(AllAssemblies.Matching("NServiceBus").And("Audit").And("Messages"));
            //configuration.EnableInstallers();

            //configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(CreateContainer()));
        }
        
        //private static IContainer CreateContainer()
        //{
        //    var containerBuilder = new ContainerBuilder();
        //    return containerBuilder.Build();
        //}
    }
}

