using System;
using Banks.Entities.BankAccounts;

namespace Banks.Entities.Client
{
    public class Сlient
    {
        private const uint MAXPASSPORT = unchecked(999999999);
        private const uint MINPASSPORT = unchecked(1000000000U);
        public Сlient(string name, uint id, string address = null, uint passport = default)
        {
            Name = name;
            Address = address;
            Passport = passport;
            ID = id;
            BankAccount = null;
        }

        public string Address { get; set; }
        public uint Passport { get; set; }
        public uint Phone { get; set; }

        public uint ID { get; }
        public IBankAccount BankAccount { get; set; }
        private string Name { get; }

        public uint GetPhone()
        {
            return Phone;
        }

        public uint GetID()
        {
            return ID;
        }

        public bool GetAdress()
        {
            return Address != null;
        }

        public bool GetPassport()
        {
            return Passport != default;
        }

        public IBankAccount GetAccount()
        {
            return BankAccount;
        }

        public void SetMoney(double sum, DateTime dateTime)
        {
            BankAccount.TopUpYourAccount(sum, dateTime);
        }
    }
}