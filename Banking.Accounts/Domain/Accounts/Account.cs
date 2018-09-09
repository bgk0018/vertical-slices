namespace Banking.Accounts.Domain.Accounts
{
    public class Account
    {
        public Account(
            AccountId id,
            decimal balance,
            AccountHolder holder)
        {
            Id = id;
            Balance = balance;
            AccountHolder = holder;
        }

        public AccountId Id { get; }

        public decimal Balance { get; }

        public AccountHolder AccountHolder { get; set; }

        public AccountStatus AccountStatus
        {
            get
            {
                if (Balance >= 0)
                    return AccountStatus.Open;

                return AccountStatus.Frozen;
            }
        }

        public static Account New(AccountId id, AccountHolder holder)
        {
            return new Account(id, 0, holder);
        }
    }
}
