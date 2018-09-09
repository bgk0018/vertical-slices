using System.ComponentModel.DataAnnotations;

namespace Banking.Accounts.Models
{
    public class TransientAccountModel
    {
        [Required(AllowEmptyStrings = false)]
        public string HolderFirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string HolderLastName { get; set; }
    }
}
