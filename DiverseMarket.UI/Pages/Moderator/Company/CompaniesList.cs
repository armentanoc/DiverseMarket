﻿using System;
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
    public partial class CompaniesList : Form
    {
        public CompaniesList(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
