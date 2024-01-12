using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.UI.Components
{
    internal class Logo : PictureBox
    {
        internal Logo()
        {
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = Image.FromFile(@"Resources\DiverseMarket.png");
        }
    }
}
