using System;

namespace Banking.Accounts.Domain.Accounts
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
