using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Service;
using SalesApp.Infrastructure.Operations;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Pages.Customer
{
    partial class CustomerDataPage
    {
        private System.ComponentModel.IContainer components = null;

        private long userId;
        private CustomerDTO customerDTO;

        private RoundedTextBox fullNameTextBox, emailTextBox, usernameTextBox, telephoneTextBox, cpfTextBox, cepTextBox, streetTextBox,
           numberTextBox, complementTextBox, cityTextBox;

        private Button homeButton;
        private RoundedButton saveButton;

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
            this.userId = userId;

            customerDTO = UserService.GetCustomerById(userId);

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += CustomerDataPage_FormClosed;
            InitScreen();
        }

        private void InitScreen()
        {
            InitLogo();
            InitLabel();
            InitTextBoxes();
            InitButtons();
        }

        private void InitButtons()
        {
            this.homeButton = new System.Windows.Forms.Button();
            this.homeButton.Location = new Point(37, 67);
            this.homeButton.Size = new Size(79, 71);
            this.homeButton.FlatStyle = FlatStyle.Flat;
            this.homeButton.FlatAppearance.BorderSize = 0;
            this.homeButton.Cursor = Cursors.Hand;
            this.homeButton.Image = Image.FromFile(@"Resources\logo-reduzida.png");
            this.homeButton.BackgroundImageLayout = ImageLayout.Zoom;
            this.homeButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new HomePageCustomer(this.userId).Show();
            });

            this.Controls.Add(homeButton);

            this.saveButton = new RoundedButton("SALVAR", 224, 57, Colors.CallToActionButton, 32);
            this.saveButton.Location = new Point(528, 715);
            this.saveButton.Cursor = Cursors.Hand;
            this.saveButton.Click += saveButton_Click;

            this.Controls.Add(this.saveButton);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           
        }

        private void InitTextBoxes()
        {
            fullNameTextBox = new RoundedTextBox(customerDTO.FullName, 572, 60);
            fullNameTextBox.Location = new Point(354, 173);
            fullNameTextBox.TextBox.Enabled = false;
            fullNameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(fullNameTextBox);

            emailTextBox = new RoundedTextBox(customerDTO.Email, 572, 60);
            emailTextBox.Location = new Point(354, 254);
            emailTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(emailTextBox);

            usernameTextBox = new RoundedTextBox(customerDTO.Username, 342, 60);
            usernameTextBox.Location = new Point(354, 342);
            usernameTextBox.Enabled = false;
            usernameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(usernameTextBox);

            telephoneTextBox = new RoundedTextBox(customerDTO.Telephone, 205, 60);
            telephoneTextBox.Location = new Point(721, 342);
            telephoneTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(telephoneTextBox);

            cpfTextBox = new RoundedTextBox(customerDTO.CPF, 342, 60);
            cpfTextBox.Location = new Point(354, 430);
            cpfTextBox.Enabled = false;
            cpfTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cpfTextBox);

            cepTextBox = new RoundedTextBox(customerDTO.Address.ZipCode, 205, 60);
            cepTextBox.Location = new Point(721, 430);
            cepTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cepTextBox);

            streetTextBox = new RoundedTextBox(customerDTO.Address.Street, 431, 60);
            streetTextBox.Location = new Point(354, 518);
            streetTextBox.Enabled = false;
            streetTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(streetTextBox);

            numberTextBox = new RoundedTextBox(customerDTO.Address.Number, 113, 60);
            numberTextBox.Location = new Point(813, 518);
            numberTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(numberTextBox);

            complementTextBox = new RoundedTextBox(customerDTO.Address.Complement == null ? "Complement" : customerDTO.Address.Complement, 264, 60);
            complementTextBox.Location = new Point(354, 606);
            complementTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(complementTextBox);

            cityTextBox = new RoundedTextBox(customerDTO.Address.City, 286, 60);
            cityTextBox.Location = new Point(640, 606);
            cityTextBox.Enabled = false;
            cityTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cityTextBox);

        }

        private void InitLabel()
        {
            Label greeting = new Label();
            greeting.Text = $"Olá, {UserService.GetUserFullNameById(this.userId)}";
            greeting.Location = new Point(140, 67);
            greeting.AutoSize = true;
            greeting.ForeColor = Color.White;
            greeting.Font = new Font("Ubuntu", 32);

            this.Controls.Add(greeting);
        }

        private void InitLogo()
        {
            Logo logo = new Logo();
            logo.Location = new Point(1033, 93);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }

        private void CustomerDataPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}