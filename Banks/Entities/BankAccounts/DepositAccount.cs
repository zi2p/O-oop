using System;
using Banks.Entities.Client;
using Banks.Entities.Methods.Percentage;
using Banks.Services.QueueOfResponsibilities;

namespace Banks.Entities.BankAccounts
{
    public class DepositAccount : IBankAccount
    {
        /* счет, с которого нельзя снимать и переводить деньги до тех пор, пока не закончится его срок (пополнять можно).
         Процент на остаток зависит от изначальной суммы, например, если открываем депозит до 50 000 р. - 3%,
         если от 50 000 р.до 100 000 р.- 3.5%, больше 100 000 р.- 4%.
         Комиссий нет.
         Проценты должны задаваться для каждого банка свои.*/
        private Bank _myBank;
        private QueueOfResponsibilities _queue = new QueueOfResponsibilities();
        public DepositAccount(double sum, DateTime date)
        {
            Amount = sum;
            NowPercentages = GetPercentages(Amount, null);
            Date = date;
            Sum = Amount * GetPercentages(Amount, PercentageChange);
            PercentageChange = null;
        }

        public DateTime Date { get; }
        private double Amount { get; set; }
        private double Sum { get; set; }
        private Tuple<string, double, DateTime> LastTransaction { get; set; }

        private double NowPercentages { get; }
        private IMethodPercentageChange PercentageChange { get; set; }
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
            return 0;
        }

        public void AppointСommission(double commission)
        {
        }

        public void SetPercentageMethod(IMethodPercentageChange methodPercentageChange)
        {
            PercentageChange = methodPercentageChange;
        }

        public void AppointPercentages(double proc)
        {
            if (_myBank.GetMyCentralBank().ADayHasPassed(LastTransaction.Item3, DateTime.Today)) Sum += Amount * GetPercentages(Amount, PercentageChange);
        }

        public void AddingInterestToTheAmount() // когда позволит ценральный банк
        {
            if (_myBank.GetMyCentralBank().AMonthHasPassed(LastTransaction.Item3, DateTime.Today)) Amount += Sum;
        }

        public double GetTheAmountOnTheAccount()
        {
            return Amount;
        }

        public double CashWithdrawal(double sum, DateTime dateTime)
        {
            if (dateTime <= Date || !(Amount > sum)) return 0;
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
            _queue.BankingOperation(this);
            LastTransaction = t;
            return Amount;
        }

        public void TransferOfMoney(double sum, Сlient person, DateTime dateTime)
        {
            if (dateTime <= Date) return;
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

        private double GetPercentages(double sum, IMethodPercentageChange method)
        {
            return method.GetPercentage(sum);
        }
    }
}