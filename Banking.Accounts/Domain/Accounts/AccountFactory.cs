using Random.Infrastructure;

namespace Banking.Accounts.Domain.Accounts
{
    public class AccountFactory : IAccountFactory
    {
        private readonly SimpleIdFactory _idFactory;

        public AccountFactory(SimpleIdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public Account Build(string firstName, string lastName)
        {
            var accountId = new AccountId(_idFactory.Generate());
            var holder = new AccountHolder(new FirstName(firstName), new LastName(lastName));
            return Account.New(accountId, holder);
        }
    }
}
