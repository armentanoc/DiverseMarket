﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiverseMarket.UI.Pages.Moderator
{
    public partial class HomePageModerator : Form
    {
        public HomePageModerator(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
