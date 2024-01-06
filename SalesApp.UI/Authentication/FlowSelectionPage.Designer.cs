using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Authentication
{
    partial class FlowSelectionPage
    {
        private System.ComponentModel.IContainer components = null;

        private Label youAreLabel;

        private GroupBox userRoleGroupBox;
        private RadioButton customerRadioButton;
        private RadioButton companyRadioButton;

        private Button loginButton;
        private RoundedButton followButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 832);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            FormClosed += FlowSelectionPage_FormClosed;
            InitScreen();
        }

        private void InitScreen()
        {
            InitLogo();
            InitRadioButtons();
            InitButtons();
        }

        private void InitRadioButtons()
        {
            userRoleGroupBox = new GroupBox();
            userRoleGroupBox.Text = "You are:";
            userRoleGroupBox.Location = new Point(388, 350);
            userRoleGroupBox.Size = new Size(572, 180);
            userRoleGroupBox.Font = new Font("Ubuntu", 24);
            userRoleGroupBox.ForeColor = Color.White;

            customerRadioButton = new RadioButton();
            customerRadioButton.Text = "Cliente";
            customerRadioButton.Location = new Point(97, 80);
            customerRadioButton.Size = new Size(154, 24);
            customerRadioButton.Font = new Font("Ubuntu", 12);
            customerRadioButton.ForeColor = Color.White;

            companyRadioButton = new RadioButton();
            companyRadioButton.Text = "Empresa";
            companyRadioButton.Location = new Point(347, 84);
            companyRadioButton.Size = new Size(154, 24);
            companyRadioButton.Font = new Font("Ubuntu", 12);
            companyRadioButton.ForeColor = Color.White;

            userRoleGroupBox.Controls.Add(customerRadioButton);
            userRoleGroupBox.Controls.Add(companyRadioButton);

            this.Controls.Add(userRoleGroupBox);
        }


        private void InitButtons()
        {
            this.loginButton = new System.Windows.Forms.Button();
            this.loginButton.Image = Image.FromFile(@"Resources\back-login.png");
            this.loginButton.Location = new Point(69, 64);
            this.loginButton.Size = new Size(104, 24);
            this.loginButton.BackColor = Color.Transparent;
            this.loginButton.FlatAppearance.BorderSize = 0;
            this.loginButton.FlatStyle = FlatStyle.Flat;
            this.loginButton.Cursor = Cursors.Hand;
            this.loginButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                new LoginPage().Show();
                this.Hide();
            });

            this.Controls.Add(this.loginButton);

            this.followButton = new RoundedButton("SEGUIR", 224, 57, Colors.CallToActionButton, 32);
            this.followButton.Location = new Point(541, 554);
            this.followButton.Cursor = Cursors.Hand;
            this.followButton.Click += followButton_Click;

            this.Controls.Add(this.followButton);

        }

        private void followButton_Click(object sender, EventArgs e)
        {
            if (customerRadioButton.Checked)
            {
                new ClientRegisterPage().Show();
                this.Hide();
            }
            else if(companyRadioButton.Checked)
            {
                new CompanyRegisterPage().Show();
                this.Hide();
            }
                
        }

        private void InitLogo()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            Logo logo = new Logo();
            logo.Location = new Point(378, 142);
            logo.Width = 523;
            logo.Height = 61;

            this.Controls.Add(logo);
        }

        private void FlowSelectionPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}