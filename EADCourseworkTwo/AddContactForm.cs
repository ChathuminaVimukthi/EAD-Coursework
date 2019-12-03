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
    public partial class AddContactForm : Form
    {
        public AddContactForm()
        {
            InitializeComponent();
            showAddForm();
        }

        public void SaveButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            { 
                Button temp = (Button)sender;
                TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)temp.Parent;
                AddContact addContact = (AddContact)tableLayoutPanel.Parent;
                string name = addContact.ContactName;
                string email = addContact.Email;
                int contactNumber = addContact.ContactNumber;

                MessageBox.Show(name+email+contactNumber);
            }
        }

        private void addMoreButton_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.saveContact.Click += SaveButton_Click;
            tableLayoutPanel3.Controls.Add(addContact, 0, 1);

            addMoreButton.Dispose();
        }

        private void showAddForm()
        {
            AddContact addContact = new AddContact();
            addContact.saveContact.Click += SaveButton_Click;
            tableLayoutPanel3.Controls.Add(addContact, 0, 0);
        }
    }
}
