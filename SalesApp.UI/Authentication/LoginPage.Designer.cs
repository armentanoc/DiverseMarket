using Microsoft.VisualBasic.Logging;
using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Service;
using SalesApp.UI.Components;
using SalesApp.UI.Messages;
using SalesApp.UI.Pages.Company;
using SalesApp.UI.Pages.Customer;
using SalesApp.UI.Pages.Moderator;
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

        #region Init Screen Components

        private void InitializeComponent()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += LoginPage_FormClosed;
            InitScreen();
        }

        private void InitComponents()
        {
            InitLabels();
            InitTextBoxes();
            InitButtons();
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
        private void InitButtons()
        {
            this.loginButton = new RoundedButton("LOGIN", 224, 57, Colors.CallToActionButton, 32);
            this.loginButton.Location = new System.Drawing.Point(528, 564);
            this.loginButton.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.loginButton.Cursor = Cursors.Hand; });
            this.loginButton.Click += new EventHandler(loginButton_Click);
            this.Controls.Add(loginButton);

            this.registerButton = new RoundedButton("CADASTRE-SE", 224, 57, Colors.SecondaryButton, 32);
            this.registerButton.Location = new System.Drawing.Point(528, 643);
            this.registerButton.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.registerButton.Cursor = Cursors.Hand; });
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
            this.forgotPasswordLabel.MouseEnter += new EventHandler((object sender, EventArgs e) => 
                                                    { this.forgotPasswordLabel.Cursor = Cursors.Hand; });
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

        #endregion

        #region OnClickLogic
        private void forgotPasswordLabel_Click(object sender, EventArgs e)
        {
            new ForgotPasswordPage().Show();
            this.Hide();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            new FlowSelectionPage().Show();
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginResponseDTO response = AuthenticationService.Login(new LoginRequestDTO(this.usernameTextBox.TextBox.Text.ToLower(), 
                this.passwordTextBox.TextBox.Text));

            switch(response.UserRole){
                case "Client":
                    new HomePageCustomer(response.Id!.Value).Show();
                    this.Hide();
                    break;
                case "Seller":
                    new HomePageCompany(response.Id!.Value).Show();
                    this.Hide();
                    break;
                case "Moderator":
                    new HomePageModerator(response.Id!.Value).Show();
                    this.Hide();
                    break;
                default: this.ShowLoginIncorrectMessage(); break;
            }
        }

        private void ShowLoginIncorrectMessage()
        {
            new LoginIncorrectMessage().Show();
        }

        #endregion

    }
}