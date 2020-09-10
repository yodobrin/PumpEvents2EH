/*
Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that. 
You agree: 
	(i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded;
    (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and
	(iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code
**/

// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

using Microsoft.Azure.EventHubs;
using System.Text;
using Newtonsoft.Json;

namespace RecursivePump
{
    public static class Push2EH
    {
        [FunctionName("Push2EH")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());

            string connectionString = Environment.GetEnvironmentVariable("EVENT_HUB_CONN");
            string EventHubName = Environment.GetEnvironmentVariable("EH_HUB");
                
            EventHubsConnectionStringBuilder eventHubConnectionStringBuilder =
                new EventHubsConnectionStringBuilder(connectionString)
                {
                    EntityPath = EventHubName
                };
            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionStringBuilder.ToString());
            dynamic person = new System.Dynamic.ExpandoObject();
            person.id = Guid.NewGuid().ToString();
            person.name = $"audiocodes{DateTime.Now.Ticks}";
            EventData eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person)));
            eventHubClient.SendAsync(eventData);
            log.LogInformation($"Push2EH sent message to EH with id: {person.id}");  
            eventHubClient.Close();

        }
    }
}
