using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;
using System.ComponentModel;

using DiverseMarket.Backend.Services;

namespace DiverseMarket.UI.Pages.Customer
{
    partial class RequestRefundPage
    {
        
        private System.ComponentModel.IContainer components = null;

        private long customerId;
        private long orderItemId;

        private Button homepageButton;

        private Panel refundContainer;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent(long customerId, long orderItemId)
        {
            this.customerId = customerId;
            this.orderItemId = orderItemId;

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
            InitContainer();
            InitLabel();
            InitRequestRefundForm();
            InitButtons();
        }

        private void InitRequestRefundForm()
        {
            OrderDetailsForRefundCard orderDetailsForRefundCard = new OrderDetailsForRefundCard(this.orderItemId, 
                CompanyService.GetCompanyNameBySellingItemId(orderItemId), UserService.GetUserFullNameById(this.customerId), 
                UserService.GetAddressByUserId(this.customerId), OrderService.GetOrderItemStatusById(this.orderItemId));
            orderDetailsForRefundCard.Location = new Point(27, 19);
            this.Controls.Add(orderDetailsForRefundCard);

            RequestRefundProductCard productCard = new RequestRefundProductCard(ProductService.GetProductNameByOrderItemId(this.orderItemId), );

        }

        private void InitContainer()
        {
            this.refundContainer = new Panel();
            this.refundContainer.Size = new Size(1216, 566);
            this.refundContainer.Location = new Point(28, 155);
            this.refundContainer.BackColor = Colors.MainBackgroundColor;
            this.refundContainer.AutoScroll = true;

            Controls.Add(this.refundContainer);
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
                new HomePageCustomer(this.customerId).Show();
            });

            this.Controls.Add(homepageButton);
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}