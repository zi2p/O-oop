using Banks.Entities.Methods;
using Banks.Entities.Methods.Percentage;

namespace Banks.Entities
{
    public interface IBank
    {
        /* В банке есть Счета и Клиенты.

         Для банков требуется реализовать методы изменений процентов и лимитов нa перевод.
         Также требуется реализовать возможность пользователям подписываться на информацию о таких изменениях -
         банк должен предоставлять интерфейс для подписывания. Например, когда происходит изменение лимита для
         кредитных карт - все пользователи, которые подписались и имеют кредитные карты, должны получить уведомление.*/
        public void SetMethodOfPercentageChange(IMethodPercentageChange percentageChange);
        public void SetMethodOfTransferLimit(TransferLimit transferLimit);
    }
}