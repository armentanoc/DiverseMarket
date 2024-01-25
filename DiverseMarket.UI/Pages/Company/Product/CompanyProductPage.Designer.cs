
using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.Logger;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Pages.Customer;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Company
{
    partial class CompanyProductPage
    {
        private System.ComponentModel.IContainer components = null;
        private long _userId;

        private Button profileButton, homepageButton, returnButton, addNewOfferButton;
        private List<CompanyProductCard> companyProductCards;
        private Panel productsPanel;
        private SearchBar searchBar;

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


        private void InitializeComponent(long userId)
        {
            this._userId = userId;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            KeyPreview = true;
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += HomePage_FormClosed;
            InitScreen();
        }
        private void InitScreen()
        {
            InitLogo();
            InitLabel();
            InitSearchBar();
            InitButtons();
            InitProducts();
        }

        #region Search Bar
        private void InitSearchBar()
        {
            searchBar = new SearchBar();
            searchBar.Location = new Point(210, 162);
            searchBar.SearchButton.Click += new EventHandler(searchButton_Click);
            this.Controls.Add(searchBar);
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchBarWorks();
        }

        private void SearchBarWorks()
        {
            if (this.searchBar.Text() != "Pesquisar")
            {
                var productsAfterSearch = new List<CompanyProductCard>(this.companyProductCards);

                foreach (var product in this.companyProductCards)
                {

                    if (!product.name.Text.Contains(this.searchBar.Text(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        productsAfterSearch.Remove(product);
                    }
                }

                ReloadProducts(productsAfterSearch);
            }
            else
                ReloadProducts(this.companyProductCards);
        }

        private void ReloadProducts(List<CompanyProductCard> products)
        {
            ClearProducts();

            int x = 8;
            int y = 17;

            foreach (var product in products)
            {
                product.Location = new Point(x, y);

                product.Visible = true;
                this.productsPanel.Controls.Add(product);
                product.BringToFront();

                if (x == 713)
                {
                    x = 8;
                    y += 150;
                }
                else
                    x += 235;

            }
            productsPanel.Invalidate();
            productsPanel.Update();
        }
        private void ClearProducts()
        {
            foreach (Control control in this.productsPanel.Controls.OfType<Control>().ToList())
            {
                productsPanel.Controls.Remove(control);
            }

        }

        #endregion

        #region Products
        private void InitProducts()
        {
            this.productsPanel = new Panel();
            this.productsPanel.Size = new Size(927, 603);
            this.productsPanel.Location = new Point(178, 226);
            this.productsPanel.BackColor = Colors.MainBackgroundColor;
            this.productsPanel.AutoScroll = true;
            this.Controls.Add(productsPanel);

            List<ProductBasicInfoDTO> productBasicInfoDTOs = ProductService.GetAllProducstBasicInfo();

            int x = 8;
            int y = 17;

            this.companyProductCards = new List<CompanyProductCard>();

            foreach (var productBasicInfoDTO in productBasicInfoDTOs)
            {
                var productCard = 
                    new CompanyProductCard
                    (
                        productBasicInfoDTO.Name, 
                        productBasicInfoDTO.Description,
                        productBasicInfoDTO.Category
                        );

                productCard.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                
                if (y + productCard.Height > this.productsPanel.Height)
                {
                    x += 235;
                    y = 17;
                }

                productCard.Location = new Point(x, y);

                productCard.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new AddSpecificProductPage(productBasicInfoDTO, this._userId).Show();
                    this.Hide();
                });

                this.companyProductCards.Add(productCard);
                this.productsPanel.Controls.Add(productCard);
                y += productCard.Height + 10;
            }

            new LogMessage($"Number of productCards: {this.companyProductCards.Count}");
        }
        private int CalculateTotalHeight(List<ProductOfferCompleteInfoDTO> productOfferCompleteInfoDTOs)
        {
            int cardHeight = 90; 
            int spacing = 10; 
            int rowCount = (int)Math.Ceiling((double)productOfferCompleteInfoDTOs.Count / 3); 
            return rowCount * (cardHeight + spacing) + spacing; 
        }

        #endregion

        #region Labels
        private void InitLabel()
        {
            Label pageTitle = new Label();
            pageTitle.Text = "Produtos";
            pageTitle.Location = new Point(140, 67);
            pageTitle.AutoSize = true;
            pageTitle.ForeColor = Color.White;
            pageTitle.Font = new Font("Ubuntu", 32);

            this.Controls.Add(pageTitle);
        }

        #endregion

        #region Buttons
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

            this.returnButton = new RoundedButton("Voltar", 150, 57, Colors.SecondaryButton, 32);
            this.returnButton.Location = new System.Drawing.Point(1080, 57);
            this.returnButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.returnButton.Cursor = Cursors.Hand;
            });
            this.returnButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new HomePageCompany(this._userId).Show();
            });

            this.Controls.Add(returnButton);

            this.addNewOfferButton = new RoundedButton("Novo Produto", 150, 57, Colors.SecondaryButton, 32);
            this.addNewOfferButton.Location = new System.Drawing.Point(900, 57);
            this.addNewOfferButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.addNewOfferButton.Cursor = Cursors.Hand;
            });
            this.addNewOfferButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new AddNewProductPage(this._userId).Show();
            });

            this.Controls.Add(addNewOfferButton);
        }

        #endregion

        #region Logo
        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(640, 77);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }

        #endregion

        #region Override to Allow Search By Enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SearchBarWorks();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Form Closed
        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}