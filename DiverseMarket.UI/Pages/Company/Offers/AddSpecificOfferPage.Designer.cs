using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.Logger;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Messages;
using DiverseMarket.UI.Styles;
using DiverseMarket.UI.Util;
using System.Globalization;

namespace DiverseMarket.UI.Pages.Company
{
    partial class AddSpecificOfferPage
    {
        private System.ComponentModel.IContainer components = null;
        private ProductOfferCompleteInfoDTO _completeProductOffer;

        private long _userId;

        #region Warning Labels
        //private Label nameWarningLabel;
        //private Label descriptionWarningLabel;
        //private Label categoryWarningLabel;
        //private Label quantityWarningLabel;
        //private Label priceWarningLabel;
        #endregion

        #region RoundedTextBoxes
        private RoundedTextBox nameTextBox;
        private RoundedTextBox descriptionTextBox;
        private RoundedTextBox categoryTextBox;
        private RoundedTextBox quantityTextBox;
        private RoundedTextBox priceTextBox;

        #endregion

        #region Buttons
        private Button homepageButton;
        private Button returnButton;
        private Button newButton;
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Init Components
        private void InitializeComponent(long userId)
        {
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
            InitTextBoxes();
            InitLabel();
            InitButtons();
        }

        #endregion

        #region Text Boxes
        private void InitTextBoxes()
        {
            int y = 200;
            int spacing = 50;

            this.Select(false, false);

            nameTextBox = new RoundedTextBox(string.Empty, 572, 40);
            nameTextBox.Location = new Point(354, y);
            nameTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(nameTextBox);

            descriptionTextBox = new RoundedTextBox(string.Empty, 572, 40);
            descriptionTextBox.Location = new Point(354, nameTextBox.Bottom + spacing);
            descriptionTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(descriptionTextBox);

            #region Category Text Box
            //entre 354 e 926
            //242
            //padding 30
            //106

            //categoryTextBox = new RoundedTextBox(_completeProductOffer.Category, 342, 40);
            //categoryTextBox.Location = new Point(354, 336);
            //categoryTextBox.TextBox.Font = new Font("Ubuntu", 10);
            //this.Controls.Add(categoryTextBox);

            #endregion

            quantityTextBox = new RoundedTextBox(string.Empty.ToString(), 572, 40);
            quantityTextBox.Location = new Point(354, descriptionTextBox.Bottom + spacing);
            quantityTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(quantityTextBox);

            priceTextBox = new RoundedTextBox(string.Empty, 572, 40); ;
            priceTextBox.Location = new Point(354, quantityTextBox.Bottom + spacing);
            priceTextBox.TextBox.Font = new Font("Ubuntu", 10);
            this.Controls.Add(priceTextBox);

        }

        #endregion

        #region Labels
        private void InitLabel()
        {
            int spacing = 35;

            Label pageTitle = new Label();
            pageTitle.Text = "Detalhes do pedido";
            pageTitle.Location = new Point(140, 67);
            pageTitle.AutoSize = true;
            pageTitle.ForeColor = Color.White;
            pageTitle.Font = new Font("Ubuntu", 32);
            this.Controls.Add(pageTitle);

            Label nameLabel = new Label();
            nameLabel.Text = "Nome";
            nameLabel.Location = new Point(354, nameTextBox.Top - spacing);
            nameLabel.ForeColor = Color.White;
            nameLabel.Font = new Font("Ubuntu", 12);
            this.Controls.Add(nameLabel);

            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Descrição";
            descriptionLabel.Location = new Point(354, descriptionTextBox.Top - spacing);
            descriptionLabel.ForeColor = Color.White;
            descriptionLabel.Font = new Font("Ubuntu", 12);
            this.Controls.Add(descriptionLabel);

            Label quantityLabel = new Label();
            quantityLabel.Text = "Quantidade";
            quantityLabel.Location = new Point(354, quantityTextBox.Top - spacing);
            quantityLabel.ForeColor = Color.White;
            quantityLabel.Font = new Font("Ubuntu", 12);
            this.Controls.Add(quantityLabel);

            Label priceLabel = new Label();
            priceLabel.Text = "Preço";
            priceLabel.Location = new Point(354, priceTextBox.Top - spacing);
            priceLabel.ForeColor = Color.White;
            priceLabel.Font = new Font("Ubuntu", 12);
            this.Controls.Add(priceLabel);
        }

        #endregion

