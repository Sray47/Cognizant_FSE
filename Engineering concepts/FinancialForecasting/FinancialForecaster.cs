namespace FinancialForecasting
{
    public class FinancialForecaster
    {
        public double Predict(double[] history, int years, double rate)
        {
            if (years == 0)
            {
                return history[history.Length - 1];
            }
            return Predict(history, years - 1, rate) * (1 + rate);
        }
        public double PredictMemo(double[] history, int years, double rate, double[] memo)
        {
            if (memo[years] != 0)
            {
                return memo[years];
            }
            if (years == 0)
            {
                memo[0] = history[history.Length - 1];
                return memo[0];
            }
            memo[years] = PredictMemo(history, years - 1, rate, memo) * (1 + rate);
            return memo[years];
        }
    }
}
