using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Accounts.Domain
{
    public class LastName
    {
        public string Value { get; }

        public LastName(string value)
        {
            Value = value;
        }
    }
}
