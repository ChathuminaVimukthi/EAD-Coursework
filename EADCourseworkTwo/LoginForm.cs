using EADCourseworkTwo.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EADCourseworkTwo
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.pictureBox1.Image = Properties.Resources.calendar;
            this.nameLabel.Text = Properties.Resources.nameLabel;
            this.passwordLabel.Text = Properties.Resources.passwordLabel;
            this.loginBtn.Text = Properties.Resources.loginButton;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            UserModel userModel = new UserModel();
            string userName = this.nameTxtBox.Text;
            string password = this.passwordTxtBox.Text;
            if (validations())
            {
                Boolean validate = userModel.validateUser(userName, password);
                if (!validate)
                {
                    
                    MessageBox.Show("Entered Username and Password do not match !");
                }
                else
                {
                    HomeForm homeForm = new HomeForm(userModel.getUser(userName));
                    this.Hide();
                    homeForm.ShowDialog();
                    this.Close();
                }
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (validationsForRegiter())
            {
                User user = new User();
                user.UserName = userNameTxtBox.Text;
                user.Password = PasswordHandler.CreatePasswordHash(regPassTxtBox.Text);
                user.FirstName = firstNameTxtBox.Text;
                user.LastName = lastNameTxtBox.Text;
                user.Email = emailTxtBox.Text;
                UserModel userModel = new UserModel(); 
                Boolean isRegistered = userModel.addUser(user);

                if (isRegistered)
                {
                    ClearTextBoxes();
                    MessageBox.Show("You have been registered successfully. Please Login to continue!");
                }
                else
                {
                    MessageBox.Show("Database connection error occured! Please try again!");
                }
            }
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private Boolean validations()
        {
            Boolean isOkay = true;
            
            if (string.IsNullOrWhiteSpace(nameTxtBox.Text))
            {
                isOkay = false;
                errorProviderEvent.SetError(nameTxtBox, "Username should not be left blank!");
            }
            else
            {
                errorProviderEvent.SetError(nameTxtBox, "");
            }
            if (string.IsNullOrWhiteSpace(passwordTxtBox.Text))
            {
                isOkay = false;
                errorProvider1.SetError(passwordTxtBox, "Password should not be left blank!");
            }
            else
            {
                errorProvider1.SetError(passwordTxtBox, "");
            }
            return isOkay;
        }

        private Boolean validationsForRegiter()
        {
            Boolean isOkay = true;
            if (string.IsNullOrWhiteSpace(userNameTxtBox.Text))
            {
                isOkay = false;
                errorProvider2.SetError(userNameTxtBox, "Username should not be left blank!");
            }
            else
            {
                UserModel userModel = new UserModel();
                User user = userModel.getUser(userNameTxtBox.Text);
                if (user.UserName == userNameTxtBox.Text)
                {
                    isOkay = false;
                    errorProvider2.SetError(userNameTxtBox, "Username already exists! Please choose a different Username !");
                }
                else
                {
                    errorProvider2.SetError(userNameTxtBox, "");
                }
            }
            if (string.IsNullOrWhiteSpace(regPassTxtBox.Text))
            {
                isOkay = false;
                errorProvider3.SetError(regPassTxtBox, "Password should not be left blank!");
            }
            else
            {
                errorProvider3.SetError(regPassTxtBox, "");
            }
            if (string.IsNullOrWhiteSpace(confirmPassTxtBox.Text))
            {
                isOkay = false;
                errorProvider4.SetError(confirmPassTxtBox, "Confirm Password should not be left blank!");
            }
            else
            {
                if (regPassTxtBox.Text.Equals(confirmPassTxtBox.Text))
                {
                    errorProvider4.SetError(confirmPassTxtBox, "");
                }
                else
                {
                    isOkay = false;
                    errorProvider4.SetError(confirmPassTxtBox, "Confirm Password and Password do not match!");
                }
            }
            if (string.IsNullOrWhiteSpace(firstNameTxtBox.Text))
            {
                isOkay = false;
                errorProvider5.SetError(firstNameTxtBox, "First Name should not be left blank!");
            }
            else
            {
                errorProvider5.SetError(firstNameTxtBox, "");
            }
            if (string.IsNullOrWhiteSpace(lastNameTxtBox.Text))
            {
                isOkay = false;
                errorProvider6.SetError(lastNameTxtBox, "Last Name should not be left blank!");
            }
            else
            {
                errorProvider6.SetError(lastNameTxtBox, "");
            }
            if (string.IsNullOrWhiteSpace(emailTxtBox.Text))
            {
                isOkay = false;
                errorProvider7.SetError(emailTxtBox, "Email should not be left blank!");
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(emailTxtBox.Text);
                if (match.Success)
                {
                    errorProvider7.SetError(emailTxtBox, "");
                    
                }
                else
                {
                    errorProvider7.SetError(emailTxtBox, "Email format is incorrect!");
                    isOkay = false;
                }
            }
            return isOkay;
        }

    }
}
