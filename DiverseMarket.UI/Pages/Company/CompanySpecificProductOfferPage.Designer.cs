using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Authentication;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;
using DiverseMarket.UI.Util;
using System.Text;

namespace DiverseMarket.UI.Pages.Company
{
    partial class CompanySpecificProductOfferPage
    {
        private System.ComponentModel.IContainer components = null;
        private ProductOfferCompleteInfoDTO _completeProductOffer;

        //private Label nameWarningLabel;
        //private Label descriptionWarningLabel;
        //private Label categoryWarningLabel;
        //private Label quantityWarningLabel;
        //private Label priceWarningLabel;

        private RoundedTextBox nameTextBox, descriptionTextBox, 
            categoryTextBox, quantityTextBox, priceTextBox;

        private Button homepageButton, returnButton, editButton, deleteButton;
        private long _userId;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent(ProductOfferCompleteInfoDTO completeProductOffer, long userId)
        {
            this._completeProductOffer = completeProductOffer;
            this._userId = userId;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += _FormClosed;
            InitScreen();
        }

        private void InitScreen()
        {
            InitLogo();
            InitLabel();
            InitTextBoxes();
            InitButtons();
        }

        private void InitTextBoxes()
        {
            this.Select(false, false);

            nameTextBox = new RoundedTextBox(_completeProductOffer.Name, 572, 60);
            nameTextBox.Location = new Point(354, 200);
            nameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(nameTextBox);

            descriptionTextBox = new RoundedTextBox(_completeProductOffer.Description, 572, 60);
            descriptionTextBox.Location = new Point(354, 288);
            descriptionTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(descriptionTextBox);

            //entre 354 e 926
            //242
            //padding 30
            //106

            //categoryTextBox = new RoundedTextBox(_completeProductOffer.Category, 342, 60);
            //categoryTextBox.Location = new Point(354, 376);
            //categoryTextBox.TextBox.Font = new Font("Ubuntu", 10);
            //this.Controls.Add(categoryTextBox);

            quantityTextBox = new RoundedTextBox(_completeProductOffer.Quantity.ToString(), 572, 60);
            quantityTextBox.Location = new Point(354, 376);
            quantityTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(quantityTextBox);

            priceTextBox = new RoundedTextBox($"R${_completeProductOffer.Price.ToString("N2")}", 572, 60);
            priceTextBox.Location = new Point(354, 464);
            priceTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(priceTextBox);

        }

        private void InitLabel()
        {
            Label pageTitle = new Label();
            pageTitle.Text = "Detalhes do pedido";
            pageTitle.Location = new Point(140, 67);
            pageTitle.AutoSize = true;
            pageTitle.ForeColor = Color.White;
            pageTitle.Font = new Font("Ubuntu", 32);
            this.Controls.Add(pageTitle);
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(820, 77);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }

        private void InitButtons()
        {
            this.homepageButton = new System.Windows.Forms.Button();
            this.homepageButton.Location = new Point(37, 67);
            this.homepageButton.Size = new Size(79, 71);
            this.homepageButton.FlatStyle = FlatStyle.Flat;
            this.homepageButton.FlatAppearance.BorderSize = 0;
            this.homepageButton.Cursor = Cursors.Hand;
            this.homepageButton.Image = Image.FromFile(@"Resources\logo-reduzida.png");
            this.homepageButton.BackgroundImageLayout = ImageLayout.Zoom;
            this.homepageButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new HomePageCompany(this._userId).Show();
            });

            this.Controls.Add(homepageButton);

            this.editButton = new RoundedButton("Editar", 150, 57, Colors.SecondaryButton, 32);
            this.editButton.Location = new System.Drawing.Point(475, 552);
            this.editButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.editButton.Cursor = Cursors.Hand;
            });


            this.deleteButton = new RoundedButton("Excluir", 150, 57, Colors.SecondaryButton, 32);
            this.deleteButton.Location = new System.Drawing.Point(655, 552);
            this.deleteButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.deleteButton.Cursor = Cursors.Hand;
            });

            this.Controls.Add(deleteButton);

            this.Controls.Add(editButton);

            this.returnButton = new RoundedButton("Voltar", 150, 57, Colors.SecondaryButton, 32);
            this.returnButton.Location = new System.Drawing.Point(1080, 57);
            this.returnButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.returnButton.Cursor = Cursors.Hand;
            });
            this.returnButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CompanyProductOfferPage(this._userId).Show();
            });

            this.Controls.Add(returnButton);

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    string name = this.nameTextBox.TextBox.Text;
                    string description = this.descriptionTextBox.TextBox.Text;
                    string quantity = this.quantityTextBox.TextBox.Text;
                    //string category = this.categoryTextBox.TextBox.Text;
                    decimal price = Convert.ToDecimal(this.priceTextBox.TextBox.Text);

                    //AddressDTO address = new AddressDTO(this.cepTextBox.TextBox.Text,
                    //    this.streetTextBox.TextBox.Text,
                    //    (this.complementTextBox.TextBox.Text.Equals("Complemento") ? null : this.complementTextBox.TextBox.Text),
                    //    addressNeighborhood,
                    //    this.cityTextBox.TextBox.Text,
                    //    this.numberTextBox.TextBox.Text);
                    //string password = this.passwordTextBox.TextBox.Text;

                    //LoginResponseDTO response = AuthenticationService.RegisterCustomer(
                    //    new RegisterCustomerDTO(fullName, email, username, telephone, CPF, address, password));
                    //if (response.Id != null)
                    //{
                    //    MessageBox.Show("Conta criada com sucesso.");
                    //    new LoginPage().Show();
                    //    this.Hide();

                }
                else
                    MessageBox.Show("Houve um erro, tente novamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private bool CheckFields()
        {
            List<string> wrongFields = new List<string>();

            string name = this.nameTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidName(name, _completeProductOffer.Name, true))
                wrongFields.Add("name");

            string description = this.descriptionTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidName(description, _completeProductOffer.Description, true))
                wrongFields.Add("description");

            //string category = this.descriptionTextBox.TextBox.Text;

            //if (!ValidationUtils.IsInputAValidName(description, _completeProductOffer.Category, false))
            //    wrongFields.Add("category");

            string quantity = this.quantityTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidLong(quantity, _completeProductOffer.Quantity.ToString(), false))
                wrongFields.Add("quantity");

            string price = this.priceTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidDecimal(price, $"R${_completeProductOffer.Price.ToString("N2")}", false))
                wrongFields.Add("price");

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

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}