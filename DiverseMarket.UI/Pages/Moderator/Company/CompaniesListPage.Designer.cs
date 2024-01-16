using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Services;
using DiverseMarket.UI.Components;
using DiverseMarket.UI.Components.Moderator;
using DiverseMarket.UI.Pages.Customer;
using DiverseMarket.UI.Styles;
using DiverseMarket.Backend.DTOs.Moderator;
using DiverseMarket.Backend.Services.Moderator;

namespace DiverseMarket.UI.Pages.Moderator.Company
{
    partial class CompaniesListPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private long _userId;
        private SearchBar searchBar;
        private List<CompanyCard> companyCards;
        private Panel companiesPanel;

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
            InitLogo();
            InitSearchBar();
            InitCompanies();
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(544, 46);
            logo.Width = 192;
            logo.Height = 22;

            this.Controls.Add(logo);
        }

        private void InitSearchBar()
        {
            searchBar = new SearchBar();
            searchBar.Location = new Point(184, 126);

            searchBar.SearchButton.Click += new EventHandler(searchButton_Click);

            this.Controls.Add(searchBar);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (this.searchBar.Text() != "Pesquisar")
            {
                List<CompanyCard> companiesAfterSearch = new List<CompanyCard>(this.companyCards);

                foreach (var product in this.companyCards)
                {

                    if (!product.name.Text.Contains(this.searchBar.Text(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        companiesAfterSearch.Remove(product);
                    }
                }

                ReloadCompanies(companiesAfterSearch);
            }
            else
                ReloadCompanies(this.companyCards);
        }

        private void ReloadCompanies(List<CompanyCard> companies)
        {
            ClearCompanies();

            int x = 8;
            int y = 17;

            foreach (var product in companies)
            {
                product.Location = new Point(x, y);

                product.Visible = true;
                this.companiesPanel.Controls.Add(product);
                product.BringToFront();

                if (x == 713)
                {
                    x = 8;
                    y += 150;
                }
                else
                    x += 235;

            }
            companiesPanel.Invalidate();
            companiesPanel.Update();
        }

        private void ClearCompanies()
        {
            foreach (Control control in this.companiesPanel.Controls.OfType<Control>().ToList())
            {
                companiesPanel.Controls.Remove(control);
            }

        }

        private void InitCompanies()
        {
            this.companiesPanel = new Panel();
            this.companiesPanel.Size = new Size(927, 603);
            this.companiesPanel.Location = new Point(178, 188);
            this.companiesPanel.BackColor = Colors.MainBackgroundColor;
            this.companiesPanel.AutoScroll = true;
            this.Controls.Add(companiesPanel);

            List<CompanyBasicInfoDTO> companyBasicInfoDTOs = CompanyService.GetAllCompaniesBasicInfo();

            int x = 8;
            int y = 17;

            this.companyCards = new List<CompanyCard>();

            foreach (var companyBasicInfoDTO in companyBasicInfoDTOs)
            {
                CompanyCard CompanyCard = new CompanyCard(companyBasicInfoDTO.CNPJ, companyBasicInfoDTO.CorporateName);
                CompanyCard.Location = new Point(x, y);
                CompanyCard.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    new SpecificCompanyPage(companyBasicInfoDTO.Id, companyBasicInfoDTO.CNPJ, companyBasicInfoDTO.CorporateName).Show();
                    this.Hide();
                });

                this.companyCards.Add(CompanyCard);

                this.companiesPanel.Controls.Add(CompanyCard);

                if (x == 713)
                {
                    x = 8;
                    y += 150;
                }
                else
                    x += 235;

            }
        }
        #endregion
    }
}