
namespace DiverseMarket.Logger
{
    public class LogMessage
    {
        private readonly string logFilePath = "log.txt";

        #region Constructors
        public LogMessage(string logMessage)
        {
            WriteInLog(logMessage);
        }

        public LogMessage(string methodName, Exception ex)
        {
            ErrorLog(methodName, ex);
        }

        #endregion

        #region ErrorLog
        private void ErrorLog(string methodName, Exception ex)
        {
            string log = $"An error occurred in {methodName} - Message: {ex.Message} - StackTrace: {ex.StackTrace}";
            WriteInLog(log);
        }
        #endregion

        #region WriteInLog
        private void WriteInLog(string logToWrite)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now}: {logToWrite}");
                }
            }
            catch (IOException thisEx)
            {
                Console.WriteLine($"An error occurred in {nameof(WriteInLog)} - Message: {thisEx.Message} - StackTrace: {thisEx.StackTrace}");
                throw;
            }
        }
        #endregion

    }
}