using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Components
{
    internal class ProductOfferCard : Panel
    {
        public Label name;
        public Label categoryLabel;
        public ProductOfferCard(string productName, string description, string category, decimal price, long quantity)
        {
            Width = 203;
            Height = 118;
            BorderStyle = BorderStyle.None;
            Cursor = Cursors.Hand;
            AddProductName(productName);
            AddDescription(description);
            AddCategory(category);
            AddPrice(price);
        }

        private void AddProductName(string productName)
        {
            name = new Label();
            name.Text = productName;
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 10);
            name.Location = new Point(12, 12);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private void AddDescription(string description)
        {
            Label descriptionLabel = new Label();
            descriptionLabel.Text = description;
            descriptionLabel.ForeColor = Colors.LightBlue;
            descriptionLabel.Font = new Font("Ubuntu", 8);
            descriptionLabel.Location = new Point(12, 55);
            descriptionLabel.AutoSize = true;
            descriptionLabel.BackColor = Color.Transparent;
            Controls.Add(descriptionLabel);
        }

        private void AddCategory(string category)
        {
            categoryLabel = new Label();
            categoryLabel.Text = category;
            categoryLabel.ForeColor = Colors.LightBlue;
            categoryLabel.Font = new Font("Ubuntu", 8);
            categoryLabel.Location = new Point(12, 33);
            categoryLabel.AutoSize = true;
            categoryLabel.BackColor = Color.Transparent;
            Controls.Add(categoryLabel);
        }

        private void AddPrice(decimal currentPrice)
        {
            Label price = new Label();
            price.Text = $"Valor atual: R${string.Format("{0:N2}", currentPrice).Replace('.', ',')}";
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
