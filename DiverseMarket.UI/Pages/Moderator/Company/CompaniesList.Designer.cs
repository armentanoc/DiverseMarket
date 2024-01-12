using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Styles;

namespace DiverseMarket.UI.Pages.Moderator.Company
{
    partial class CompaniesList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private long _userId;
        private Button cartButton, profileButton;
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
            Name = "CompaniesListPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CompaniesList";
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
            string userName = UserService.GetUserFullNameById(userId);

            this.helloLabel = new Label();
            this.helloLabel.Text = $"Olá, {userName}";
            this.helloLabel.ForeColor = Colors.SecondaryButton;
            this.helloLabel.Font = new System.Drawing.Font("Ubuntu", 24);
            this.helloLabel.Location = new Point(50, 100);
            this.helloLabel.Size = new Size(500, 100);
            this.Controls.Add(helloLabel);

            this.textLabel = new Label();
            this.textLabel.Text = "Selecione uma opção";
            this.textLabel.ForeColor = Colors.SecondaryButton;
            this.textLabel.Font = new System.Drawing.Font("Ubuntu", 16);
            this.textLabel.Location = new Point(50, 250);
            this.textLabel.Size = new Size(500, 100);
            this.Controls.Add(textLabel);
        }

        private void InitButtons()
        {
        }

        #endregion
    }
}