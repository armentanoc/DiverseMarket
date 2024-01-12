using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiverseMarket.UI.Pages.Moderator.Category
{
    public partial class CategoriesList : Form
    {
        public CategoriesList(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
