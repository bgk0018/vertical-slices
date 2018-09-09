using System;

namespace Banking.Accounts.Domain.Accounts
{
    public class LastName
    {
        private string _lastName;

        public string Value
        {
            get => _lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _lastName = value;
            }
        }

        public LastName(string value)
        {
            Value = value;
        }
    }
}
