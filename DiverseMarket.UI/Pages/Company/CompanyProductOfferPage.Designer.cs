
using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Company
{
    partial class CompanyProductOfferPage
    {
        private System.ComponentModel.IContainer components = null;
        private long _userId;

        private Button profileButton, homepageButton;
        private List<ProductCard> productCards;
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
            //InitLogo();
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
                List<ProductCard> productsAfterSearch = new List<ProductCard>(this.productCards);

                foreach (var product in this.productCards)
                {

                    if (!product.name.Text.Contains(this.searchBar.Text(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        productsAfterSearch.Remove(product);
                    }
                }

                ReloadProducts(productsAfterSearch);
            }
            else
                ReloadProducts(this.productCards);
        }
        private void ReloadProducts(List<ProductCard> products)
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

            int x = 8;
            int y = 17;

            this.productCards = new List<ProductCard>();

            foreach (var productOfferBasicInfoDTO in productOfferBasicInfoDTOs)
            {
                ProductCard productCard = new ProductCard("teste", "teste",
                    "teste", Convert.ToDouble(productOfferBasicInfoDTO.Price));
                productCard.Location = new Point(x, y);
                productCard.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new CompanySpecificProductOfferPage(productOfferBasicInfoDTO.Id, this._userId).Show();
                    this.Hide();
                });

                this.productCards.Add(productCard);

                this.productsPanel.Controls.Add(productCard);

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
        }
        //private void InitLogo()
        //{
        //    this.Icon = new Icon(@"Resources\icon.ico");

        //    Logo logo = new Logo();
        //    logo.Location = new Point(1033, 93);
        //    logo.Width = 192;
        //    logo.Height = 22;

        //    this.Controls.Add(logo);
        //}
        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}