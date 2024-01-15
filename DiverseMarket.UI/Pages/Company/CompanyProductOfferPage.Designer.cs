
using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Authentication;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Company
{
    partial class CompanyProductOfferPage
    {
        private System.ComponentModel.IContainer components = null;
        private long _userId;

        private Button profileButton, homepageButton, returnButton;
        private List<ProductOfferCard> productOfferCards;
        private Panel productsPanel;
        private SearchBar searchBar;

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
            this._userId = userId;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
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
        private void InitSearchBar()
        {
            searchBar = new SearchBar();
            searchBar.Location = new Point(184, 186);

            searchBar.SearchButton.Click += new EventHandler(searchButton_Click);

            this.Controls.Add(searchBar);
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (this.searchBar.Text() != "Pesquisar")
            {
                var productsAfterSearch = new List<ProductOfferCard>(this.productOfferCards);

                foreach (var product in this.productOfferCards)
                {

                    if (!product.name.Text.Contains(this.searchBar.Text(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        productsAfterSearch.Remove(product);
                    }
                }

                ReloadProducts(productsAfterSearch);
            }
            else
                ReloadProducts(this.productOfferCards);
        }
        private void ReloadProducts(List<ProductOfferCard> products)
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
        private void InitProducts()
        {
            this.productsPanel = new Panel();
            this.productsPanel.Size = new Size(927, 603);
            this.productsPanel.Location = new Point(178, 276);
            this.productsPanel.BackColor = Colors.MainBackgroundColor;
            this.productsPanel.AutoScroll = true;
            this.Controls.Add(productsPanel);

            List<ProductOfferBasicInfoDTO> productOfferBasicInfoDTOs = 
                ProductService.GetAllProductOffersByCompanyUserId(_userId);

            List<ProductOfferCompleteInfoDTO> productOfferCompleteInfoDTOs =
            ProductService.GetAllProductOfferInfo(productOfferBasicInfoDTOs);

            int x = 8;
            int y = 17;

            this.productOfferCards = new();

            foreach (var completeOfferDTO in productOfferCompleteInfoDTOs)
            {
                var productOfferCard = new ProductOfferCard(
                    productName: completeOfferDTO.Name,
                    description: completeOfferDTO.Description,
                    category: completeOfferDTO.Category,
                    price: completeOfferDTO.Price,
                    quantity: completeOfferDTO.Quantity
                    );

                productOfferCard.Location = new Point(x, y);
                productOfferCard.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new CompanySpecificProductOfferPage(completeOfferDTO, this._userId).Show();
                    this.Hide();
                });

                this.productOfferCards.Add(productOfferCard);

                this.productsPanel.Controls.Add(productOfferCard);

                if (x == 713)
                {
                    x = 8;
                    y += 150;
                }
                else
                    x += 235;
            }
        }

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
        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}