        #region Logo
        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(820, 77);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }
        #endregion

        #region Buttons
        private void InitButtons()
        {

            int spacing = 50;

            #region HomePage Button
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

            #endregion

            this.newButton = new RoundedButton("Adicionar", 150, 40, Colors.CallToActionButton, 32);
            this.newButton.Location = new System.Drawing.Point(475, priceTextBox.Bottom + spacing);
            this.newButton.Cursor = Cursors.Hand;
            this.newButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                newButton_Click(sender, e);
            });

            this.Controls.Add(newButton);

            #region Return Button

            this.returnButton = new RoundedButton("Voltar", 150, 57, Colors.SecondaryButton, 32);
            this.returnButton.Location = new System.Drawing.Point(1080, 57);
            this.returnButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.returnButton.Cursor = Cursors.Hand;
            });
            this.returnButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CompanyOfferPage(this._userId).Show();
            });

            this.Controls.Add(returnButton);

            #endregion
        }

        #endregion

        #region Clicks
        private void newButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AreFieldsValid())
                {
                    var offer = _completeProductOffer;

                    string cleanedPrice = ValidationUtils.CleanMonetaryInput(this.priceTextBox.TextBox.Text);

                    var newName = this.nameTextBox.TextBox.Text;
                    var newDescription = this.descriptionTextBox.TextBox.Text;
                    var newPrice = decimal.Parse(cleanedPrice);
                    var newQuantity = long.Parse(this.quantityTextBox.TextBox.Text);
                    var newCategory = "teste";

                    //var newProductOffer = new ProductOfferCompleteInfoDTO(
                    //    this._userId,
                    //    offer.ProductId,
                    //    newPrice,
                    //    newQuantity,
                    //    newName,
                    //    newCategory,
                    //    newDescription
                    //);

                    //bool wasUpdateSuccessful = ProductService.UpdateProductOfferByCompleteInfoDTO(newProductOffer);

                    //if (wasUpdateSuccessful)
                    //{
                    //    MessageBoxUtils.ShowMessageBox("Produto atualizado com sucesso!", MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBoxUtils.ShowMessageBox("Falha ao atualizar o produto. Tente novamente.", MessageBoxIcon.Error);
                    //}
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxUtils.ConfirmAction("Você tem certeza que deseja excluir essa oferta de produto?"))
                {
                    if (ProductService.DeleteCompanyProductOfferByCompleteInfoDTO(_completeProductOffer))
                    {
                        MessageBoxUtils.ShowMessageBox("Oferta de produto excluída com sucesso.", MessageBoxIcon.Information);
                        this.Hide();
                        new CompanyOfferPage(this._userId).Show();
                    }
                    else
                    {
                        MessageBoxUtils.ShowMessageBox("Falha em excluir a oferta de produto. \nPor favor, tente novamente.", MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBoxUtils.ShowMessageBox("Operação cancelada. O produto não será excluído", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }


        #endregion

        #region Validations
        private bool AreFieldsValid()
        {
            var wrongFields = new List<string>();

            #region Other Checks to Implement
            //string name = this.nameTextBox.TextBox.Text;

            //if (!ValidationUtils.IsInputAValidName(name, _completeProductOffer.Name, true))
            //    wrongFields.Add("name");

            //string description = this.descriptionTextBox.TextBox.Text;

            //if (!ValidationUtils.IsInputAValidName(description, _completeProductOffer.Description, true))
            //    wrongFields.Add("description");

            //string category = this.descriptionTextBox.TextBox.Text;

            //if (!ValidationUtils.IsInputAValidName(description, _completeProductOffer.Category, false))
            //    wrongFields.Add("category");
            #endregion

            string quantity = this.quantityTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidLong(quantity, _completeProductOffer.Quantity, false))
                wrongFields.Add($"quantidade - {quantity}");

            string priceInput = this.priceTextBox.TextBox.Text;

            if (!ValidationUtils.IsInputAValidDecimal(priceInput, _completeProductOffer.Price, false))
                wrongFields.Add($"preço - {priceInput}");

            bool validInputs = wrongFields.Count == 0;

            if (!validInputs)
            {
                string wrongFieldsStr = $"{string.Join(", ", wrongFields)}";
                MessageBox.Show("Falha ao atualizar o produto. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new ArgumentException($"CheckFields retornou falso: ({wrongFieldsStr})");
            }

            return validInputs;
        }
        #endregion

        #region Form Closed
        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}