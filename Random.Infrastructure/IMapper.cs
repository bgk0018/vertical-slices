using System;
using System.Collections.Generic;
using System.Text;

namespace Random.Infrastructure
{
    public interface IMapper<in TSource, out TDestination>
    {
        TDestination Map(TSource source);
    }
}
