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
    public partial class AddEventForm : Form
    {
        User loggedInUser;
        int recurringAmount = 0;
        public AddEventForm(User user)
        {
            InitializeComponent();
            loggedInUser = user;
            this.tableLayoutPanel2.CellPaint += tableLayoutPane2_CellPaint;
            this.startDatePicker.MinDate = DateTime.Now;
            this.endDatePicker.MinDate = DateTime.Now;
            populateCheckedList();
            this.startTimePicker.CustomFormat = "HH:mm";
            this.endTimePicker.CustomFormat = "HH:mm";
            this.tableLayoutPanel5.Controls.Add(startTimePicker,0,1);
            this.tableLayoutPanel6.Controls.Add(endTimePicker,0,1);
        }

        private void tableLayoutPane2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
        } 

        private void populateCheckedList()
        {
            ContactModel contactModel = new ContactModel();
            IList<Contact> contactList = contactModel.getContact(loggedInUser.UserId);
            foreach (Contact contact in contactList)
            {
                checkedListBox1.Items.Add(contact.ContactName);
            }
           
        }

        private void addEventBtn_Click(object sender, EventArgs e)
        {
            //recurring: 1 = Daily, 2 = Monthly, 3 = One-Off
            //eventType: 1 = Task, 2 = Appointment
            string title = this.eventTitleTxtBox.Text;
            string location = this.locationTextBox.Text;
            string description = this.descriptionTxtBox.Text;
            string startingTime = this.startTimePicker.Value.ToString("HH:mm");
            string startingDate = this.startDatePicker.Value.ToString("yyyy-MM-dd");
            string endingTime = this.endTimePicker.Value.ToString("HH:mm");
            string endingDate = this.endDatePicker.Value.ToString("yyyy-MM-dd");
            IList<Contact> contactList = new List<Contact>();
            int recurring = 0;
            if (dailyRadioBtn.Checked)
            {
                recurring = 1;
            }else if (monthlyRadioBtn.Checked)
            {
                recurring = 2;
            }else if (oneOffRadioBtn.Checked)
            {
                recurring = 3;
            }

            int eventType = 0;
            if (tastRadBtn.Checked)
            {
                eventType = 1;
            }else if (appointmentRadBtn.Checked)
            {
                eventType = 2;
            }

            foreach (string item in checkedListBox1.CheckedItems)
            {
                ContactModel contactModel = new ContactModel();
                Contact contact = contactModel.getContactUsingContactName(item);
                contactList.Add(contact);
            }

            DateTime startingDateTime = DateTime.Parse(startingDate+" "+startingTime);
            DateTime endingDateTime = DateTime.Parse(endingDate+" "+endingTime);

            if(validations()) 
            {
                Boolean isTimeNotInRange = validateEnteredDate(startingDateTime,endingDateTime);
                recurringAmount = Convert.ToInt32(this.numericUpDown1.Value);

                if (isTimeNotInRange)
                {
                    EventModel eventModel = new EventModel();
                    if (recurring == 1)
                    {
                        Boolean validate = false;
                        Boolean validate1 = false;
                        int recId = eventModel.getEventId() + 1;
                        for (int x = 0; x < recurringAmount; x++)
                        {
                            Event evnt = new Event();
                            evnt.EventTitle = title;
                            evnt.EventDescription = description;
                            evnt.StartingDateTime = startingDateTime.AddDays(x);
                            evnt.EndingDateTime = endingDateTime.AddDays(x);
                            evnt.EventFlag = eventType;
                            evnt.RecurringFlag = recurring;
                            evnt.Location = location;
                            evnt.UserId = loggedInUser.UserId;
                            evnt.RecurringId = recId;
                            evnt.ContactList = contactList;
                            validate = eventModel.addEvent(evnt);
                            validate1 = eventModel.addContactsSelected(evnt);
                        }
                        if (validate && validate1)
                        {
                            MessageBox.Show("Event Saved Successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Database connection failed.Try again !");
                        }
                    }
                    if(recurring == 2)
                    {
                        Boolean validate = false;
                        Boolean validate1 = false;
                        int recId = eventModel.getEventId() + 1;
                        for (int x = 0; x < recurringAmount; x++)
                        {
                            Event evnt = new Event();
                            evnt.EventTitle = title;
                            evnt.EventDescription = description;
                            evnt.StartingDateTime = startingDateTime.AddDays(x);
                            evnt.EndingDateTime = endingDateTime.AddDays(x);
                            evnt.EventFlag = eventType;
                            evnt.RecurringFlag = recurring;
                            evnt.Location = location;
                            evnt.UserId = loggedInUser.UserId;
                            evnt.RecurringId = recId;
                            evnt.ContactList = contactList;
                            validate = eventModel.addEvent(evnt);
                            validate1 = eventModel.addContactsSelected(evnt);
                        }
                        if (validate && validate1)
                        {
                            MessageBox.Show("Event Saved Successfully!");
                        } 
                        else
                        {
                            MessageBox.Show("Database connection failed.Try again !");
                        }
                    }
                    if (recurring == 3)
                    {
                        Event evnt = new Event();
                        evnt.EventTitle = title;
                        evnt.EventDescription = description;
                        evnt.StartingDateTime = startingDateTime;
                        evnt.EndingDateTime = endingDateTime;
                        evnt.EventFlag = eventType;
                        evnt.RecurringFlag = recurring;
                        evnt.Location = location;
                        evnt.UserId = loggedInUser.UserId;
                        evnt.ContactList = contactList;

                        Boolean addedEvent = eventModel.addEvent(evnt);
                        Boolean addedContactList = eventModel.addContactsSelected(evnt);

                        if (addedEvent && addedContactList)
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
            else
            {
                MessageBox.Show("Please Fill the Required Fields !");
            }
        }


        private Boolean validateEnteredDate(DateTime startDateTime,DateTime endDateTime)
        {
            EventModel eventModel = new EventModel();
            IList<Event> evntList = eventModel.getAllEventDetails(loggedInUser.UserId);

            Boolean startInRange = false;
            Boolean endInRange = false;

            DateTime srtTime = new DateTime();
            DateTime endTime = new DateTime();

            foreach(Event evnt in evntList)
            {
                if(startDateTime >= evnt.StartingDateTime && startDateTime < evnt.EndingDateTime)
                {
                    srtTime = evnt.StartingDateTime;
                    endTime = evnt.EndingDateTime;
                    startInRange = true;
                    break;
                }
                else
                {
                    startInRange = false;
                    if (endDateTime >= evnt.StartingDateTime && endDateTime < evnt.EndingDateTime)
                    {
                        srtTime = evnt.StartingDateTime;
                        endTime = evnt.EndingDateTime;
                        endInRange = true;
                        break;
                    }
                    else
                    {
                        endInRange = false;
                    }
                }
            }


            if (!startInRange && !endInRange)
            {
                return true;
            }
            else
            {
                MessageBox.Show("There is another event scheduled from " + srtTime.ToString() + " to " + endTime.ToString()+
                    ". Please select another time range!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }


        private void startTimePicker_MouseDown(object sender, MouseEventArgs e)
        {
            this.startTimePicker.CustomFormat = "HH:mm";
        }

        private void endTimePicker_MouseDown(object sender, MouseEventArgs e)
        {
            this.endTimePicker.CustomFormat = "HH:mm";
        }


        private Boolean validations()
        {
            Boolean isOkay = true;
            string startingTime = this.startTimePicker.Value.ToString("HH:mm");
            string startingDate = this.startDatePicker.Value.ToString("yyyy-MM-dd");
            string endingTime = this.endTimePicker.Value.ToString("HH:mm");
            string endingDate = this.endDatePicker.Value.ToString("yyyy-MM-dd");
            DateTime startingDateTime = DateTime.Parse(startingDate + " " + startingTime);
            DateTime endingDateTime = DateTime.Parse(endingDate + " " + endingTime);

            if (string.IsNullOrWhiteSpace(eventTitleTxtBox.Text))
            {
                isOkay = false;
                errorProviderEvent.SetError(eventTitleTxtBox, "Title should not be left blank!");
            }
            else
            {
                errorProviderEvent.SetError(eventTitleTxtBox, "");
            }
            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                isOkay = false;
                errorProviderEvent.SetError(locationTextBox, "Location should not be left blank!");
            }
            else
            {
                errorProviderEvent.SetError(locationTextBox, "");
            }
            if (string.IsNullOrWhiteSpace(descriptionTxtBox.Text))
            {
                isOkay = false;
                errorProviderEvent.SetError(descriptionTxtBox, "Description should not be left blank!");
            }
            else
            {
                errorProviderEvent.SetError(descriptionTxtBox, "");
            }
            if (startingDateTime >= endingDateTime)
            {
                isOkay = false;
                errorProviderEvent.SetError(tableLayoutPanel6, "End date has to be bigger than Starting date!");
            }
            else
            {
                errorProviderEvent.SetError(tableLayoutPanel6, "");
            }
            if (checkedListBox1.CheckedIndices.Count == 0)
            {
                isOkay = false;
                errorProviderEvent.SetError(checkedListBox1, "Select atleast one contact!");
            }
            else
            {
                errorProviderEvent.SetError(checkedListBox1, "");
            }
            if (!monthlyRadioBtn.Checked && !dailyRadioBtn.Checked && !oneOffRadioBtn.Checked)
            {
                isOkay = false;
                errorProviderEvent.SetError(tableLayoutPanel3, "Select atleast one option!");
            }
            else
            {
                errorProviderEvent.SetError(tableLayoutPanel3, "");
            }
            if (!appointmentRadBtn.Checked && !tastRadBtn.Checked)
            {
                isOkay = false;
                errorProviderEvent.SetError(tableLayoutPanel4, "Select atleast one option!");
            }
            else
            {
                errorProviderEvent.SetError(tableLayoutPanel4, "");
            }
            return isOkay;
        }

        private void dailyRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = true;
            recurringAmount =  Convert.ToInt32(this.numericUpDown1.Value);
        }

        private void oneOffRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = false;
            recurringAmount = 0;
        }

        private void monthlyRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = true;
            recurringAmount = Convert.ToInt32(this.numericUpDown1.Value);
        }
    }
}
