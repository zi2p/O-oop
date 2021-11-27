using Banks.Entities;
using Banks.Entities.BankAccounts;
using Banks.Entities.Client;

namespace Banks.Services.Builder
{
    public interface IClientBuilder
    {
        public Сlient Build();
        public void SetName(string name, uint id);
        public void SetPassport(uint passport);
        public void SetAddress(string address);
        public void SetAccount(IBankAccount account);
    }
}