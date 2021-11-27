using Banks.Entities.BankAccounts;

namespace Banks.Services.QueueOfResponsibilities
{
    public class QueueOfResponsibilities : IQueueOfResponsibilities
    {
        public void BankingOperation(IBankAccount account)
        {
            account.AppointPercentages(account.GetPercentages());
            account.AddingInterestToTheAmount();
            account.AppointСommission(account.GetCommission());
        }
    }
}