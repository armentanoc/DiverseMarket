using SalesApp.UI.Authentication;
using SalesApp.UI.Components;
using SalesApp.UI.Pages.Moderator.Category;
using SalesApp.UI.Pages.Moderator.Company;
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
        private Label helloLabel;
        private Label textLabel;
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
            InitScreen(userId);
        }


        private void InitScreen(long userId)
        {
            InitComponents(userId);
        }

        private void InitComponents(long userId)
        {
            InitButtons();
            InitLabels(userId);
        }

        private void InitLabels(long userId)
        {
            this.helloLabel = new Label();
            this.helloLabel.Text = $"Olá,{userId}";
            this.helloLabel.ForeColor = Colors.SecondaryButton;
            this.helloLabel.Font = new Font("Ubuntu", 24);
            this.helloLabel.Location = new Point(50, 100);
            this.helloLabel.Size = new Size(500, 100);
            this.Controls.Add(helloLabel);

            this.textLabel = new Label();
            this.textLabel.Text = "Selecione uma opção";
            this.textLabel.ForeColor = Colors.SecondaryButton;
            this.textLabel.Font = new Font("Ubuntu", 16);
            this.textLabel.Location = new Point(50, 250);
            this.textLabel.Size = new Size(500, 100);
            this.Controls.Add(textLabel);
        }

        private void InitButtons()
        {
            this.logoutButton = new RoundedButton("Sair", 150, 57, Colors.SecondaryButton, 32);
            this.logoutButton.Location = new System.Drawing.Point(1105, 25);
            this.logoutButton.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.logoutButton.Cursor = Cursors.Hand; });
            //this.logoutButton.Click += new EventHandler(logoutButton_Click);
            this.Controls.Add(logoutButton);

            this.companiesList = new RoundedButton("Listar Estabelecimentos", 300, 57, Colors.SecondaryButton, 32);
            this.companiesList.Location = new System.Drawing.Point(50, 300);
            this.companiesList.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.companiesList.Cursor = Cursors.Hand; });
            this.companiesList.Click += new EventHandler(companiesList_Click);
            this.Controls.Add(companiesList);

            this.categoriesList = new RoundedButton("Listar categorias", 300, 57, Colors.SecondaryButton, 32);
            this.categoriesList.Location = new System.Drawing.Point(370, 300);
            this.categoriesList.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.categoriesList.Cursor = Cursors.Hand; });
            this.categoriesList.Click += new EventHandler(categoriesList_Click);
            this.Controls.Add(categoriesList);

            this.refundList = new RoundedButton("Listar categorias", 300, 57, Colors.SecondaryButton, 32);
            this.refundList.Location = new System.Drawing.Point(690, 300);
            this.refundList.MouseEnter += new EventHandler((object sender, EventArgs e) => { this.refundList.Cursor = Cursors.Hand; });
            //this.refundList.Click += new EventHandler(refundList_Click);
            this.Controls.Add(refundList);            
        }

        private void companiesList_Click(object sender, EventArgs e)
        {
            new CompaniesList().Show();
            this.Hide();
        }

        private void categoriesList_Click(object sender, EventArgs e)
        {
            new CategoriesList().Show();
            this.Hide();
        }

        #endregion
    }
}