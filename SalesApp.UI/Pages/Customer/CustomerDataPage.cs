﻿using System;
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
    public partial class CustomerDataPage : Form
    {
        public CustomerDataPage(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
