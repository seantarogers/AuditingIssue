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

#### Repo structure

The repo contains the following components:

## To recreate




