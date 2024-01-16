using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Components.Moderator
{
    internal class CompanyCard : Panel
    {
        public Label name;
        public Label cnpjLabel;
        public Label tradeName;

        public CompanyCard(string cnpj, string companyName, string tradeName)
        {
            Width = 203;
            Height = 118;
            BorderStyle = BorderStyle.None;
            Cursor = Cursors.Hand;
            AddCompanyName(companyName);
            addTradeName(tradeName);
            AddCNPJ(cnpj);
        }

        private void AddCompanyName(string companyName)
        {
            name = new Label();
            name.Text = companyName;
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 10);
            name.Location = new Point(12, 12);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private void addTradeName(string tradeName)
        {
            cnpjLabel = new Label();
            cnpjLabel.Text = tradeName;
            cnpjLabel.ForeColor = Colors.LightBlue;
            cnpjLabel.Font = new Font("Ubuntu", 8);
            cnpjLabel.Location = new Point(12, 35);
            cnpjLabel.AutoSize = true;
            cnpjLabel.BackColor = Color.Transparent;
            Controls.Add(cnpjLabel);
        }

        private void AddCNPJ(string cnpj)
        {
            cnpjLabel = new Label();
            cnpjLabel.Text = cnpj;
            cnpjLabel.ForeColor = Colors.LightBlue;
            cnpjLabel.Font = new Font("Ubuntu", 8);
            cnpjLabel.Location = new Point(12, 55);
            cnpjLabel.AutoSize = true;
            cnpjLabel.BackColor = Color.Transparent;
            Controls.Add(cnpjLabel);
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
