using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Model.Users;
using SalesApp.DomainLayer.Service;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;
using SalesApp.UI.Util;
using System.Text;

namespace SalesApp.UI.Authentication
{
    partial class ClientRegisterPage
    {
        private System.ComponentModel.IContainer components = null;

        private Label usernameWarningLabel;
        private Label passwordWarningLabel;
        private Label emailWarningLabel;
        private Label cepWarningLabel;
        private Label cpfWarningLabel;

        private RoundedTextBox fullNameTextBox, emailTextBox, usernameTextBox, telephoneTextBox, cpfTextBox, cepTextBox, streetTextBox,
            numberTextBox, complementTextBox, cityTextBox;
        private PasswordRoundedTextBox passwordTextBox;

        private Button loginButton;
        private RoundedButton registerButton;
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
            FormClosed += ClientRegisterPage_FormClosed;
            InitScreen();
        }

        private void ClientRegisterPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitScreen()
        {
            InitLogo();
            InitLabels();
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


            this.registerButton = new RoundedButton("CADASTRAR-SE", 224, 57, Colors.CallToActionButton, 32);
            this.registerButton.Location = new Point(998, 714);
            this.registerButton.Cursor = Cursors.Hand;
            this.registerButton.Click += registerButton_Click;

            this.Controls.Add(this.registerButton);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                string fullName = this.fullNameTextBox.TextBox.Text;
                string email = this.emailTextBox.TextBox.Text;
                string username = this.usernameTextBox.TextBox.Text;
                string? telephone = (this.telephoneTextBox.TextBox.Text.Equals("Telefone") ? null : this.telephoneTextBox.TextBox.Text);
                string CPF = this.cpfTextBox.TextBox.Text;
                AddressDTO address = new AddressDTO(this.cepTextBox.TextBox.Text, 
                    this.streetTextBox.TextBox.Text, 
                    (this.complementTextBox.TextBox.Text.Equals("Complemento") ? null : this.complementTextBox.TextBox.Text), 
                    this.numberTextBox.TextBox.Text, this.cityTextBox.TextBox.Text);
                string password = this.passwordTextBox.TextBox.Text;

                LoginResponseDTO response = AuthenticationService.RegisterCustomer(
                    new DomainLayer.DTOs.RegisterCustomerDTO(fullName, email, username, telephone, CPF, address, password));
                if (response.Id != null)
                {
                    MessageBox.Show("Conta criada com sucesso.");
                    new LoginPage().Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Houve um erro, tente novamente.");
            }
        }

        private bool CheckFields()
        {
            List<string> wrongFields = new List<string>();

            string fullName = this.fullNameTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidName(fullName, "Nome completo*", true))
                wrongFields.Add("Nome completo");

            string email = emailTextBox.TextBox.Text;

            if (!ValidationUtils.IsEmailValid(email))
            {
                wrongFields.Add("e-mail");
                if (!email.Equals("E-mail*"))
                    this.emailWarningLabel.Visible = true;
            }
            else
                this.emailWarningLabel.Visible = false;

            string username = usernameTextBox.TextBox.Text;

            if (!ValidationUtils.IsUsernameValid(username))
            {
                wrongFields.Add("username");
                if (!username.Equals("Username"))
                    this.usernameWarningLabel.Visible = true;
            }
            else
                this.usernameWarningLabel.Visible = false;

            string password = passwordTextBox.TextBox.Text;

            if (!ValidationUtils.IsPasswordValid(password))
            {
                wrongFields.Add("password");
                this.passwordWarningLabel.ForeColor = Color.MediumVioletRed;
            }
            else
                this.passwordWarningLabel.ForeColor = Color.White;


            string cpf = cpfTextBox.TextBox.Text;

            if (!ValidationUtils.IsCPFValid(cpf))
            {
                wrongFields.Add("CPF");
                if (!cpf.Equals("CPF*"))
                    this.cpfWarningLabel.Visible = true;
            }
            else
                this.cpfWarningLabel.Visible = false;

            bool validInputs = wrongFields.Count == 0;

            if (!validInputs)
                ShowMessageForWrongFields(wrongFields);


