﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiverseMarket.UI.Pages.Customer
{
    public partial class CustomerOrdersPage : Form
    {
        public CustomerOrdersPage(long userId)
        {
            InitializeComponent(userId);
        }
    }
}