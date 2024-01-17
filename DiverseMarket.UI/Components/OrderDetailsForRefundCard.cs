using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.UI.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.UI.Components
{
    internal class OrderDetailsForRefundCard : Panel
    {
        public OrderDetailsForRefundCard(long orderId, string companyName, string customerName, AddressDTO deliveryAddress, OrderStatus status)
        {
            Width = 1169;
            Height = 151;
            BorderStyle = BorderStyle.None;
            AddTitle(orderId, status,companyName);
            AddAddress(customerName, deliveryAddress);

        }

        private void AddTitle(long orderId, OrderStatus status, string companyName)
        {
            string statusString = GetStatusName(status);

            Label name = new Label();
            name.Text = $"Pedido {orderId} | {statusString} | {companyName}";
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 24);
            name.Location = new Point(32, 30);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private string GetStatusName(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Sent: return "Enviado";
                case OrderStatus.Canceled: return "Cancelado";
                case OrderStatus.Preparation: return "Em preparação";
                case OrderStatus.NotRecieved: return "Não recebido";

                default: return "Recebido";
            }
        }

        private void AddAddress(string customerName, AddressDTO deliveryAddress)
        {
            Label name = new Label();
            name.Text = $"{customerName} | {deliveryAddress.Street}, n°{deliveryAddress.Number}" +
                deliveryAddress.Complement == null ? "" : $"{deliveryAddress.Complement} | " +
                $"\n{deliveryAddress.City} | {deliveryAddress.ZipCode}";
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 12);
            name.Location = new Point(32, 75);
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
