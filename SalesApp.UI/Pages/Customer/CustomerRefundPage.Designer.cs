using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Service;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Pages.Customer
{
    partial class CustomerRefundPage
    {
        private System.ComponentModel.IContainer components = null;

        private long userId;
        private Button homepageButton;
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
            FormClosed += _FormClosed;
            InitScreen();
        }

        private void InitScreen()
        {
            InitLogo();
            InitLabel();
            InitButtons();
            InitRefunds();
        }

        private void InitRefunds()
        {
            List<RefundBasicInfoDTO> refundsDTO = RefundService.GetAllRefundsByUserId(this.userId);

            Panel container = new Panel();
            container.Size = new Size(1188, 568);
            container.Location = new Point(178, 188);
            container.BackColor = Colors.MainBackgroundColor;
            container.AutoScroll = true;
            Controls.Add(container);

            int x = 15, y = 12;

            foreach(var refund in refundsDTO)
            {
                RefundCard refundCard = new RefundCard(refund.Id, refund.ProductName, refund.CompanyName, refund.Status, refund.TotalAmount);
                refundCard.Location = new Point(x, y);
                refundCard.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new CustomerSpecificRefundPage(refund.Id).Show();
                    this.Hide();
                });

                this.Controls.Add(refundCard);

                if (x == 959)
                {
                    x = 15;
                    y = 152;
                }
                else
                    x += 236;
            }
        }

        private void InitLabel()
        {
            Label pageTitle = new Label();
            pageTitle.Text = "Reembolsos";
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
            logo.Location = new Point(1033, 93);
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
                new HomePageCustomer(this.userId).Show();
            });

            this.Controls.Add(homepageButton);
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}