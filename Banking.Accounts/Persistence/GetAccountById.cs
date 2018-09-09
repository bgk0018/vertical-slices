using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Accounts.Domain;
using Highway.Data;

namespace Banking.Accounts.Persistence
{
    public class GetAccountById : Scalar<Account>
    {
        public GetAccountById(AccountId id)
        {
            ContextQuery = x => x
                .AsQueryable<Account>()
                .FirstOrDefault(p => p.Id.Value == id.Value);
        }
    }
}
