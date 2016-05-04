# AuditingIssue
NServiceBus auditing issue repo

#### Problem
I want to add auditing to our NServiceBus endpoints. I am using the following configuration in each endpoints app.config.

  `<AuditConfig QueueName="audit" />`

When this is added and I start each endpoint, an erroneous, recursive subscription is being written to my subscription table. My 'EventPublisher' endpoint is subscribing to an event that it publishes.  The 'EventPublisher' does not have an event handler for that event, so this is resulting in the following error.

```cs

System.InvalidOperationException: No handlers could be found for message type: Messages.Events.SomethingHappenedEvent

````

The additional event is then being moved the the EventPublisher.Error queue.  The EventPublisher should not be subscribing to this event, it is only publishing it to the Saga endpoint.

When auditing is disabled in the Event Publisher the recursive subscription does not get added and the error goes away.
I have tried to disable AutoSubscribe in the Publisher, but this did not resolve the problem

```cs
configuration.DisableFeature<AutoSubscribe>();
````

#### To recreate the problem

* Open solution in Visual Studio 2015
* Ensure that you have an empty SQL Server 2014 database named "NServiceBus" for the subscriptions and saga data (see connection string for details)
* Ensure that you have an NServiceBus license in this location: C:\NServiceBus\License.xml
* Configure the solution to start the Audit, EventPublisher, Saga*, TestMessageSender consoles
* Type "dsc" into the TestMessageSender console window to send a command to the EventPublisher
* Check the subscription table to see the unwanted subscription - *EventPublisher@MyMachine* *Messages.Events.SomethingHappenedEvent,1.0.0.0*
* Observe that a copy of the SomethingHappenedEvent is moved onto the EventPublisher.Error queue

#### Solution Structure

The repo contains the following components:

![Image of Solution](https://github.com/seantarogers/AuditingIssue/blob/master/auditingissue.png)

* The TestMessageSender is a sendonly bus and it sends a command to the EventPublisher.  
* The EventPublisher endpoint handles the command and publishes an event to which the Saga is subscribing.
* The Saga endpoint handles the event and that is the end of the workflow.
* The Audit endpoint audits all messages processed by both the EventPublisher endpoint and the Saga endpoint

Thanks for your help





