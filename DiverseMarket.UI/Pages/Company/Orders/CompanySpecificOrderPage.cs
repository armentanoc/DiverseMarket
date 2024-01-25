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
    public partial class CompanySpecificOrderPage : Form
    {
        public CompanySpecificOrderPage(OrderBasicInfoDTO order, long userId)
        {
            InitializeComponent(order, userId);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
