# EventGridPump

## High Level Narrative
Sending large number of events to event hub, via 2 functions. 
First one, listen to post mesages, picking up the number of events it needs to push. It will send then messages to an event grid.
The second function, is triggered by the event grid messages, and it sends a single message to an event hub.

![Diagram](https://user-images.githubusercontent.com/37622785/92710016-14d2f100-f360-11ea-836b-16323566478e.png)