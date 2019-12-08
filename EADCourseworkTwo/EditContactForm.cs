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
    public partial class EditContactForm : Form
    {
        User loggedInUser;
        int contactId;
        public EditContactForm(User user,int cntId)
        {
            InitializeComponent();
            loggedInUser = user;
            contactId = cntId;
            showAddForm();
        }

        private void showAddForm()
        {
            ContactModel contactModel = new ContactModel();
            Contact contact = contactModel.getContactUsingContactId(contactId);
            AddContact addContact = new AddContact();
            addContact.ContactName = contact.ContactName;
            addContact.ContactNumber = contact.ContactNumber;
            addContact.Email = contact.Email;
            addContact.saveContact.Text = "Update";
            addContact.saveContact.Click += SaveButton_Click;

            tableLayoutPanel2.Controls.Add(addContact, 0, 1  );
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

                if (name == "")
                {
                    MessageBox.Show("User Name cannot be empty !");
                }
                else if (email == "")
                {
                    MessageBox.Show("Enter valid Email Address !");
                }
                else if (contactNumber == 0)
                {
                    MessageBox.Show("Enter valid Mobile Number !");
                }
                else
                {
                    Contact contact = new Contact();
                    contact.Id = contactId;
                    contact.ContactName = name;
                    contact.Email = email;
                    contact.ContactNumber = contactNumber;
                    contact.UserId = loggedInUser.UserId;

                    ContactModel contactModel = new ContactModel();
                    Boolean isUpdated = contactModel.updateContact(contact);
                    if (isUpdated)
                    {
                        MessageBox.Show("Contact Updated !");
                    }
                    else
                    {
                        MessageBox.Show("Failed !");
                    }
                }
            }
        }



        private void contactListBtn_Click(object sender, EventArgs e)
        {
            ViewContactListForm viewContactListForm = new ViewContactListForm(loggedInUser);
            this.Hide();
            viewContactListForm.Show();
            this.Close();

        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedInUser);
            this.Hide();
            homeForm.Show();
            this.Close();
        }

        private void viewContactList_Click(object sender, EventArgs e)
        {
            ViewContactListForm viewContactListForm = new ViewContactListForm(loggedInUser);
            this.Hide();
            viewContactList.Show();
            this.Close();
        }
    }
}
