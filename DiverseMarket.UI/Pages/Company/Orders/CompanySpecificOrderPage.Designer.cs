using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Pages.Company;
using DiverseMarket.UI.Styles;


namespace DiverseMarket.UI.Pages.Company
{
    partial class CompanySpecificOrderPage
    {
        private long orderId;
        private DateTime date;
        private decimal totalAmount;
        private long customerId;
        private long companyId;
        private long userId;
        private OrderBasicInfoDTO order;

        private System.ComponentModel.IContainer components = null;
        private Button homepageButton, returnButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void InitializeComponent(OrderBasicInfoDTO order, long userId)
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += _FormClosed;
            this.orderId = order.Id;
            this.date = order.Date;
            this.totalAmount = order.TotalAmount;
            this.customerId = order.CustomerId;
            this.companyId = order.CompanyId;
            this.userId = userId;
            this.order = order;
            InitScreen();
        }

        private void InitScreen()
        {
            InitLogo();
            InitLabel();
            InitButtons();
            InitOrderDetails(this.order);
        }

        private void InitLabel()
        {
            Label pageTitle = new Label();
            pageTitle.Text = "Detalhes do Pedido";
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
            logo.Location = new Point(820, 77);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }

        private void InitOrderDetails(OrderBasicInfoDTO order)
        {
            try
            {
                OrderSpecificDetailsDTO orderDetails = OrderService.GetOrderDetails(order);

                if (orderDetails != null)
                {
                    OrderDetailCompanyCard orderDetailsCard = new OrderDetailCompanyCard(orderDetails);
                    orderDetailsCard.Location = new Point((Width - orderDetailsCard.Width) / 2, 200);
                    this.Controls.Add(orderDetailsCard);
                }
                else
                {
                    throw new Exception("Não foi possível obter os detalhes do pedido.");
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
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
                new CompanyOrderPage(this.userId).Show();
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
                new CompanyOrderPage(this.userId).Show();
            });

            this.Controls.Add(returnButton);
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
