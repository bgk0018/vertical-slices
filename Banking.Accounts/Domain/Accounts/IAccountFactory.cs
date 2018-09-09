namespace Banking.Accounts.Domain.Accounts
{
    public interface IAccountFactory
    {
        Account Build(string firstName, string lastName);
    }
}
