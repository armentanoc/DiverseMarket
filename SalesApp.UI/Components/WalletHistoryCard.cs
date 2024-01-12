
namespace SalesApp.UI.Components
{
    public class WalletHistoryCard : Panel
    {
        public WalletHistoryCard(string type, DateTime dateTime, double amount)
        {
            Width = 1169;
            Height = 102;
            BorderStyle = BorderStyle.None;
            AddAmount(type, amount);
            AddName(type, amount);
            AddDateTime(dateTime);
        }

        private void AddDateTime(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        private void AddName(string type, double amount)
        {
            throw new NotImplementedException();
        }

        private void AddAmount(string type, double amount)
        {
            throw new NotImplementedException();
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
