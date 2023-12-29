using Microsoft.VisualBasic.Logging;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Authentication
{
    partial class LoginPage
    {
        private System.ComponentModel.IContainer components = null;

        //componentes da tela
        private RoundedTextBox usernameTextBox;
        private PasswordRoundedTextBox passwordTextBox;
        private RoundedButton loginButton;
        private RoundedButton registerButton;
        private Label forgotPasswordLabel;
        private Label orLabel;

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
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            InitScreen();
            
        }

        private void InitComponents()
        {
            InitLabels();
            InitTextBoxes();
            InitButtons();
        }

        private void InitButtons()
        {
            this.loginButton = new RoundedButton("LOGIN", 224, 57, Colors.CallToActionButton, 32);
            this.loginButton.Location = new System.Drawing.Point(528, 564);
            this.loginButton.Click += new EventHandler(loginButton_Click);
            this.Controls.Add(loginButton);

            this.registerButton = new RoundedButton("CADASTRE-SE", 224, 57, Colors.SecondaryButton, 32);
            this.registerButton.Location = new System.Drawing.Point(528, 643);
            this.registerButton.Click += new EventHandler(registerButton_Click);
            this.Controls.Add(registerButton);
        }

        private void InitTextBoxes()
        {
            this.usernameTextBox = new RoundedTextBox("Username", 681, 60);
            this.usernameTextBox.Location = new System.Drawing.Point(299, 342);
            this.Controls.Add(usernameTextBox);

            this.passwordTextBox = new PasswordRoundedTextBox(681, 60);
            this.passwordTextBox.Location = new System.Drawing.Point(299, 430);
            this.Controls.Add(passwordTextBox);
        }

        private void InitLabels()
        {
            this.forgotPasswordLabel = new Label();
            this.forgotPasswordLabel.Text = "Esqueceu sua senha?";
            this.forgotPasswordLabel.ForeColor = Colors.SecondaryButton;
            this.forgotPasswordLabel.Font = new Font("Ubuntu", 8, FontStyle.Italic | FontStyle.Underline);
            this.forgotPasswordLabel.Location = new Point(323, 499);
            this.forgotPasswordLabel.Size = new Size(140,20);
            this.forgotPasswordLabel.Click += new EventHandler(forgotPasswordLabel_Click);
            this.Controls.Add(forgotPasswordLabel);

            this.orLabel = new Label();
            this.orLabel.Text = "ou";
            this.orLabel.Font = new Font("Ubuntu", 8, FontStyle.Italic);
            this.orLabel.Location = new Point(631, 622);
            this.orLabel.BackColor = Color.Transparent;
            this.orLabel.ForeColor = Colors.SecondaryButton;
            this.orLabel.Size = new Size(36, 18);
            this.Controls.Add(orLabel);
        }

        private void forgotPasswordLabel_Click(object sender, EventArgs e)
        {

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            
        }

        private void InitScreen()
        {
            InitLogo();
            InitComponents();
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(378, 142);
            logo.Width = 523;
            logo.Height = 61;

            this.Controls.Add(logo);
        }

        #endregion
    }
}