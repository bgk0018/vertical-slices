using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Banking.Accounts.Core.Models;
using Banking.Accounts.Models;
using Random.Infrastructure;

namespace Banking.Accounts.Domain
{
    public class AccountMapper : IMapper<AccountModel, Account>
    {
        public Account Map(AccountModel source)
        {
            return new Account(
                new AccountId(source.Id), 
                source.Balance, 
                new AccountHolder(
                    new FirstName(source.HolderFirstName), 
                    new LastName(source.HolderLastName)));
        }
    }
}
