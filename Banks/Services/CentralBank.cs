using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;

namespace Banks.Services
{
    public class CentralBank
    {
        /* Регистрацией всех банков, а также взаимодействием между банками занимается центральный банк.
         Он должен предоставлять все нужные другим банкам методы, методы добавления нового банка.
         Он также занимается уведомлением других банков о том, что нужно начислять остаток или комиссию -
         для этого механизма НЕ требуется создавать таймеры и завязываться на реальное время. */
        private uint _id;
        public CentralBank()
        {
            Banks = new List<Bank>();
            _id = 0;
        }

        private List<Bank> Banks { get; }

        public void AddBank(IMethodPercentageChange percentageChange, TransferLimit transferLimit)
        {
            var bank = new Bank(_id);
            _id++;
            ProvideTheMethodToTheBank(bank, percentageChange, transferLimit);
            Banks.Add(bank);
            bank.SetMyCentralBank(this);
        }

        public void AddBank(Bank bank)
        {
            Banks.Add(bank);
        }

        public List<Bank> GetListBanks()
        {
            return Banks;
        }

        public void ProvideTheMethodToTheBank(Bank bank, IMethodPercentageChange percentageChange, TransferLimit transferLimit)
        {
            bank.SetMethodOfPercentageChange(percentageChange);
            bank.SetMethodOfTransferLimit(transferLimit);
        }

        public bool AMonthHasPassed(DateTime lastTime, DateTime nowTime)
        {
            return (nowTime.Month - lastTime.Month == 1 && nowTime.Year == lastTime.Year) || (nowTime.Year == lastTime.Year + 1 && nowTime.Month == 1 && lastTime.Month == 12);
        }

        public bool ADayHasPassed(DateTime lastTime, DateTime nowTime)
        {
            return nowTime.Year == lastTime.Year && nowTime.Month == lastTime.Month &&
                   nowTime.Day == lastTime.Day + 1;
        }
    }
}