
namespace DiverseMarket.UI.Components
{
    internal class MessageBoxUtils
    {
        internal static bool ConfirmAction(string message)
        {
            return MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes);
        }

        internal static void ShowMessageBox(string message, MessageBoxIcon icon)
        {
            MessageBox.Show(message, icon.Equals(MessageBoxIcon.Information) ? "Success" : "Error", MessageBoxButtons.OK, icon);
        }
    }
}
