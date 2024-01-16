
namespace DiverseMarket.Backend.MyLogger
{
    internal class Log
    {
        public static void Error(string log)
        {
            string logFilePath = "error_log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now}: {log}");
                }
            }
            catch (IOException ex)
            {
                // Handle exception, e.g., log to console
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }
}
