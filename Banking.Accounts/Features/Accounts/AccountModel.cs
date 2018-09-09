namespace Banking.Accounts.Features.Accounts
{
    public class AccountModel : TransientAccountModel
    {
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public string Status { get; set; }
    }
}
