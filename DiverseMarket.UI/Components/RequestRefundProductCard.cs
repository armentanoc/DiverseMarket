using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.UI.Components
{
    internal class RequestRefundProductCard : Panel
    {
        public RequestRefundProductCard(string productName, int quantity, double unityPrice)
        {

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
