using DiverseMarket.UI.Styles;
using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Model.Enums;

namespace DiverseMarket.UI.Components
{
    internal class OrderItemCard : Panel
    {
        private OrderItemDTO item;

        public RoundedButton recievedButton, refundButton;
        public OrderItemCard(OrderItemDTO item, DateTime expectedDeliveryDate)
        {
            this.item = item;
            Width = 1169;
            Height = 151;
            BorderStyle = BorderStyle.None;
            AddProduct();
            AddCompany();
            AddLabels();
            AddRefundDate(expectedDeliveryDate);
            AddQuantityAndPrice();
            AddButtons(expectedDeliveryDate);

        }

        private void AddQuantityAndPrice()
        {
            Label label = new Label();
            label.Text = $"Quantidader: {item.Quantity} | Subtotal: R${item.UnityPrice * item.Quantity}";
            label.ForeColor = Colors.MainBackgroundColor;
            label.Font = new Font("Ubuntu", 12);
            label.Location = new Point(44, 146);
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            Controls.Add(label);
        }

        private void AddRefundDate(DateTime date)
        {
            TimeSpan timespan = DateTime.Today - date.Date;

            Label label = new Label();
            if (timespan.TotalDays < 7 && timespan.TotalDays >= 0)
                label.Text = $"Elegível para reembolso até {date.AddDays(7).Date.ToString("dd/MM/yy")}";
            else
                label.Text = $"Item não elegível para reembolso";
            label.ForeColor = Colors.MainBackgroundColor;
            label.Font = new Font("Ubuntu", 12);
            label.Location = new Point(44, 115);
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            Controls.Add(label);
        }

        private void AddCompany()
        {
            Label label = new Label();
            label.Text = $"Vendido por: {item.CompanyName}";
            label.ForeColor = Colors.MainBackgroundColor;
            label.Font = new Font("Ubuntu", 12);
            label.Location = new Point(44, 30);
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            Controls.Add(label);
        }

        private void AddProduct()
        {
            Label name = new Label();
            name.Text = item.Name;
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 12);
            name.Location = new Point(44, 23);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);

            Label description = new Label();
            description.Text = item.Description;
            description.ForeColor = Colors.MainBackgroundColor;
            description.Font = new Font("Ubuntu", 10);
            description.Location = new Point(44, 54);
            description.AutoSize = true;
            description.BackColor = Color.Transparent;
            Controls.Add(description);
        }

        public void Recieved()
        {
            recievedButton = new RoundedButton("RECEBIDO", 265, 57, ColorTranslator.FromHtml("#D2D2D2"), 32);
            recievedButton.ForeColor = ColorTranslator.FromHtml("#999999");
            recievedButton.Enabled = false;

            recievedButton.Location = new Point(875, 38);

            Controls.Add(recievedButton);
        }

        private void AddButtons(DateTime date)
        {
            bool recieved = item.Status == OrderStatus.Recieved;

            Color recieveButtonColor = ColorTranslator.FromHtml(recieved ? "#D2D2D2" : "#72B4DB");
            recievedButton = new RoundedButton("RECEBI ESTE ITEM", 265, 57, recieveButtonColor, 32);
            if (recieved)
            {
                recievedButton.ForeColor = ColorTranslator.FromHtml("#999999");
                recievedButton.Enabled = false;
            }
            recievedButton.Location = new Point(875, 38);

            Controls.Add(recievedButton);

            bool refundEnabled = item.Status != OrderStatus.Recieved && date > DateTime.Today
                || item.Status == OrderStatus.Recieved && date.AddDays(7).Date >= DateTime.Today;
            Color refundButtonColor = ColorTranslator.FromHtml(refundEnabled ? "#72B4DB" : "#D2D2D2");
            refundButton = new RoundedButton("SOLICITAR REEMBOLSO", 265, 57, refundButtonColor, 32);
            if (!refundEnabled)
            {
                refundButton.ForeColor = ColorTranslator.FromHtml("#999999");
                refundButton.Enabled = false;
            }
            refundButton.Location = new Point(875, 122);

            Controls.Add(refundButton);
        }

        private void AddLabels()
        {
            Label label = new Label();
            label.Text = "Endereço de entrega:";
            label.ForeColor = Color.Black;
            label.Font = new Font("Ubuntu", 12);
            label.Location = new Point(44, 30);
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            Controls.Add(label);

            Label name = new Label();
            name.Text = item.Name;
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 12);
            name.Location = new Point(44, 23);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                var radius = 12;
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(Width - radius - 1, 0, radius, radius), -90, 90);
                path.AddArc(new Rectangle(Width - radius - 1, Height - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, Height - radius - 1, radius, radius), 90, 90);
                path.CloseAllFigures();

                e.Graphics.FillPath(new SolidBrush(Color.White), path);

                e.Graphics.DrawPath(new Pen(Color.White, 1), path);
            }
        }
    }
}
