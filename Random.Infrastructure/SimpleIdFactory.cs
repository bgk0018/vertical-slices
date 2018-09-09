using System;
using System.Collections.Generic;
using System.Text;

namespace Random.Infrastructure
{
    public class SimpleIdFactory
    {
        private static int _oneUpCounter = 1;

        public int Generate()
        {
            return _oneUpCounter++;
        }
    }
}
