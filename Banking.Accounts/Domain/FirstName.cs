using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Accounts.Domain
{
    public class FirstName
    {
        public string Value { get; }

        public FirstName(string value)
        {
            Value = value;
        }
    }
}
