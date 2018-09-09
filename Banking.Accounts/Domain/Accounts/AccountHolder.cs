namespace Banking.Accounts.Domain.Accounts
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
