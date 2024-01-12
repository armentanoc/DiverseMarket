using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Service;
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Pages.Customer
{
    partial class CustomerSpecificOrderPage
    {
        private System.ComponentModel.IContainer components = null;

        private long orderId, userId;
        private Button homepageButton;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent(long orderId)
        {
            this.orderId = orderId;
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
            InitOrder();
        }

        private void InitOrder()
        {
            OrderDetailsDTO orderDetails = OrderService.GetOrderDetailsById(this.orderId);

            this.userId = orderDetails.UserId;

            OrderDetailsCard orderDetailsCard = new OrderDetailsCard(UserService.GetUserFullNameById(this.userId), orderDetails.DeliveryAddress, orderDetails.TotalAmount);
            orderDetailsCard.Location = new System.Drawing.Point(55, 174);
            this.Controls.Add(orderDetailsCard);

            Panel container = new Panel();
            container.Size = new Size(1204, 471);
            container.Location = new Point(42, 344);
            container.BackColor = Colors.MainBackgroundColor;
            container.AutoScroll = true;
            Controls.Add(container);

            int x = 9, y = 9;

            foreach (var item in orderDetails.Items)
            {
                OrderItemCard itemCard = new OrderItemCard(item, OrderService.GetOrderDateById(this.orderId).AddDays(7).Date);
                itemCard.Location = new System.Drawing.Point(x, y);

                itemCard.recievedButton.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    OrderService.SetOrderItemAsRecieved(orderId, item.Id);
                    itemCard.Recieved();
                });

                itemCard.refundButton.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new RequestRefundPage(this.userId, item.Id).Show();
                    this.Hide();
                });

                container.Controls.Add(itemCard);
                y += 238;
            }

        }

        private void InitLabel()
        {
            Label pageTitle = new Label();
            pageTitle.Text = "Detalhes do pedido";
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
                new CustomerOrdersPage(this.userId).Show();
            });

            this.Controls.Add(homepageButton);
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}