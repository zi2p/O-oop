using System;
using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;

namespace Banks.Services
{
    public class ConsoleService : IConsole
    {
        // Для взаимодействия с банком требуется реализовать консольный интерфейс,
        // который будет взаимодействовать с логикой приложения, отправлять и получать данные,
        // отображать нужную информацию и предоставлять интерфейс для ввода информации пользователем.
        public void EnterTheData()
        {
            string address = null;
            uint passport = default;
            double sum = 0;
            IBankAccount bankAccount = null;
            Console.WriteLine("Введите Ваше полное имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Хотите ли ввести свои данные о паспорте и месте жительства сейчас? (да или нет)");
            string ans = Console.ReadLine();
            if (ans == "да")
            {
                Console.WriteLine("Введите номер и сореию паспорта без пробела: ");
                passport = Convert.ToUInt32(Console.Read());
                Console.WriteLine("Введите свой адрес: ");
                address = Console.ReadLine();
            }

            Console.WriteLine("Каким счётом Вы хотели бы воспользоваться? (1-Дебетовый счет, 2-Депозит, 3-Кредитный счет) ");
            int account = Console.Read();
            switch (account)
            {
                case 1:
                case 2:
                {
                    Console.WriteLine("Какую сумму Вы кладете на счет? ");
                    sum = Console.Read();
                    switch (account)
                    {
                        case 1:
                        {
                            bankAccount = new DebitAccount(sum);
                            break;
                        }

                        case 2:
                        {
                            bankAccount = new DebitAccount(sum);
                            break;
                        }
                    }

                    break;
                }

                case 3:
                {
                    sum = double.MaxValue;
                    bankAccount = new CreditAccount();
                    break;
                }
            }

            var person = new Сlient(name, 0, address, passport);
            var transferLimit = new TransferLimit(sum / 2);
            var bank = new Bank(0);
            MakeBank(bank);
            bank.AddClient(name, bankAccount, address, passport);
            IMethodPercentageChange methodPercentageChange = new PercentageChange1();
            MakeBank(transferLimit, methodPercentageChange);
        }

        public void SeeHowMuchIsOnTheAccount(IBankAccount account)
        {
            Console.WriteLine(account.GetTheAmountOnTheAccount());
        }

        private void MakeBank(TransferLimit transferLimit, IMethodPercentageChange percentageChange)
        {
            var cb = new CentralBank();
            cb.AddBank(percentageChange, transferLimit);
        }

        private void MakeBank(Bank bank)
        {
            var cb = new CentralBank();
            cb.AddBank(bank);
        }
    }
}