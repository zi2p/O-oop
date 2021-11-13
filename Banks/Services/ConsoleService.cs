using System;
using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;

namespace Banks.Services
{
    public class ConsoleService : IConsole
    {
        /* Для взаимодействия с банком требуется реализовать консольный интерфейс,
         который будет взаимодействовать с логикой приложения, отправлять и получать данные,
         отображать нужную информацию и предоставлять интерфейс для ввода информации пользователем. */
        private const uint MAXPASSPORT = unchecked(999999999);
        private const uint MINPASSPORT = unchecked(1000000000U);
        public IBankAccount EnterTheData()
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
                Console.WriteLine("Введите номер и сорию паспорта без пробела: ");
                passport = Convert.ToUInt32(Console.ReadLine());
                if (passport > MAXPASSPORT || passport < MINPASSPORT) Console.WriteLine("Некорректно введен парспорт. Зайдите в ближайший банк для исправления.");
                Console.WriteLine("Введите свой адрес: ");
                address = Console.ReadLine();
            }

            Console.WriteLine("Каким счётом Вы хотели бы воспользоваться? (1-Дебетовый счет, 2-Депозит, 3-Кредитный счет) ");
            int account = Convert.ToInt32(Console.ReadLine());
            switch (account)
            {
                case 1:
                case 2:
                {
                    Console.WriteLine("Какую сумму Вы кладете на счет? ");
                    sum = Convert.ToDouble(Console.ReadLine());
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

            Console.WriteLine("Ваш банковский аккаунт создан. Спасибо за визит.");
            var person = new Сlient(name, 0, address, passport);
            var transferLimit = new TransferLimit(sum / 2);
            var bank = new Bank(0);
            MakeBank(bank);
            bank.AddClient(name, bankAccount, address, passport);
            IMethodPercentageChange methodPercentageChange = new PercentageChange1();
            MakeBank(transferLimit, methodPercentageChange);
            return bankAccount;
        }

        public void SeeHowMuchIsOnTheAccount(IBankAccount account)
        {
            Console.Write("На вашем счёте: ");
            Console.Write(account.GetTheAmountOnTheAccount());
        }

        public void CashWithdrawal(IBankAccount account, double sum)
        {
            double s = account.CashWithdrawal(sum, DateTime.Now);
            if (s == 0)
            {
                Console.WriteLine("Деньги снять не удалось.");
            }
            else
            {
                Console.Write("На Вашем счёте ");
                Console.Write(sum - s);
            }
        }

        public void TopUpYourAccount(IBankAccount account, double sum)
        {
            double s = account.TopUpYourAccount(sum, DateTime.Now);
            Console.Write("На Вашем счёте ");
            Console.Write(sum + s);
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