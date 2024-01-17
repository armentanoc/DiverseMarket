using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Logger;

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
                new LogMessage($"An error occurred: {ex.Message}");
            }   
        }
    }
}
