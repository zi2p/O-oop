namespace Banks.Entities.Methods
{
    public class TransferLimit : IMethodTransferLimit
    {
        public TransferLimit(double sum)
        {
            Sum = sum;
        }

        private double Sum { get; }
        public double GetMaxSum()
        {
            return Sum;
        }
    }
}