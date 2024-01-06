using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Authentication
{
    partial class ForgotPasswordPage
    {
        private System.ComponentModel.IContainer components = null;

        private RoundedTextBox cpfOrCnpjTextBox, emailTextBox;
        private PasswordRoundedTextBox newPasswordTextBox;

        private Button loginButton;
        private RoundedButton resetPasswordButton;

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
            FormClosed += ForgotPasswordPage_FormClosed;
            InitScreen();
        }

        private void ForgotPasswordPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitScreen()
        {

            InitLogo();
            InitTextBoxes();
            InitButtons();
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


            this.resetPasswordButton= new RoundedButton("REDEFINIR SENHA", 224, 57, Colors.CallToActionButton, 32);
            this.resetPasswordButton.Location = new Point(528, 618);
            this.resetPasswordButton.Cursor = Cursors.Hand;
            this.resetPasswordButton.Click += resetPasswordButton_Click;

            this.Controls.Add(this.resetPasswordButton);
        }

        private void resetPasswordButton_Click(object sender, EventArgs e)
        {
            
        }

        private void InitTextBoxes()
        {

            emailTextBox = new RoundedTextBox("E-mail", 681, 60);
            emailTextBox.Location = new Point(299, 314);
            emailTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(emailTextBox);

            cpfOrCnpjTextBox = new RoundedTextBox("CPF/CNPJ", 681, 60);
            cpfOrCnpjTextBox.Location = new Point(299, 390);
            cpfOrCnpjTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cpfOrCnpjTextBox);

            newPasswordTextBox = new PasswordRoundedTextBox(681, 60);
            newPasswordTextBox.Location = new Point(299, 466);
            newPasswordTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(newPasswordTextBox);
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(478, 63);
            logo.Width = 324;
            logo.Height = 38;

            this.Controls.Add(logo);
        }


    }
}