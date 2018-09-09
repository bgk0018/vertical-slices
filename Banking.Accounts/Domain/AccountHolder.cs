using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Accounts.Domain
{
    public class AccountHolder
    {
        public AccountHolder(FirstName firstName, LastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public FirstName FirstName { get; }

        public LastName LastName { get; }
    }
}
