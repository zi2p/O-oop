using System;
using Banks.Entities.BankAccounts;

namespace Banks.Services.Factory
{
    public class FactoryConsole : IFactory
    {
        private IBankAccount _account;
        public void SetAccount(IBankAccount account)
        {
            _account = account;
        }

        public IBankAccount CreatedBankAccount(int account)
        {
            double sum;
            IBankAccount bankAccount = null;
            switch (account)
            {
                case 1:
                case 2:
                {
                    Console.WriteLine("Какую сумму Вы кладете на счет? ");
                    sum = Convert.ToDouble(Console.ReadLine());
                    bankAccount = account switch
                    {
                        1 => new DebitAccount(sum),
                        2 => new DebitAccount(sum),
                        _ => (IBankAccount)null
                    };

                    break;
                }

                case 3:
                {
                    sum = double.MaxValue;
                    bankAccount = new CreditAccount();
                    break;
                }
            }

            return bankAccount;
        }
    }
}