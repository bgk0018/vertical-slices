using Banking.Accounts.Domain;
using Banking.Accounts.Models;
using Random.Infrastructure;

namespace Banking.Accounts.Mappers
{
    public class AccountMapper : IMapper<Account, AccountModel>
    {
        public AccountModel Map(Account source)
        {
            return new AccountModel()
            {
                Balance = source.Balance,
                HolderFirstName = source.AccountHolder.FirstName.Value,
                HolderLastName = source.AccountHolder.LastName.Value,
                Status = source.AccountStatus.ToString(),
                Id = source.Id.Value
            };
        }
    }
}
