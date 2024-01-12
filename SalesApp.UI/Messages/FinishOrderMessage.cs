using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesApp.UI.Messages
{
    public partial class FinishOrderMessage : Form
    {
        public FinishOrderMessage(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
