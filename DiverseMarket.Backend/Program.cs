using DiverseMarket.Backend.Infrastructure.Operations;
using System.Runtime.CompilerServices;

namespace DiverseMarket.Backend

{
    public class Program
    {
        public static void Main()
        {
            try
            {
                DatabaseConnection.CreateDB();
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error($"An error occurred: {ex.Message}");
            }
        }
    }
}
