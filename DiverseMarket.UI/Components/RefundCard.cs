using DiverseMarket.UI.Styles;
using DiverseMarket.Backend.Model.Enums;

namespace DiverseMarket.UI.Components
{
    public class RefundCard : Panel
    {
        public RefundCard(long id, string productName, string companyName, RefundStatus status, double price)
        {
            Width = 203;
            Height = 118;
            BorderStyle = BorderStyle.None;
            Cursor = Cursors.Hand;
            AddId(id);
            AddProductName(productName);
            AddCompanyName(companyName);
            AddStatus(status);
            AddPrice(price);
        }

        private void AddPrice(double price)
        {
            Label name = new Label();
            name.Text = $"| Total: R${string.Format("{0:N2}", price).Replace('.', ',')}";
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 10);
            name.Location = new Point(16, 16);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private void AddStatus(RefundStatus status)
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

        private Color GetStatusColor(RefundStatus status)
        {
            switch (status)
            {
                case RefundStatus.Accepted: return Colors.ApprovedRefund;
                case RefundStatus.Denied: return Colors.DeniedRefund;
                default: return Colors.AnalysisRefund;
            }
        }

        private string GetStatusToString(RefundStatus status)
        {
            switch (status)
            {
                case RefundStatus.Accepted: return "Aprovada";
                case RefundStatus.Denied: return "Recusada";
                default: return "Em análise";
            }
        }

        private void AddCompanyName(string companyName)
        {
            Label name = new Label();
            name.Text = $"Vendido por {companyName}";
            name.ForeColor = Colors.LightBlue;
            name.Font = new Font("Ubuntu", 8);
            name.Location = new Point(16, 61);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private void AddId(long id)
        {
            Label name = new Label();
            name.Text = $"Solicitação #{id}";
            name.ForeColor = Colors.MainBackgroundColor;
            name.Font = new Font("Ubuntu", 10);
            name.Location = new Point(16, 16);
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            Controls.Add(name);
        }

        private void AddProductName(string productName)
        {
            Label name = new Label();
            name.Text = productName;
            name.ForeColor = Colors.LightBlue;
            name.Font = new Font("Ubuntu", 8);
            name.Location = new Point(16, 42);
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
