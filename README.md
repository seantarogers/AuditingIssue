# AuditingIssue
NServiceBus auditing issue repo

#### Problem
Adding auditing to an NServiceBus event publisher, is causing a recursive subscription so that the publisher is subscribing to its own 
events.  This is resulting in the following error:

```cs

System.InvalidOperationException: No handlers could be found for message type: Messages.Events.SomethingHappenedEvent

````

When auditing is disabled in the publishing service.  The recursive subscription does not get added, and the error goes away.
Tried to disable AutoSubscribe in the publisher, but this did not resolve the problem

```cs
configuration.DisableFeature<AutoSubscribe>();
````

#### To Recreate

* Open solution in Visual Studio 2015
* Ensure that you have an empty SQL Server 2014 database named NServiceBus for the subscriptions and saga data (see connection string for details)
* Ensure that you have an NServiceBus license in this location: C:\NServiceBus\License.xml
* Configure the solution to start the Audit, EventPublisher, Saga, TestMessageSender consoles
* Type "dsc" into the TestMessageSender console window to send a command to the EventPublisher
* Check the subscription table to see the EventPublisher@MyMachine Messages.Events.SomethingHappenedEvent,1.0.0.0 subscription
* Observe the second event appear in the EventPublisher.error queue

#### Solution Structure

The repo contains the following components:

![Image of Solution](https://github.com/seantarogers/AuditingIssue/blob/master/auditingissue.png)





