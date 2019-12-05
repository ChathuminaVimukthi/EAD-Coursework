using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EADCourseworkTwo
{
    public partial class ContactControl : UserControl
    {
        private int id;
        public ContactControl()
        {
            InitializeComponent();
            this.pictureBox1.Image = Properties.Resources.contacts_book;
        }


        public int Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }

        public string ContactName
        {
            set
            {
                this.contactNameLabel.Text = value;
            }
            get
            {
                return this.contactNameLabel.Text;
            }
        }

        public string Email
        {
            set
            {
                this.emailLabel.Text = value;
            }
        }

        public string ContactNumber
        {
            set
            {
                this.contactNumberLabel.Text = value;
            }
        }
    }
}
