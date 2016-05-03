# AuditingIssue
NServiceBus auditing issue repo

#### Problem
I am wanting to add auditing to our NServiceBus endpoints via the following configuration.

  `<AuditConfig QueueName="audit" />`

However, it seems that adding auditing to an endpoint that publishes events is causing a recursive subscription.  In other words, the endpoint is subscribing to its own events.  This is resulting in the following error (and events being added to the endpoint's error queue):

```cs

System.InvalidOperationException: No handlers could be found for message type: Messages.Events.SomethingHappenedEvent

````

When auditing is disabled in the Event Publisher the recursive subscription does not get added and the error goes away.
Tried to disable AutoSubscribe in the Publisher but this did not resolve the problem

```cs
configuration.DisableFeature<AutoSubscribe>();
````

#### To recreate the problem

* Open solution in Visual Studio 2015
* Ensure that you have an empty SQL Server 2014 database named "NServiceBus" for the subscriptions and saga data (see connection string for details)
* Ensure that you have an NServiceBus license in this location: C:\NServiceBus\License.xml
* Configure the solution to start the Audit, EventPublisher, Saga*, TestMessageSender consoles
* Type "dsc" into the *TestMessageSender* console window to send a command to the EventPublisher
* Check the subscription table to see the *EventPublisher@MyMachine* *Messages.Events.SomethingHappenedEvent,1.0.0.0* subscription
* Observe that a copy of the SomethingHappenedEvent is moved onto the EventPublisher.Error queue

#### Solution Structure

The repo contains the following components:

![Image of Solution](https://github.com/seantarogers/AuditingIssue/blob/master/auditingissue.png)

Thanks for your help





