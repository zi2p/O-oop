using System;
using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Client;
using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;
using Banks.Services;
using Banks.Services.Builder;
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
            var clientBuilder1 = new ClientBuilder();
            clientBuilder1.SetName("Ксения Павловна", 1613923365);
            IBankAccount bankAccount1 = new DebitAccount(sum1);
            var transferLimit = new TransferLimit(sum1 / 2);
            var bank = new Bank(0);
            clientBuilder1.SetAccount(bankAccount1);
            clientBuilder1.SetAddress("ул.Михалкова 14");
            clientBuilder1.SetPassport(1613923365);
            Сlient Ksenia = clientBuilder1.Build();
            IMethodPercentageChange methodPercentageChange = new PercentageChange1();
            bank.SetMethodOfPercentageChange(methodPercentageChange);
            bank.SetMethodOfTransferLimit(transferLimit);
            
            var clientBuilder2 = new ClientBuilder();
            clientBuilder2.SetName("Лидия Михайловна", 1414778398);
            IBankAccount bankAccount2 = new DebitAccount(sum2);
            clientBuilder2.SetAccount(bankAccount2);
            clientBuilder2.SetAddress("ул.Чуйкова 5");
            clientBuilder2.SetPassport(1414778398);
            Сlient Lida = clientBuilder1.Build();
            bankAccount1.SetMyBank(bank);
            bankAccount2.SetMyBank(bank);
            _centralBank.AddBank(bank);
            bank.SetMyCentralBank(_centralBank);
            Assert.That(sum1.Equals(bankAccount1.GetTheAmountOnTheAccount()));
            bankAccount1.TransferOfMoney((sum1 / 4), Lida, DateTime.Today);
        }
    }
}