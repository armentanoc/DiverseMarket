using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Authentication
{
    partial class CompanyRegisterMessagePage
    {
        private System.ComponentModel.IContainer components = null;

        private Label messageLabel;

        private Button loginButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += CompanyRegisterMessagePage_FormClosed;
            InitScreen();
        }

        private void CompanyRegisterMessagePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitScreen()
        {

            InitLogo();
            InitLabels();
            InitButtons();
        }

        private void InitLabels()
        {
            messageLabel = new Label();
            messageLabel.Text = "O DiverseMarket agradece a sua inscrição. " +
                "Você receberá um e-mail com o resultado da sua solicitação " +
                "quando ela for avaliada pela nossa equipe.";
            messageLabel.ForeColor = Color.White;
            messageLabel.Width = 708;
            messageLabel.Height = 300;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Font = new Font("Ubuntu", 24);
            messageLabel.BackColor = Color.Transparent;
            messageLabel.Location = new Point(286, 342);
            this.Controls.Add(messageLabel);
        }

        private void InitButtons()
        {
            this.loginButton = new System.Windows.Forms.Button();
            this.loginButton.Image = Image.FromFile(@"Resources\back-login.png");
            this.loginButton.Location = new Point(69, 64);
            this.loginButton.Size = new Size(104, 24);
            this.loginButton.BackColor = Color.Transparent;
            this.loginButton.FlatAppearance.BorderSize = 0;
            this.loginButton.FlatStyle = FlatStyle.Flat;
            this.loginButton.Cursor = Cursors.Hand;
            this.loginButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                new LoginPage().Show();
                this.Hide();
            });

            this.Controls.Add(this.loginButton);
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