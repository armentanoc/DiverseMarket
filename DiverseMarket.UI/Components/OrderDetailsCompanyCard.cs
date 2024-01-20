using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Model;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Components
{
    public class OrderDetailCompanyCard : Panel
    {
        public OrderDetailCompanyCard(OrderSpecificDetailsDTO order)
        {
            InitializeComponents(order);
        }

        private void InitializeComponents(OrderSpecificDetailsDTO order)
        {
            this.Size = new Size(1000, 500);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None;
            this.Region = new Region(GetRoundedRectangle(this.ClientRectangle, 20));

            AddLabel("Order ID:", order.OrderInfo.Id, 40, 40);
            AddLabel("Data:", order.OrderInfo.Date.ToShortDateString(), 40, 80);
            AddLabel("Quantidade:", $"{order.OrderInfo.TotalAmount:C2}", 40, 120);
            AddLabel("Customer ID:", order.OrderInfo.CustomerId, 40, 160);
            AddLabel("Company ID:", order.OrderInfo.CompanyId, 40, 200);
            AddLabel("Nome de usuário:", order.User.Name, 40, 240);
            AddLabel("E-mail:", order.User.Email, 40, 280);
            AddAddressDetails(order.Address, 40, 320);
        }

        private void AddLabel(string labelText, object value, int x, int y)
        {
            Label label = new Label();
            label.Text = $"{labelText} {value}";
            label.Location = new Point(x, y);
            label.ForeColor = Colors.MainBackgroundColor;
            label.Font = new Font("Ubuntu", 10);
            label.AutoSize = true;
            this.Controls.Add(label);
        }

        private void AddAddressDetails(Address address, int x, int y)
        {
            Label addressLabel = new Label();
            addressLabel.Text = $"Endereço: {address.Street}, {address.Number}, {address.Complement}, {address.ZipCode}, {address.Neighborhood}, {address.City}";
            addressLabel.Location = new Point(x, y);
            addressLabel.ForeColor = Colors.MainBackgroundColor;
            addressLabel.Font = new Font("Ubuntu", 10);
            addressLabel.AutoSize = true;
            this.Controls.Add(addressLabel);
        }

        private static GraphicsPath GetRoundedRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rectangle.X, rectangle.Y, radius * 2, radius * 2, 180, 90);
            roundedRect.AddArc(rectangle.Right - radius * 2, rectangle.Y, radius * 2, radius * 2, 270, 90);
            roundedRect.AddArc(rectangle.Right - radius * 2, rectangle.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            roundedRect.AddArc(rectangle.X, rectangle.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            roundedRect.CloseFigure();
            return roundedRect;
        }
    }
}


