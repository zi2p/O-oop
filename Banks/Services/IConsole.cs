using Banks.Entities.BankAccounts;

namespace Banks.Services
{
    public interface IConsole
    {
        public IBankAccount EnterTheData();
    }
}