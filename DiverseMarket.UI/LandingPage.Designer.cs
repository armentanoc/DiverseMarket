
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;
using Microsoft.VisualBasic.Logging;
using DiverseMarket.UI.Authentication;

namespace DiverseMarket.UI
{
    partial class LandingPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Design

        private void InitializeComponent()
        {
            InitializeComponentAsync();
        }

        private async Task InitializeComponentAsync()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 832);
            this.Text = "DiverseMarket";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Colors.MainBackgroundColor;
            InitLogo();

            await Task.Delay(1500);
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Hide();
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(378, 366);
            logo.Width = 523;
            logo.Height = 61;

            this.Controls.Add(logo);


        }

        #endregion
    }
}