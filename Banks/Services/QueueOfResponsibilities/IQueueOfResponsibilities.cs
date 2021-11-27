using Banks.Entities.BankAccounts;

namespace Banks.Services.QueueOfResponsibilities
{
    public interface IQueueOfResponsibilities
    {
        public void BankingOperation(IBankAccount account);
    }
}