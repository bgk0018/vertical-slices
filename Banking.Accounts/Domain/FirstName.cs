using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Accounts.Domain
{
    public class FirstName
    {
        private string _firstName;

        public string Value
        {
            get => _firstName;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _firstName = value;
            }
        }

        public FirstName(string value)
        {
            Value = value;
        }
    }
}
