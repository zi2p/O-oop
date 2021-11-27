using System;
using Banks.Entities.Client;
using Banks.Services.QueueOfResponsibilities;

namespace Banks.Entities.BankAccounts
{
    public class DebitAccount : IBankAccount
    {
        /* обычный счет с фиксированным процентом на остаток.
         Деньги можно снимать в любой момент, в минус уходить нельзя.
         Комиссий нет */
        private Bank _myBank;
        private QueueOfResponsibilities _queue = new QueueOfResponsibilities();
        public DebitAccount(double sum, double proc = 1)
        {
            Amount = sum;
            Percentages = proc;
            Sum = Amount * Percentages;
            LastTransaction = new Tuple<string, double, DateTime>(" ", 0, DateTime.Now);
        }

        private double Amount { get; set; }
        private double Sum { get; set; }
        private double Percentages { get; set; }

        private Tuple<string, double, DateTime> LastTransaction { get; set; }

        public void SetPercentage(double proc)
        {
            Percentages = proc;
        }

        public void SetMyBank(Bank bank)
        {
            _myBank = bank;
        }

        public void AppointСommission(double commission)
        {
        }

        public void AppointPercentages(double proc)
        {
            if (_myBank.GetMyCentralBank().ADayHasPassed(LastTransaction.Item3, DateTime.Today)) Sum += Amount * Percentages;
        }

        public void AddingInterestToTheAmount()
        {
            if (_myBank.GetMyCentralBank().AMonthHasPassed(LastTransaction.Item3, DateTime.Today)) Amount += Sum;
        }

        public double GetTheAmountOnTheAccount()
        {
            return Amount;
        }

        public double GetPercentages()
        {
            return Percentages;
        }

        public double GetCommission()
        {
            return 0;
        }

        public double CashWithdrawal(double sum, DateTime dateTime)
        {
            if (!(Amount > sum)) return 0; // столько мы можем снять (ложный код возврата)
            Amount -= sum;
            var t = new Tuple<string, double, DateTime>("+", sum, dateTime);
            _queue.BankingOperation(this);
            LastTransaction = t;
            return sum;
        }

        public double TopUpYourAccount(double sum, DateTime dateTime)
        {
            Amount += sum;
            var t = new Tuple<string, double, DateTime>("-", sum, dateTime);
            _queue.BankingOperation(this);
            LastTransaction = t;
            return Amount;
        }

        public void TransferOfMoney(double sum, Сlient person, DateTime dateTime)
        {
            if (!(sum < Amount) || person == null) return;
            Amount -= sum;
            person.SetMoney(sum, dateTime);
            var t = new Tuple<string, double, DateTime>("-", sum, dateTime);
            _queue.BankingOperation(this);
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