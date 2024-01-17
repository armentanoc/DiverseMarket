using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Moderator.Company
{
    partial class SpecificCompanyPage
    {
        private System.ComponentModel.IContainer components = null;

        private Label emailWarningLabel;
        private Label cepWarningLabel;
        private Label cnpjWarningLabel;
        private RoundedTextBox nameTextBox, emailTextBox, tradeNameTextBox, telephoneTextBox, cnpjTextBox, cepTextBox, streetTextBox,
            numberTextBox, complementTextBox, cityTextBox;
        private Button editButton;
        private RoundedButton deleteButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent(long id, string cnpj, string corporateName)
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += SpecificCompanyPage_FormClosed;
            InitScreen();
        }

        private void InitScreen()
        {
            InitLogo();
            InitLabels();
            InitTextBoxes();
            InitButtons();
        }

        private void InitTextBoxes()
        {
            nameTextBox = new RoundedTextBox("Razão social*", 572, 60);
            nameTextBox.Location = new Point(354, 166);
            nameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(nameTextBox);

            emailTextBox = new RoundedTextBox("E-mail*", 572, 60);
            emailTextBox.Location = new Point(354, 254);
            emailTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(emailTextBox);

            tradeNameTextBox = new RoundedTextBox("Nome fantasia*", 342, 60);
            tradeNameTextBox.Location = new Point(354, 342);
            tradeNameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(tradeNameTextBox);

            telephoneTextBox = new RoundedTextBox("Telefone", 205, 60);
            telephoneTextBox.Location = new Point(721, 342);
            telephoneTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(telephoneTextBox);

            cnpjTextBox = new RoundedTextBox("CNPJ*", 342, 60);
            cnpjTextBox.Location = new Point(354, 430);
            cnpjTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(cnpjTextBox);

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


        }

        private void InitLabels()
        {
            emailWarningLabel = new Label();
            emailWarningLabel.Text = "E-mail inválido ou em uso.";
            emailWarningLabel.ForeColor = Color.White;
            emailWarningLabel.AutoSize = true;
            emailWarningLabel.Font = new Font("Ubuntu", 6);
            emailWarningLabel.BackColor = Color.Transparent;
            emailWarningLabel.Location = new Point(380, 318);
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

            cnpjWarningLabel = new Label();
            cnpjWarningLabel.Text = "CNPJ inválido.";
            cnpjWarningLabel.ForeColor = Color.White;
            cnpjWarningLabel.AutoSize = true;
            cnpjWarningLabel.Font = new Font("Ubuntu", 6);
            cnpjWarningLabel.BackColor = Color.Transparent;
            cnpjWarningLabel.Location = new Point(380, 494);
            cnpjWarningLabel.Visible = false;
            this.Controls.Add(cnpjWarningLabel);

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
        private void InitButtons()
        {
            this.editButton = new RoundedButton("Editar estabelecimento", 274, 57, Colors.CallToActionButton, 32);
            this.editButton.Location = new Point(354, 715);
            this.editButton.Cursor = Cursors.Hand;
            this.editButton.Click += registerButton_Click;

            this.Controls.Add(this.editButton);


            this.deleteButton = new RoundedButton("Excluir estabelecimento", 274, 57, Colors.CallToActionButton, 32);
            this.deleteButton.Location = new Point(652, 715);
            this.deleteButton.Cursor = Cursors.Hand;
            this.deleteButton.Click += registerButton_Click;

            this.Controls.Add(this.deleteButton);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            //new CompanyRegisterMessagePage().Show();
        }

        private void SpecificCompanyPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        #endregion
    }
    }