﻿using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Components
{
    internal class CompanyProductCard : Panel
    {
        public Label name;
        public Label categoryLabel;
        public CompanyProductCard(string productName, string description, string category)
        {
            Width = 203;
            Height = 88;
            BorderStyle = BorderStyle.None;
            Cursor = Cursors.Hand;
            AddProductName(productName);
            AddDescription(description);
            AddCategory(category);
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
