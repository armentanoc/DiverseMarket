using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.UI.Resources
{
    internal class Logo : PictureBox
    {
        internal Logo()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Image.FromFile(@"Resources\DiverseMarket.png");
        }
    }
}
