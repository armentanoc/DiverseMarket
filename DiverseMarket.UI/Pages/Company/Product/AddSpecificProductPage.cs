using DiverseMarket.Backend.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiverseMarket.UI.Pages.Company
{
    public partial class AddSpecificProductPage : Form
    {
        public AddSpecificProductPage(ProductBasicInfoDTO productBasicInfoDTO, long userId)
        {
            InitializeComponent(productBasicInfoDTO, userId);
        }
    }
}
