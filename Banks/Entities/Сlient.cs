using System;
using Banks.Entities.BankAccounts;

namespace Banks.Entities
{
    public class Сlient
    {
        public Сlient(string name, uint id, string address = null, uint passport = default)
        {
            Name = name;
            Address = address;
            Passport = passport;
            ID = id;
            BankAccount = null;
        }

        private string Name { get; }
        private string Address { get; set; }
        private uint Passport { get; set; }

        private uint ID { get; }
        private IBankAccount BankAccount { get; set; }
        private uint Phone { get; set; }

        public void SetAccount(IBankAccount account)
        {
            BankAccount = account;
        }

        public void SetPassport(uint passport)
        {
            Passport = passport;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public void SetPhone(uint phone)
        {
            Phone = phone;
        }

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