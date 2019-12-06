﻿using EADCourseworkTwo.model;
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
    public partial class ViewContactListForm : Form
    {
        User loggedInUser;
        public ViewContactListForm(User user)
        {
            InitializeComponent();
            loggedInUser = user;
            populateList();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if(sender is Button)
            {
                Button temp = (Button)sender;
                TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)temp.Parent;
                TableLayoutPanel tableLayoutPanel1 = (TableLayoutPanel)tableLayoutPanel.Parent;
                ContactControl contactControl = (ContactControl)tableLayoutPanel1.Parent;
                int id = contactControl.Id;
                EditContactForm editContactForm = new EditContactForm(loggedInUser,id);
                editContactForm.Show();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button temp = (Button)sender;
                TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)temp.Parent;
                TableLayoutPanel tableLayoutPanel1 = (TableLayoutPanel)tableLayoutPanel.Parent;
                ContactControl contactControl = (ContactControl)tableLayoutPanel1.Parent;
                int id = contactControl.Id;
                DialogResult result = MessageBox.Show("Do you want to delete contact?", "Warning", MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    ContactModel contactModel = new ContactModel();
                    Boolean isDeleted = contactModel.deleteContact(id,loggedInUser.UserId);
                    if (isDeleted)
                    {
                        contactControl.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed due to database connection error !");
                    }
                }
            }
        }


        private void populateList()
        {
            ContactModel contactModel = new ContactModel();
            IList<Contact> contactList = contactModel.getContact(loggedInUser.UserId);

            foreach (Contact contact in contactList)
            {
                ContactControl contactControl = new ContactControl();
                contactControl.BackColor = ColorTranslator.FromHtml("#706fd3");
                contactControl.contactNameLabel.Text = contact.ContactName;
                contactControl.Id = contact.Id;
                contactControl.contactNumberLabel.Text = contact.ContactNumber.ToString();
                contactControl.emailLabel.Text = contact.Email;
                contactControl.editCntactBtn.Click += EditButton_Click;
                contactControl.dltCntctBtn.Click += DeleteButton_Click;
                flowLayoutPanel1.Controls.Add(contactControl);
            }
        }
    }
}
