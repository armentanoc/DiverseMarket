
using SalesApp.UI.Components;
using SalesApp.UI.Styles;

namespace SalesApp.UI.Messages
{
    partial class LoginIncorrectMessage
    {

        private System.ComponentModel.IContainer components = null;

        private Label wrongUsernameOrPassword;
        private Label tryAgain;
        private RoundedButton okButton;


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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 324);
            this.Name = "Login incorreto";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "DiverseMarket";
            this.BackColor = Colors.MainBackgroundColor;
            this.Text = "Login incorreto";
            InitScreen();
        }

        private void InitScreen()
        {
            this.Icon = new Icon(@"Resources\icon.ico");

            this.wrongUsernameOrPassword = new Label();
            this.wrongUsernameOrPassword.Location = new System.Drawing.Point(150, 72);
            this.wrongUsernameOrPassword.Font = new Font("Ubuntu", 16);
            this.wrongUsernameOrPassword.Size = new System.Drawing.Size(580, 40);
            this.wrongUsernameOrPassword.Text = "Username e/ou senha incorretos";
            this.wrongUsernameOrPassword.ForeColor = Color.White;
            this.Controls.Add(wrongUsernameOrPassword);

            this.tryAgain = new Label();
            this.tryAgain.Location = new System.Drawing.Point(230, 152);
            this.tryAgain.Font = new Font("Ubuntu", 16);
            this.tryAgain.Size = new System.Drawing.Size(310, 40);
            this.tryAgain.Text = "Tente novamente";
            this.tryAgain.ForeColor = Color.White;
            this.Controls.Add(tryAgain);

            this.okButton = new RoundedButton("OK", 200, 33, Color.White, 32);
            this.okButton.Location = new Point(265, 220);
            this.okButton.Click += new EventHandler((object sender, EventArgs e) => { this.Close(); });
            this.Controls.Add(okButton);

        }
    }
}