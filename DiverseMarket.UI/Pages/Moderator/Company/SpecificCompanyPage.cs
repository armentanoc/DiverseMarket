using DiverseMarket.Backend.DTOs.Moderator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiverseMarket.UI.Pages.Moderator.Company
{
    public partial class SpecificCompanyPage : Form
    {
        public SpecificCompanyPage(long id, string cnpj, string corporateName)
        {
            InitializeComponent(id, cnpj, corporateName);
        }
    }
}
