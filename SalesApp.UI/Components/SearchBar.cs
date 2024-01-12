using SalesApp.UI.Pages.Customer;
using SalesApp.UI.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.UI.Components
{
    internal class SearchBar : Panel
    {
        private TextBox text {  get; set; }
        public Button SearchButton { get; private set; }

        public string Text()
        {
            return text.Text;
        }

        public SearchBar()
        {
            InitDesign();
        }

        private void InitDesign()
        {
            this.text = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(59, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Width = 788,
                Height = 34,
                Text = "Pesquisar",
                BackColor = Colors.TextBoxFill,
                ForeColor = Colors.CallToActionText,
                Font = new Font("Ubuntu", 12)
            };

            this.text.Enter += new EventHandler((sender, e) =>
            {
                TextBox textBox = sender as TextBox;
                if (textBox.Text == "Pesquisar")
                {
                    textBox.Text = string.Empty;
                }
            });
            this.text.Leave += new EventHandler((sender, e) =>
            {
                TextBox textBox = sender as TextBox;
                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text.Equals("Pesquisar"))
                {
                    textBox.Text = "Pesquisar";
                }
            });

            Size = new Size(863, 42);
            Padding = new Padding(10);
            BackColor = Color.Transparent;
            Controls.Add(this.text);

            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchButton.Location = new Point(18, 9);
            this.SearchButton.Size = new Size(24, 24);
            this.SearchButton.FlatStyle = FlatStyle.Flat;
            this.SearchButton.FlatAppearance.BorderSize = 0;
            this.SearchButton.Cursor = Cursors.Hand;
            this.SearchButton.Image = Image.FromFile(@"Resources\search.png");
            this.SearchButton.BackgroundImageLayout = ImageLayout.Zoom;
            

            Controls.Add(this.SearchButton);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                var radius = 32;
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
