using System;

namespace Banks.Entities.BankAccounts
{
    public interface IBankAccount
    {
        public void AppointСommission(double commission); // комиссия
        public void AppointPercentages(double proc); // проценты
        public void AddingInterestToTheAmount();
        public double GetTheAmountOnTheAccount(); // сумма
        public double CashWithdrawal(double sum, DateTime dateTime);
        public double TopUpYourAccount(double sum, DateTime dateTime);
        public void TransferOfMoney(double sum, Сlient person, DateTime dateTime);
        public void DeleteLastTransaction();
        public void SetMyBank(Bank bank);
    }
}