using EADCourseworkTwo.model;
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
        User loggedinUser;

        public AddContactForm(User user)
        {
            InitializeComponent();
            loggedinUser = user;
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

                if(name == "")
                {
                    MessageBox.Show("User Name cannot be empty !");
                }else if(email == "")
                {
                    MessageBox.Show("Enter valid Email Address !");
                }else if(contactNumber == 0)
                {
                    MessageBox.Show("Enter valid Mobile Number !");
                }
                else
                {
                    Contact contact = new Contact();
                    contact.ContactName = name;
                    contact.Email = email;
                    contact.ContactNumber = contactNumber;
                    contact.UserId = loggedinUser.UserId;

                    ContactModel contactModel = new ContactModel();
                    Boolean isInserted = contactModel.addContact(contact);
                    if (isInserted)
                    {
                        MessageBox.Show("Contact saved !");
                    }
                    else
                    {
                        MessageBox.Show("Failed !");
                    }
                }
            }
        }

        private void addMoreButton_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.saveContact.Click += SaveButton_Click;
            addContact.BackColor = ColorTranslator.FromHtml("#706fd3");
            tableLayoutPanel3.Controls.Add(addContact, 0, 1);
            addMoreButton.Dispose();
        }

        private void showAddForm()
        {
            AddContact addContact = new AddContact();
            addContact.saveContact.Click += SaveButton_Click;
            addContact.BackColor = ColorTranslator.FromHtml("#706fd3");
            tableLayoutPanel3.Controls.Add(addContact, 0, 0);
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedinUser);
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private void addEvent_Click(object sender, EventArgs e)
        {
            AddEventsForm addEventForm = new AddEventsForm(loggedinUser);
            this.Hide();
            addEventForm.ShowDialog();
            this.Close();
        }

        private void addMoreButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(addMoreButton, "Click to add multiple contacts!");
        }
    }
}
