using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Accounts.Core.Models;

namespace Banking.Accounts.Models
{
    public class AccountModel : TransientAccountModel
    {
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public string Status { get; set; }
    }
}
