using DiverseMarket.UI.Styles;
using DiverseMarket.Backend.Model.Enums;

namespace DiverseMarket.UI.Components
{
    public class OrderCard : Panel
    {
        public OrderCard(long id, DateTime date, OrderStatus status, double price)
        {
            Width = 203;
            Height = 118;
            BorderStyle = BorderStyle.None;
            Cursor = Cursors.Hand;
            AddId(id);
            AddDate(date);
            AddStatus(status);
        }

        private void AddStatus(OrderStatus status)
        {
            Label name = new Label();
            name.Text = GetStatusToString(status);
            name.ForeColor = GetStatusColor(status);
            name.Font = new Font("Ubuntu", 10);
            name.Location = new Point(16, 90);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }


        private Color GetStatusColor(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Sent: return Colors.SentOrder;
                case OrderStatus.Canceled: return Colors.CanceledOrder;
                case OrderStatus.Preparation: return Colors.PreparationOrder;
                default: return Colors.RecievedOrder;
            }
        }

        private string GetStatusToString(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Sent: return "Enviado";
                case OrderStatus.Canceled: return "Cancelado";
                case OrderStatus.Preparation: return "Preparação";
                default: return "Recebido";
            }
        }
        private void AddDate(DateTime date)
        {
            Label name = new Label();
            name.Text = string.Format(date.ToString(), "DD/MM/YY");
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 8);
            name.Location = new Point(16, 42);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);

        }

        private void AddId(long id)
        {
            Label name = new Label();
            name.Text = $"Pedido #{id}";
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 10);
            name.Location = new Point(16, 16);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }
        private void AddPrice(double totalPrice)
        {
            Label price = new Label();
            price.Text = $"| Total: R${string.Format("{0:N2}", totalPrice).Replace('.', ',')}";
            price.ForeColor = Colors.MainBackgroundColor;
            price.Font = new Font("Ubuntu", 10);
            price.Location = new Point(12, 91);
            price.AutoSize = true;
            price.BackColor = Color.Transparent;
            Controls.Add(price);
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
