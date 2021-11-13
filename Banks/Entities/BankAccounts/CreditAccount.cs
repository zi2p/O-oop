using System;
using Banks.Entities.Client;
using Banks.Services.QueueOfResponsibilities;

namespace Banks.Entities.BankAccounts
{
    public class CreditAccount : IBankAccount
    {
        /* имеет кредитный лимит, в рамках которого можно уходить в минус (в плюс тоже можно).
         Процента на остаток нет. Есть фиксированная комиссия за использование, если клиент в минусе.*/
        private Bank _myBank;
        private QueueOfResponsibilities _queue = new QueueOfResponsibilities();
        public CreditAccount(double commission = default)
        {
            Commission = commission;
            Amount = 0;
        }

        private double Commission { get; }
        private double Amount { get; set; }
        private Tuple<string, double, DateTime> LastTransaction { get; set; }

        public void SetMyBank(Bank bank)
        {
            _myBank = bank;
        }

        public double GetPercentages()
        {
            return 0;
        }

        public double GetCommission()
        {
            return Commission;
        }

        public void AppointСommission(double commission)
        {
            if (Amount < 0 && _myBank.GetMyCentralBank().ADayHasPassed(LastTransaction.Item3, DateTime.Today)) Amount -= Commission;
        }

        public void AppointPercentages(double proc)
        {
        }

        public void AddingInterestToTheAmount()
        {
        }

        public double GetTheAmountOnTheAccount()
        {
            return Amount;
        }

        public double CashWithdrawal(double sum, DateTime dateTime)
        {
            Amount -= sum;
            var t = new Tuple<string, double, DateTime>("-", sum, dateTime);
            _queue.BankingOperation(this);
            LastTransaction = t;
            return sum;
        }

        public double TopUpYourAccount(double sum, DateTime dateTime)
        {
            Amount += sum;
            var t = new Tuple<string, double, DateTime>("-", sum, dateTime);
            LastTransaction = t;
            return Amount;
        }

        public void TransferOfMoney(double sum, Сlient person, DateTime dateTime)
        {
            Amount -= sum;
            person.SetMoney(sum, dateTime);
            var t = new Tuple<string, double, DateTime>("-", sum, dateTime);
            LastTransaction = t;
        }

        public void DeleteLastTransaction() // у мошенников деньги мы уже не заберем
        {
            if (LastTransaction.Item1 == "+")
            {
                CashWithdrawal(LastTransaction.Item2, LastTransaction.Item3);
            }
            else
            {
                TopUpYourAccount(LastTransaction.Item2, LastTransaction.Item3);
            }

            LastTransaction = null;
        }
    }
}