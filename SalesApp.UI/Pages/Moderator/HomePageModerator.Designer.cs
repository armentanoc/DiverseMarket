using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Pages.Moderator
{
    partial class HomePageModerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private long _userId;
        private RoundedButton companiesList;
        private RoundedButton categoriesList;
        private RoundedButton refundList;
        private RoundedButton logoutButton;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(long userId)
        {
            this._userId = userId;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HomePageModerator";
            this.BackColor = Colors.MainBackgroundColor;
            InitScreen();
        }

        private void InitComponents()
        {
            InitButtons();
        }

        private void InitScreen()
        {
            InitComponents();
        }

        private void InitButtons()
        {
            this.logoutButton = new RoundedButton("Sair", 300, 57, Colors.SecondaryButton, 32);
            this.logoutButton.Location = new System.Drawing.Point(955, 25);
            this.logoutButton.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.logoutButton.Cursor = Cursors.Hand; });
            //this.logoutButton.Click += new EventHandler(logoutButton_Click);
            this.Controls.Add(logoutButton);

            this.companiesList = new RoundedButton("Listar Estabelecimentos", 300, 57, Colors.SecondaryButton, 32);
            this.companiesList.Location = new System.Drawing.Point(150, 300);
            this.companiesList.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.companiesList.Cursor = Cursors.Hand; });
            //this.companiesList.Click += new EventHandler(companiesList_Click);
            this.Controls.Add(companiesList);

            this.categoriesList = new RoundedButton("Listar categorias", 300, 57, Colors.SecondaryButton, 32);
            this.categoriesList.Location = new System.Drawing.Point(470, 300);
            this.categoriesList.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.categoriesList.Cursor = Cursors.Hand; });
            //this.categoriesList.Click += new EventHandler(categoriesList_Click);
            this.Controls.Add(categoriesList);

            this.refundList = new RoundedButton("Listar categorias", 300, 57, Colors.SecondaryButton, 32);
            this.refundList.Location = new System.Drawing.Point(790, 300);
            this.refundList.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.refundList.Cursor = Cursors.Hand; });
            //this.refundList.Click += new EventHandler(refundList_Click);
            this.Controls.Add(refundList);            
        }

        #endregion
    }
}