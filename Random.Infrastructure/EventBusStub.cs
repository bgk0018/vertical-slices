using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Random.Infrastructure
{
    public class EventBusStub : IEventBus
    {
        public Task Queue<T>(T model, string eventName) where T : class
        {
            Console.WriteLine($"{eventName}");

            return Task.CompletedTask;
        }
    }
}
