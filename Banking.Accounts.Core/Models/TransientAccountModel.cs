using System;
using System.Collections.Generic;
using System.Text;
using Banking.Accounts.Models;

namespace Banking.Accounts.Core.Models
{
    public class TransientAccountModel : AccountModel
    {
        public string HolderFirstName { get; set; }

        public string HolderLastName { get; set; }
    }
}
