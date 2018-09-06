using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Accounts.Domain
{
    public class AccountId
    {
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Value));
                }

                _value = value;
            }
        }

        public AccountId(int value)
        {
            Value = value;
        }
    }
}
