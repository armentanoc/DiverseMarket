using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.UI.Components
{
    internal class PasswordRoundedTextBox : RoundedTextBox
    {
        public PasswordRoundedTextBox(int width, int height) : base("Senha", width, height)
        {
            this.TextBox.Enter += new EventHandler((sender, e) =>
            {
                TextBox textBox = sender as TextBox;
                if (textBox.Text == "Senha")
                {
                    textBox.PasswordChar = '*';
                    textBox.Text = string.Empty;
                    textBox.UseSystemPasswordChar = true;
                }
            });
            this.TextBox.Leave += new EventHandler((sender, e) =>
            {
                TextBox textBox = sender as TextBox;
                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text.Equals("Senha"))
                {
                    textBox.Text = "Senha";
                    textBox.PasswordChar = '\0';
                    textBox.UseSystemPasswordChar = false;
                }
            });
        }
    }
}
