using Backups.Tools;
using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Client;

namespace Banks.Services.Builder
{
    public class ClientBuilder : IClientBuilder
    {
        private const uint MAXPASSPORT = unchecked(999999999);
        private const uint MINPASSPORT = unchecked(1000000000U);
        private Сlient _сlient;
        public Сlient Build()
        {
            if (_сlient != null) return _сlient;
            throw new BackupsException("the client does not exist");
        }

        public void SetName(string name, uint id)
        {
            _сlient = new Сlient(name, id);
        }

        public void SetPassport(uint passport)
        {
            if (passport <= MAXPASSPORT && passport >= MINPASSPORT) _сlient.Passport = passport;
        }

        public void SetAddress(string address)
        {
            _сlient.Address = address;
        }

        public void SetAccount(IBankAccount account)
        {
            _сlient.BankAccount = account;
        }
    }
}