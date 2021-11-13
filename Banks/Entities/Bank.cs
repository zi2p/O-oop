using System;
using System.Collections.Generic;
using Banks.Entities.BankAccounts;
using Banks.Entities.Client;
using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;
using Banks.Services;

namespace Banks.Entities
{
    public class Bank : IBank
    {
        private CentralBank _myCentralBank;
        public Bank(uint id)
        {
            ID = id;
            Clients = new List<Tuple<Сlient, IBankAccount>>();
            ClientID = 0;
            TransferLimit = new TransferLimit(double.MaxValue);
        }

        private List<Tuple<Сlient, IBankAccount>> Clients { get; }
        private TransferLimit TransferLimit { get; set; }
        private TransferLimit DoubtfulLimit { get; set; }
        private IMethodPercentageChange PercentageChange { get; set; }
        private uint ID { get; }
        private uint ClientID { get; set; }

        public TransferLimit GetDoubtfulLimit()
        {
            return DoubtfulLimit;
        }

        public TransferLimit GetTransferLimit()
        {
            return TransferLimit;
        }

        public void SetMyCentralBank(CentralBank cb)
        {
            _myCentralBank = cb;
        }

        public uint GetID()
        {
            return ID;
        }

        public CentralBank GetMyCentralBank()
        {
            return _myCentralBank;
        }

        public void SetMethodOfPercentageChange(IMethodPercentageChange percentageChange)
        {
            PercentageChange = percentageChange;
        }

        public void SetMethodOfTransferLimit(TransferLimit transferLimit)
        {
            TransferLimit = transferLimit;
        }

        public void SetDoubtfulLimit(TransferLimit transferLimit)
        {
            DoubtfulLimit = transferLimit;
        }

        public Сlient AddClient(string name, IBankAccount account, string address = null, uint passport = default)
        {
            var client = new Tuple<Сlient, IBankAccount>(SetClient(name, address, passport = default), account);
            Clients.Add(client);
            ClientID++;
            return client.Item1;
        }

        public void AddClient(Сlient person)
        {
            var t = new Tuple<Сlient, IBankAccount>(person, person.GetAccount());
            Clients.Add(t);
        }

        public double GetMoney(Сlient person, double sum, DateTime dateTime)
        {
            foreach (Tuple<Сlient, IBankAccount> client in Clients)
            {
                if (client.Item1.GetID() == person.GetID())
                {
                    if (client.Item1.GetAdress() && client.Item1.GetPassport()) return client.Item2.CashWithdrawal(sum, dateTime);
                    return client.Item2.CashWithdrawal(sum < DoubtfulLimit.GetMaxSum() ? sum : DoubtfulLimit.GetMaxSum(), dateTime);
                }
            }

            return 0;
        }

        private Сlient SetClient(string name, string address = null, uint passport = default)
        {
            var person = new Сlient(name, ClientID, address, passport);
            return person;
        }
    }
}