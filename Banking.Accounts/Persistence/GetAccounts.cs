using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Accounts.Domain;
using Highway.Data;

namespace Banking.Accounts.Persistence
{
    public class GetAccounts : Query<Account>
    {
        public GetAccounts()
        {
            ContextQuery = x => x.AsQueryable<Account>();
        }
    }
}
