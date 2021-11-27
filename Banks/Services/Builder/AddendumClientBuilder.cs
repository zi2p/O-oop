using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Client;

namespace Banks.Services.Builder
{
    public class AddendumClientBuilder : IClientBuilder
    {
        private const uint MAXPASSPORT = unchecked(999999999);
        private const uint MINPASSPORT = unchecked(1000000000U);
        private Сlient _client;

        public AddendumClientBuilder(Сlient client)
        {
            _client = client;
        }

        public Сlient Build()
        {
            return _client;
        }

        public void SetName(string name, uint id)
        {
        }

        public void SetPassport(uint passport)
        {
            if (passport <= MAXPASSPORT && passport >= MINPASSPORT) _client.Passport = passport;
        }

        public void SetAddress(string address)
        {
            _client.Address = address;
        }

        public void SetAccount(IBankAccount account)
        {
            _client.BankAccount = account;
        }

        public void SetPhone(uint phone)
        {
            _client.Phone = phone;
        }
    }
}