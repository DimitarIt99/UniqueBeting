namespace UniqueBetValue.Logger
{
    public class Logger : ILogger
    {
        public void Log(string stringToLog)
        {
            System.Console.WriteLine(stringToLog);
        }
    }
}
