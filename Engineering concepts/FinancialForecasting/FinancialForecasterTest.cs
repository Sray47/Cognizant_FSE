using System;

namespace FinancialForecasting
{
    public class FinancialForecasterTest
    {
        public static void Main(string[] args)
        {
            double[] history = { 1000, 1100, 1210 };
            double rate = 0.1;
            int years = 3;
            FinancialForecaster f = new FinancialForecaster();
            double result = f.Predict(history, years, rate);
            Console.WriteLine(result);
            double[] memo = new double[years + 1];
            double resultMemo = f.PredictMemo(history, years, rate, memo);
            Console.WriteLine(resultMemo);
        }
    }
}
