using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Pages.Company;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Company
{
    partial class CompanyOrderPage
    {
        private System.ComponentModel.IContainer components = null;
        private long userId;
        private Button homepageButton, returnButton;
        //private Label? orderInfoLabel;
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
 
            //orderInfoLabel = new Label();
            //orderInfoLabel.Location = new Point(10, 800);
            //orderInfoLabel.AutoSize = true;
            //orderInfoLabel.ForeColor = Color.White;
            //this.Controls.Add(orderInfoLabel);


            InitOrders();
        }

        private void InitOrders()
        {
            try
            {
                List<OrderBasicInfoDTO> ordersDTO = OrderService.GetAllOrdersByCompanyUserId(this.userId);

                Panel container = new Panel();
                container.Size = new Size(1188, 568);
                container.Location = new Point(178, 188);
                container.BackColor = Colors.MainBackgroundColor;
                container.AutoScroll = true;
                Controls.Add(container);

                int cardsPerRow = 4; 
                int cardWidth = 236;
                int cardHeight = 106;
                int spacingX = 15;
                int spacingY = 20;

                int totalWidth = cardsPerRow * cardWidth + (cardsPerRow - 1) * spacingX;
                int initialX = (container.Width - totalWidth) / 2;

                int x = initialX, y = spacingY;

                if (ordersDTO != null)
                {
                    foreach (var order in ordersDTO)
                    {
                        OrderCard orderCard = new OrderCard(order.Id, order.Date, order.TotalAmount, order.CustomerId, order.CompanyId);
                        orderCard.Location = new Point(x, y);
                        orderCard.Click += new EventHandler((object sender, EventArgs e) =>
                        {
                            var specificOrderPage = new CompanySpecificOrderPage(order, this.userId);
                            specificOrderPage.Show();
                            this.Hide();
                        });

                        container.Controls.Add(orderCard);

                        if (x == initialX + (cardsPerRow - 1) * (cardWidth + spacingX))
                        {
                            x = initialX;
                            y += cardHeight + spacingY;
                        }
                        else
                            x += cardWidth + spacingX;
                    }
                    //orderInfoLabel.Text = $"Número de pedidos: {ordersDTO.Count}; UserId: {this.userId}. Tipo de ordersDTO: {ordersDTO.GetType}";
                }
                else
                {
                    //orderInfoLabel.Text = $"ordersDTO está nulo: {ordersDTO is null}; UserId: {this.userId}. ";
                }
            }
            catch (Exception ex)
            {
                //orderInfoLabel.Text = $"Erro ao obter pedidos: {ex.Message}";
                Console.WriteLine("Eoor: " + ex.ToString());
            }
        }


        private void InitLabel()
        {
            Label pageTitle = new Label();
            pageTitle.Text = "Pedidos";
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
                new HomePageCompany(this.userId).Show();
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
                new HomePageCompany(this.userId).Show();
            });

            this.Controls.Add(returnButton);
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}