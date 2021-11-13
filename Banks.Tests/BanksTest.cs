using System;
using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;
using Banks.Services;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private CentralBank _centralBank;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void AddBankToCentralBankList_CentralBankListContainsBank()
        {
            var bank = new Bank(0);
            _centralBank.AddBank(bank);
            Assert.Contains(bank, _centralBank.GetListBanks());
        }

        [Test]
        public void TheMoneyWasReturnedToTheClientAfterTheTransactionWasCanceled()
        {
            double sum1 = 100000.5;
            double sum2 = 100000.3;
            string name1 = "Ксения Павловна";
            IBankAccount bankAccount1 = new DebitAccount(sum1, 1);
            var transferLimit = new TransferLimit(sum1 / 2);
            var bank = new Bank(0);
            Сlient Ann = bank.AddClient(name1, bankAccount1, "ул.Михалкова 14", 1613923365);
            Ann.SetAccount(bankAccount1);
            IMethodPercentageChange methodPercentageChange = new PercentageChange1();
            bank.SetMethodOfPercentageChange(methodPercentageChange);
            bank.SetMethodOfTransferLimit(transferLimit);
            IBankAccount bankAccount2 = new DebitAccount(sum2, 1);
            bankAccount1.SetMyBank(bank);
            bankAccount2.SetMyBank(bank);
            Сlient Lida = bank.AddClient("Лидия Михайловна", bankAccount2, "ул.Чуйкова 5", 1414778398);
            _centralBank.AddBank(bank);
            bank.SetMyCentralBank(_centralBank);
            Lida.SetAccount(bankAccount2);
            Assert.That(sum1.Equals(bankAccount1.GetTheAmountOnTheAccount()));
            bankAccount1.TransferOfMoney((sum1 / 4), Lida, DateTime.Today);
            Assert.That((3 * sum1 / 4).Equals(bankAccount1.GetTheAmountOnTheAccount()));
            bankAccount1.DeleteLastTransaction();
            Assert.That(sum1.Equals(bankAccount1.GetTheAmountOnTheAccount()));
        }
    }
}