using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EADCourseworkTwo
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.loginPicBox.Image = Properties.Resources.logo;
            this.nameLabel.Text = Properties.Resources.nameLabel;
            this.passwordLabel.Text = Properties.Resources.passwordLabel;
            this.loginBtn.Text = Properties.Resources.loginButton;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            UserModel userModel = new UserModel();
            string userName = this.nameTxtBox.Text;
            string password = this.passwordTxtBox.Text;
            Boolean validate = userModel.validateUser(userName,password);
            if (!validate)
            {
                MessageBox.Show("Entered Username and Password do not match !");
            }
            else
            {
                HomeForm homeForm = new HomeForm(userModel.getUser(userName));

                homeForm.Show();
            }
            
        }
    }
}
