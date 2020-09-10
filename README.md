# EventGridPump

## High Level Narrative
Sending large number of events to event hub, via 2 functions. 
First one, listen to post mesages, picking up the number of events it needs to push. It will send then messages to an event grid.
The second function, is triggered by the event grid messages, and it sends a single message to an event hub.

![Diagram](https://user-images.githubusercontent.com/37622785/92710016-14d2f100-f360-11ea-836b-16323566478e.png)

## Deploy Instructions
1. clone this repo (local of code space)
2. create `local.settings.json` file at the same directory
3. modify the newly created file, to include these values
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "<a storage connection string>",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "EVENT_HUB_CONN": "<your event hub connection string>",
    "EH_HUB": "<event hubs name>",
    "FUNCTIONS_EXTENSION_VERSION": "~3",
    "EG_EP":"<an event grid end point>",
    "EG_KEY":"<the event grid key>"
  }
}
```
4. deploy to your azure account
5. create an event grid topic
6. upload/verify configuration/setting are reflected in the newly deployed function app
7. go the newly created event grid, and add an endpoint listener to your newly created event grid listening function (push2eh)

## Testing your functions
send a post message with `{"spawnFactor":4000}` it will send 4000 messages to your event grid topic, which will create multiple functions which will be triggered by these events, and send a corresponding message to your  event hub.