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