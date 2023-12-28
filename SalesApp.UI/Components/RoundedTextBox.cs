using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Components
{
    internal class RoundedTextBox : UserControl
    {
        public TextBox TextBox { get; private set; }

        public RoundedTextBox(string hintText, int width, int height)
        {
            TextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(16, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Width = width - 32,
                Height = height,
                Text = hintText,
                BackColor = Colors.TextBoxFill,
                ForeColor = Colors.CallToActionText,
                Font = new Font("Ubuntu", 16)
            };

            TextBox.Enter += new EventHandler((sender, e) =>
            {
                TextBox textBox = sender as TextBox;
                if (textBox.Text == hintText)
                {
                    textBox.Text = string.Empty;
                }
            });
            TextBox.Leave += new EventHandler((sender, e) =>
            {
                TextBox textBox = sender as TextBox;
                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text.Equals(hintText))
                {
                    textBox.Text = hintText;
                }
            });

            Size = new Size(width, height);
            Padding = new Padding(10);
            BackColor = Color.Transparent;
            Controls.Add(TextBox);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                var radius = 16;
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(Width - radius - 1, 0, radius, radius), -90, 90);
                path.AddArc(new Rectangle(Width - radius - 1, Height - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, Height - radius - 1, radius, radius), 90, 90);
                path.CloseAllFigures();

                e.Graphics.FillPath(new SolidBrush(Colors.TextBoxFill), path);

                e.Graphics.DrawPath(new Pen(Colors.StrokeTextBox, 1), path);
            }
        }
    }
}
