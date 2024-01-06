using SalesApp.UI.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.UI.Components
{
    internal class LogoutButton
    {
        private RoundedButton logoutButton;
        internal LogoutButton()
        {
            this.logoutButton = new RoundedButton("Sair", 300, 57, Colors.SecondaryButton, 32);
            this.logoutButton.Location = new System.Drawing.Point(955, 25);
            this.logoutButton.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.logoutButton.Cursor = Cursors.Hand; });
            //this.logoutButton.Click += new EventHandler(logoutButton_Click);
        }
    }
}
