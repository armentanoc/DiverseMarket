using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Authentication;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Pages.Customer;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Company
{
    partial class HomePageCompany
    {
        private System.ComponentModel.IContainer components = null;

        private long userId;

        private Button homepageButton, productsButton, ordersButton, logoutButton;

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
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += CustomerProfilePage_FormClosed;
            InitScreen();
        }
        private void InitScreen()
        {
            InitLogo();
            InitLabels();
            InitButtons();
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
                new HomePageCustomer(this.userId).Show();
            });

            this.Controls.Add(homepageButton);

            this.productsButton = new RoundedButton("Listar Produtos", 300, 57, Colors.SecondaryButton, 32);
            this.productsButton.Location = new System.Drawing.Point(50, 300);
            this.productsButton.FlatStyle = FlatStyle.Flat;
            this.productsButton.FlatAppearance.BorderSize = 0;
            this.productsButton.Cursor = Cursors.Hand;
            this.productsButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CompanyProductOfferPage(this.userId).Show();
            });

            this.Controls.Add(productsButton);

            this.ordersButton = new RoundedButton("Listar Pedidos", 300, 57, Colors.SecondaryButton, 32);
            this.ordersButton.Location = new System.Drawing.Point(370, 300);
            this.ordersButton.FlatStyle = FlatStyle.Flat;
            this.ordersButton.FlatAppearance.BorderSize = 0;
            this.ordersButton.Cursor = Cursors.Hand;
            this.ordersButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CompanyOrderPage(this.userId).Show();
            });

            this.Controls.Add(ordersButton);

            this.logoutButton = new RoundedButton("Sair", 150, 67, Colors.SecondaryButton, 32);
            this.logoutButton.Location = new System.Drawing.Point(1080, 57);
            this.logoutButton.MouseEnter += new EventHandler((object sender, EventArgs e) =>
            {
                this.logoutButton.Cursor = Cursors.Hand;
            });
            this.logoutButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new LoginPage().Show();
            });

            this.Controls.Add(logoutButton);

        }

        private void InitLabels()
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
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(820, 77);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }

        private void CustomerProfilePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}