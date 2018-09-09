using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Accounts.Domain
{
    public class AccountId
    {
        private int _id;

        public int Value
        {
            get => _id;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _id = value;
            }
        }

        public AccountId(int value)
        {
            Value = value;
        }
    }
}
