using System;

namespace Banking.Accounts.Domain.Accounts
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
