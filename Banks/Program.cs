using Banks.Entities.BankAccounts;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var service = new ConsoleService();
            IBankAccount account = service.EnterTheData();
            service.SeeHowMuchIsOnTheAccount(account);
            service.CashWithdrawal(account, 100);
            service.TopUpYourAccount(account, 1000);
        }
    }
}
