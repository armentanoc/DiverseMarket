﻿using System;
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
    public partial class AddSpecificOfferPage : Form
    {
        public AddSpecificOfferPage(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
