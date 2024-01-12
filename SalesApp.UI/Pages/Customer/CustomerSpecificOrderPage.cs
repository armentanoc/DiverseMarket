using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesApp.UI.Pages.Customer
{
    public partial class CustomerSpecificOrderPage : Form
    {
        public CustomerSpecificOrderPage(long orderId)
        {
            InitializeComponent(orderId);
        }
    }
}
