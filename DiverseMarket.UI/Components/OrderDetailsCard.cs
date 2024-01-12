using DiverseMarket.Backend.DTOs;

namespace DiverseMarket.UI.Components
{
    public class OrderDetailsCard : Panel
    {
        public OrderDetailsCard(string customerName, AddressDTO deliveryAddress, double totalAmount)
        {
            Width = 1169;
            Height = 151;
            BorderStyle = BorderStyle.None;
            AddAddress(customerName, deliveryAddress);
            AddTotalAmount(totalAmount);

        }

        private void AddTotalAmount(double totalAmount)
        {
            Label name = new Label();
            name.Text = $"Total: R${string.Format("{0:N2}", totalAmount).Replace('.', ',')}";
            name.ForeColor = Color.Black;
            name.Font = new Font("Ubuntu", 24);
            name.Location = new Point(838, 74);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private void AddAddress(string customerName, AddressDTO deliveryAddress)
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
            name.Text = $"{customerName} | {deliveryAddress.Street}, n°{deliveryAddress.Number}" +
                deliveryAddress.Complement == null ? "" : $"{deliveryAddress.Complement} | " +
                $"\n{deliveryAddress.City} | {deliveryAddress.ZipCode}";
            name.ForeColor = Color.Black;
            name.Font = new Font("Ubuntu", 24);
            name.Location = new Point(44, 65);
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
