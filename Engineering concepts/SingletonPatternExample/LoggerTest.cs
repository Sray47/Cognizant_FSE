namespace SingletonPatternExample
{
    public class LoggerTest
    {
        public static void Main(string[] args)
        {
            Logger logger1 = Logger.GetInstance();
            Logger logger2 = Logger.GetInstance();
            logger1.Log("First log message");
            logger2.Log("Second log message");
            if (logger1 == logger2)
            {
                System.Console.WriteLine("Both logger1 and logger2 refer to the same instance.");
            }
            else
            {
                System.Console.WriteLine("Different instances exist! Singleton pattern failed.");
            }
        }
    }
}
