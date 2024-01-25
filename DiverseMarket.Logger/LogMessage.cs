
using System.Diagnostics;

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

        public LogMessage(Exception ex)
        {
            ErrorLog(ex);
        }

        #endregion

        #region ErrorLog
        private void ErrorLog(Exception ex)
        {
            string log = $"An error occurred." +
                $"\nMessage: {ex.Message}" +
                $"\nStackTrace: {ex.StackTrace}";

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