using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Service;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Pages.Customer
{
    partial class CustomerProfilePage
    {
        private System.ComponentModel.IContainer components = null;

        private long userId;

        private Button homepageButton, updateDataButton, refundButton, ordersButton, walletButton;
       
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

            this.updateDataButton = new System.Windows.Forms.Button();
            this.updateDataButton.Location = new Point(46, 251);
            this.updateDataButton.Size = new Size(368, 252);
            this.updateDataButton.FlatStyle = FlatStyle.Flat;
            this.updateDataButton.FlatAppearance.BorderSize = 0;
            this.updateDataButton.Cursor = Cursors.Hand;
            this.updateDataButton.Image = Image.FromFile(@"Resources\atualizar-meus-dados.png");
            this.updateDataButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CustomerDataPage(this.userId).Show();
            });

            this.Controls.Add(updateDataButton);

            this.refundButton = new System.Windows.Forms.Button();
            this.refundButton.Location = new Point(456, 251);
            this.refundButton.Size = new Size(368, 252);
            this.refundButton.FlatStyle = FlatStyle.Flat;
            this.refundButton.FlatAppearance.BorderSize = 0;
            this.refundButton.Cursor = Cursors.Hand;
            this.refundButton.Image = Image.FromFile(@"Resources\meus-reembolsos.png");
            this.refundButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CustomerRefundPage(this.userId).Show();
            });

            this.Controls.Add(refundButton);

            this.ordersButton = new System.Windows.Forms.Button();
            this.ordersButton.Location = new Point(866, 251);
            this.ordersButton.Size = new Size(368, 252);
            this.ordersButton.FlatStyle = FlatStyle.Flat;
            this.ordersButton.FlatAppearance.BorderSize = 0;
            this.ordersButton.Cursor = Cursors.Hand;
            this.ordersButton.Image = Image.FromFile(@"Resources\meus-pedidos.png");
            this.ordersButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CustomerOrdersPage(this.userId).Show();
            });

            this.Controls.Add(ordersButton);

            this.walletButton = new System.Windows.Forms.Button();
            this.walletButton.Location = new Point(46, 541);
            this.walletButton.Size = new Size(368, 252);
            this.walletButton.FlatStyle = FlatStyle.Flat;
            this.walletButton.FlatAppearance.BorderSize = 0;
            this.walletButton.Cursor = Cursors.Hand;
            this.walletButton.Image = Image.FromFile(@"Resources\minha-carteira.png");
            this.walletButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                this.Hide();
                new CustomerWalletHistoryPage(this.userId).Show();
            });

            this.Controls.Add(walletButton);

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
            Logo logo = new Logo();
            logo.Location = new Point(1033, 93);
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