using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Random.Infrastructure
{
    public interface IEventBus
    {
        Task Queue<T>() where T : class;
    }
}
