using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EADCourseworkTwo
{
    public partial class AddContact : UserControl
    {
        public AddContact()
        {
            InitializeComponent();
        }

        public string ContactName
        {
            set
            {
                this.contactNameTxtBox.Text = value;
            }
            get
            {
                return this.contactNameTxtBox.Text;
            }
        }

         public int ContactNumber
        {
            set
            {
                this.numberTxtBox.Text = value.ToString();
            }
            get
            {
                if (validatecontactNumber(this.numberTxtBox.Text.ToString()))
                {
                    if(this.numberTxtBox.Text.ToString().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return int.Parse(this.numberTxtBox.Text.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Mobile number format is wrong !");
                    return 0;
                }
            }
        }

        public string Email
        {
            set
            {
                this.emailTxtBox.Text = value;
            }
            get
            {
                if (validateEmail(this.emailTxtBox.Text))
                {
                    return this.emailTxtBox.Text;
                }
                else
                {
                    MessageBox.Show("Email is incorrect !");
                    return "";
                }
            }
        }

        private void numTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public Boolean validatecontactNumber(string number)
        {
            Console.WriteLine(number);
            Regex re = new Regex(@"^7|0|(?:\+94)[0-9]{9,10}$");
            Match match = re.Match(number);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean validateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddContact_Load(object sender, EventArgs e)
        {

        }
    }
}
