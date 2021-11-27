using Banks.Entities.BankAccounts;

namespace Banks.Services.Factory
{
    public interface IFactory
    {
        public void SetAccount(IBankAccount account);
        public IBankAccount CreatedBankAccount(int account);
    }
}