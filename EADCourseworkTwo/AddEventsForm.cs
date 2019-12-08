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
    public partial class AddEventsForm : Form
    {
        User loggedInUser;
        public AddEventsForm(User user)
        {
            InitializeComponent();
            loggedInUser = user;
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedInUser);
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private void addContact_Click(object sender, EventArgs e)
        {
            AddContactForm addContactForm = new AddContactForm(loggedInUser);
            this.Hide();
            addContactForm.ShowDialog();
            this.Close();
        }

        private void generateEventsButn_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            int amount = Convert.ToInt32(this.numericUpDown1.Value);
            for (int i = 0; i < amount; i++)
            {
                AddEventControl addEventControl = new AddEventControl(loggedInUser);
                addEventControl.addEventBtn.Click += SaveButton_Click;
                this.flowLayoutPanel1.Controls.Add(addEventControl);
            }
        }

        public void SaveButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button temp = (Button)sender;
                TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)temp.Parent;
                AddEventControl addEventControl = (AddEventControl)tableLayoutPanel.Parent;

                if (addEventControl.IsValidated)
                {
                    if (addEventControl.IsDateInRange)
                    {
                        EventModel eventModel = new EventModel();
                        int recId = eventModel.getEventId() + 1;
                        string title = addEventControl.EventTitle;
                        string location = addEventControl.EventLocation;
                        string description = addEventControl.EventDescription;
                        DateTime startingTime = addEventControl.StartingDateTime;
                        DateTime endingTime = addEventControl.EndingDateTime;
                        int eventFlag = addEventControl.EventFlag;
                        int userId = loggedInUser.UserId;
                        int recurringId = recId;
                        IList<Contact> contactList = addEventControl.ContactsSelected;
                        int recurringflag = addEventControl.RecurringFlag;

                        if(recurringflag == 1)
                        {
                            Boolean validate = false;
                            Boolean validate1 = false;
                            for (int x = 0; x < addEventControl.RecurringAmount; x++)
                            {
                                Event evnt = new Event();
                                evnt.EventTitle = title;
                                evnt.EventDescription = description;
                                evnt.StartingDateTime = startingTime.AddDays(x);
                                evnt.EndingDateTime = endingTime.AddDays(x);
                                evnt.EventFlag = eventFlag;
                                evnt.RecurringFlag = recurringflag;
                                evnt.Location = location;
                                evnt.UserId = loggedInUser.UserId;
                                evnt.RecurringId = recurringId;
                                evnt.ContactList = contactList;
                                validate = eventModel.addEvent(evnt);
                                if (addEventControl.IsContactsSet)
                                {
                                    validate1 = eventModel.addContactsSelected(evnt);
                                }
                            }
                            if (addEventControl.IsContactsSet)
                            {
                                if (validate && validate1)
                                {
                                    MessageBox.Show("Event Saved Successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Database connection failed.Try again !");
                                }
                            }
                            else
                            {
                                if (validate)
                                {
                                    MessageBox.Show("Event Saved Successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Database connection failed.Try again !");
                                }
                            }
                        }
                        if(recurringflag == 2)
                        {
                            Boolean validate = false;
                            Boolean validate1 = false;
                            for (int x = 0; x < addEventControl.RecurringAmount; x++)
                            {
                                Event evnt = new Event();
                                evnt.EventTitle = title;
                                evnt.EventDescription = description;
                                evnt.StartingDateTime = startingTime.AddDays(30*x);
                                evnt.EndingDateTime = endingTime.AddDays(30*x);
                                evnt.EventFlag = eventFlag;
                                evnt.RecurringFlag = recurringflag;
                                evnt.Location = location;
                                evnt.UserId = loggedInUser.UserId;
                                evnt.RecurringId = recurringId;
                                evnt.ContactList = contactList;
                                validate = eventModel.addEvent(evnt);
                                if (addEventControl.IsContactsSet)
                                {
                                    validate1 = eventModel.addContactsSelected(evnt);
                                }
                            }
                            if (addEventControl.IsContactsSet)
                            {
                                if (validate && validate1)
                                {
                                    MessageBox.Show("Event Saved Successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Database connection failed.Try again !");
                                }
                            }
                            else
                            {
                                if (validate)
                                {
                                    MessageBox.Show("Event Saved Successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Database connection failed.Try again !");
                                }
                            }
                        }
                        if(recurringflag == 3)
                        {
                            Event evnt = new Event();
                            evnt.EventTitle = title;
                            evnt.EventDescription = description;
                            evnt.StartingDateTime = startingTime;
                            evnt.EndingDateTime = endingTime;
                            evnt.EventFlag = eventFlag;
                            evnt.RecurringFlag = recurringflag;
                            evnt.Location = location;
                            evnt.UserId = loggedInUser.UserId;
                            evnt.ContactList = contactList;

                            Boolean addedEvent = eventModel.addEvent(evnt);
                            Boolean addedContactList = false;
                            if (addEventControl.IsContactsSet)
                            {
                                addedContactList = eventModel.addContactsSelected(evnt);
                                if (addedEvent && addedContactList)
                                {
                                    MessageBox.Show("Event Saved Successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Database connection failed.Try again !");
                                }
                            }
                            else
                            {
                                if (addedEvent)
                                {
                                    MessageBox.Show("Event Saved Successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Database connection failed.Try again !");
                                }
                            }
                        }
                       
                    }
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
    }
}