            return validInputs;
        }

        private void ShowMessageForWrongFields(List<string> wrongFields)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Você digitou os seguintes campos errados: ");

            foreach (var field in wrongFields)
            {
                sb.Append(field + ", ");
            }

            string message = sb.ToString();

            if (message[message.Length - 2] == ',')
            {
                message = message.Substring(0, message.Length - 2);
            }



            MessageBox.Show(message, "Atenção");
        }

        private void InitTextBoxes()
        {
            fullNameTextBox = new RoundedTextBox("Nome completo*", 572, 60);
            fullNameTextBox.Location = new Point(354, 166);
            fullNameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(fullNameTextBox);

            emailTextBox = new RoundedTextBox("E-mail*", 572, 60);
            emailTextBox.Location = new Point(354, 254);
            emailTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(emailTextBox);

            usernameTextBox = new RoundedTextBox("Username*", 342, 60);
            usernameTextBox.Location = new Point(354, 342);
            usernameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(usernameTextBox);

            telephoneTextBox = new RoundedTextBox("Telefone", 205, 60);
            telephoneTextBox.Location = new Point(721, 342);
            telephoneTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(telephoneTextBox);

            cpfTextBox = new RoundedTextBox("CPF*", 342, 60);
            cpfTextBox.Location = new Point(354, 430);
            cpfTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cpfTextBox);

            cepTextBox = new RoundedTextBox("CEP*", 205, 60);
            cepTextBox.Location = new Point(721, 430);
            cepTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cepTextBox);

            streetTextBox = new RoundedTextBox("Rua*", 431, 60);
            streetTextBox.Location = new Point(354, 518);
            streetTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(streetTextBox);

            numberTextBox = new RoundedTextBox("Número*", 113, 60);
            numberTextBox.Location = new Point(813, 518);
            numberTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(numberTextBox);

            complementTextBox = new RoundedTextBox("Complemento", 264, 60);
            complementTextBox.Location = new Point(354, 606);
            complementTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(complementTextBox);

            cityTextBox = new RoundedTextBox("Cidade*", 286, 60);
            cityTextBox.Location = new Point(640, 606);
            cityTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cityTextBox);

            passwordTextBox = new PasswordRoundedTextBox(572, 60);
            passwordTextBox.Location = new Point(354, 694);
            passwordTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(passwordTextBox);

        }

        private void InitLabels()
        {
            usernameWarningLabel = new Label();
            usernameWarningLabel.Text = "Username já está em uso.";
            usernameWarningLabel.ForeColor = Color.White;
            usernameWarningLabel.AutoSize = true;
            usernameWarningLabel.Font = new Font("Ubuntu", 6);
            usernameWarningLabel.BackColor = Color.Transparent;
            usernameWarningLabel.Location = new Point(380, 406);
            usernameWarningLabel.Visible = false;
            this.Controls.Add(usernameWarningLabel);

            passwordWarningLabel = new Label();
            passwordWarningLabel.Text = "A senha deve ter pelo menos 8 caracteres, uma letra e um número.";
            passwordWarningLabel.ForeColor = Color.White;
            passwordWarningLabel.AutoSize = true;
            passwordWarningLabel.Font = new Font("Ubuntu", 6);
            passwordWarningLabel.BackColor = Color.Transparent;
            passwordWarningLabel.Location = new Point(380, 760);
            passwordWarningLabel.Visible = true;
            this.Controls.Add(passwordWarningLabel);

            emailWarningLabel = new Label();
            emailWarningLabel.Text = "E-mail inválido ou em uso.";
            emailWarningLabel.ForeColor = Color.White ;
            emailWarningLabel.AutoSize = true;
            emailWarningLabel.Font = new Font("Ubuntu", 6);
            emailWarningLabel.BackColor = Color.Transparent;
            emailWarningLabel.Location = new Point(380, 314);
            emailWarningLabel.Visible = false;
            this.Controls.Add(emailWarningLabel);

            cepWarningLabel = new Label();
            cepWarningLabel.Text = "CEP inválido.";
            cepWarningLabel.ForeColor = Color.White;
            cepWarningLabel.AutoSize = true;
            cepWarningLabel.Font = new Font("Ubuntu", 6);
            cepWarningLabel.BackColor = Color.Transparent;
            cepWarningLabel.Location = new Point(745, 494);
            cepWarningLabel.Visible = false;
            this.Controls.Add(cepWarningLabel);

            cpfWarningLabel = new Label();
            cpfWarningLabel.Text = "Cpf inválido.";
            cpfWarningLabel.ForeColor = Color.White;
            cpfWarningLabel.AutoSize = true;
            cpfWarningLabel.Font = new Font("Ubuntu", 6);
            cpfWarningLabel.BackColor = Color.Transparent;
            cpfWarningLabel.Location = new Point(380, 494);
            cpfWarningLabel.Visible = false;
            this.Controls.Add(cpfWarningLabel);

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