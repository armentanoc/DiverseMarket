using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Service;
using SalesApp.Infrastructure.Service;
using SalesApp.UI.Authentication;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Pages.Customer
{
    partial class HomePageCustomer
    {
        private System.ComponentModel.IContainer components = null;
        private long _userId;

        private Button cartButton, profileButton;
        private List<ProductCard> productCards;
        private Panel productsPanel;

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
            InitButtons();
            InitProducts();
        }

        private void InitProducts()
        {
            this.productsPanel = new Panel();
            this.productsPanel.Size = new Size(927, 603);
            this.productsPanel.Location = new Point(178, 188);
            this.productsPanel.BackColor = Colors.MainBackgroundColor;
            this.productsPanel.AutoScroll = true;
            this.Controls.Add(productsPanel);

            List<ProductBasicInfoDTO> productBasicInfoDTOs = ProductService.GetAllProducstBasicInfo();

            int x = 8;
            int y = 17;

            this.productCards = new List<ProductCard>();

            foreach(var productBasicInfoDTO in productBasicInfoDTOs)
            {
                ProductCard productCard = new ProductCard(productBasicInfoDTO.Name, productBasicInfoDTO.Description, 
                    productBasicInfoDTO.Category, productBasicInfoDTO.LowestPrice);
                productCard.Location = new Point(x, y);
                productCard.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new SpecificProductPage(productBasicInfoDTO.Id, this._userId).Show();
                    this.Hide();
                });

                this.productCards.Add(productCard);

                this.productsPanel.Controls.Add(productCard);

                if(x == 713)
                {
                    x = 8;
                    y += 150;
                }
                else
                    x += 235;

            }
        }

        private void InitButtons()
        {
            this.cartButton = new System.Windows.Forms.Button();
            this.cartButton.Image = Image.FromFile(@"Resources\cart.png");
            this.cartButton.Location = new Point(1147, 68);
            this.cartButton.Size = new Size(28, 28);
            this.cartButton.BackColor = Color.Transparent;
            this.cartButton.FlatAppearance.BorderSize = 0;
            this.cartButton.FlatStyle = FlatStyle.Flat;
            this.cartButton.Cursor = Cursors.Hand;
            this.cartButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                new CartPage().Show();
                this.Hide();
            });

            this.Controls.Add(this.cartButton);

            this.profileButton = new System.Windows.Forms.Button();
            this.profileButton.Image = Image.FromFile(@"Resources\profile.png");
            this.profileButton.Location = new Point(1207, 68);
            this.profileButton.Size = new Size(28, 28);
            this.profileButton.BackColor = Color.Transparent;
            this.profileButton.FlatAppearance.BorderSize = 0;
            this.profileButton.FlatStyle = FlatStyle.Flat;
            this.profileButton.Cursor = Cursors.Hand;
            this.profileButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                new CustomerProfilePage(this._userId).Show();
                this.Hide();
            });

            this.Controls.Add(this.profileButton);
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(544, 46);
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