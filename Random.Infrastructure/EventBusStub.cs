using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Random.Infrastructure
{
    public class EventBusStub : IEventBus
    {
        public Task Queue<T>(T model, string eventName) where T : class
        {
            var message = new
            {
                Event = eventName,
                Model = model
            };

            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));

            return Task.CompletedTask;
        }
    }
}
