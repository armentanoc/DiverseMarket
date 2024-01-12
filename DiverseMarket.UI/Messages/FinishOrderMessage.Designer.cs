using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;
using DiverseMarket.UI.Pages.Customer;

namespace DiverseMarket.UI.Messages
{
    partial class FinishOrderMessage
    {
        private System.ComponentModel.IContainer components = null;

        private Label messageLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent(long userId)
        {
            InitializeComponentAsync(userId);        
        }

        private async Task InitializeComponentAsync(long userId)
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += _FormClosed;
            InitScreen();
            await Task.Delay(1500);
            new HomePageCustomer(userId).Show();
            this.Hide();
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitScreen()
        {

            InitLogo();
            InitLabels();
        }

        private void InitLabels()
        {
            messageLabel = new Label();
            messageLabel.Text = "Obrigada por realizar um pedido. Logo sua encomenda chegará.";
            messageLabel.ForeColor = Color.White;
            messageLabel.Width = 708;
            messageLabel.Height = 300;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Font = new Font("Ubuntu", 24);
            messageLabel.BackColor = Color.Transparent;
            messageLabel.Location = new Point(286, 342);
            this.Controls.Add(messageLabel);
        }
        
        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(286, 164);
            logo.Width = 708;
            logo.Height = 83;

            this.Controls.Add(logo);
        }
    }
}