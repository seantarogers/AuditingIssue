namespace TestMessageSender
{
    using System;

    using Messages.Commands;

    using NServiceBus;

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Test Command Sender");
            Console.WriteLine("==============================");
            
            var busConfiguration = CreateBusConfiguration();
            using (var bus = Bus.CreateSendOnly(busConfiguration))
            {
                Console.WriteLine("1. enter 'dsc' to send a DoSomethingCommand");
                Console.WriteLine("2. enter 'e' to exit");

                string input;
                while ((input = Console.ReadLine()) != "e")
                {
                    if (input == "dsc")
                    {
                        var correlationId = Guid.NewGuid();
                        bus.Send(new DoSomethingCommand {CorrelationId = correlationId});
                        Console.WriteLine("==============================");
                        Console.WriteLine("sending DoSomethingCommand to the EventPublisher queue. CorrelationId: {0}", correlationId);
                    }
                }

                if (input == "e")
                {
                    return;
                }
                Console.ReadLine();
            }
        }

        private static BusConfiguration CreateBusConfiguration()
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.UseSerialization<JsonSerializer>();
            var conventions = busConfiguration.Conventions();
            busConfiguration.AssembliesToScan(AllAssemblies.Matching("NServiceBus").And("TestMessageSender").And("Messages"));
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Commands"));

            return busConfiguration;
        }
    }
}
