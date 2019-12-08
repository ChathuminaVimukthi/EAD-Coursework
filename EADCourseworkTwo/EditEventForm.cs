using EADCourseworkTwo.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace EADCourseworkTwo
{
    public partial class EditEventForm : Form
    {
        Event evntToEdit;
        User loggedInUser;
        Boolean isSetContacts = true;

        public EditEventForm(Event evnt,User user)
        {
            InitializeComponent();
            evntToEdit = evnt;
            loggedInUser = user;
            this.tableLayoutPanel2.CellPaint += tableLayoutPane2_CellPaint;
            this.startTimePicker.CustomFormat = "HH:mm";
            this.endTimePicker.CustomFormat = "HH:mm";
            initializeComponents();
        }

        private void initializeComponents()
        {
            this.eventTitleTxtBox.Text = evntToEdit.EventTitle;
            this.descriptionTxtBox.Text = evntToEdit.EventDescription;
            DateTime startTime = DateTime.Parse(evntToEdit.StartingDateTime.ToString("HH:mm"));
            DateTime startDate = DateTime.Parse(evntToEdit.StartingDateTime.ToString("yyyy-MM-dd"));
            this.startTimePicker.Value = startTime;
            this.startDatePicker.Value = startDate;
            DateTime endTime = DateTime.Parse(evntToEdit.EndingDateTime.ToString("HH:mm"));
            DateTime endDate = DateTime.Parse(evntToEdit.EndingDateTime.ToString("yyyy-MM-dd"));
            this.endTimePicker.Value = endTime;
            this.endDatePicker.Value = endDate;
            populateCheckedList();
            this.locationTextBox.Text = evntToEdit.Location;
            int evntType = evntToEdit.EventFlag;
            if(evntType == 1)
            {
                this.tastRadBtn.Checked = true;
            }
            else
            {
                this.appointmentRadBtn.Checked = true;
            }
        }

        private void populateCheckedList()
        {
            ContactModel contactModel = new ContactModel();
            IList<Contact> contactList = contactModel.getContact(loggedInUser.UserId);
            IList<int> selectedContact = contactModel.getContactsSelected(evntToEdit.EventId);
            int x = 0;
            if (contactList.Count > 0)
            {
                foreach (Contact contact in contactList)
                {
                    checkedListBox1.Items.Add(new ListItem(contact.ContactName, contact.Id.ToString()));
                    foreach (int id in selectedContact)
                    {
                        if (contact.Id == id)
                        {
                            checkedListBox1.SetItemChecked(x, true);
                        }
                    }
                    x++;
                }
            }
            else
            {
                checkedListBox1.Visible = false;
                isSetContacts = false;
                System.Windows.Forms.Label infoLabel = new System.Windows.Forms.Label();
                infoLabel.Text = "There are no contacts added! Click here to add Contacts";
                infoLabel.Margin = new Padding(3, 3, 3, 3);
                infoLabel.ForeColor = ColorTranslator.FromHtml("#b33939");
                infoLabel.BackColor = ColorTranslator.FromHtml("#f7f1e3");
                infoLabel.TextAlign = ContentAlignment.MiddleCenter;
                infoLabel.Font = new Font("Arial", 12, FontStyle.Bold); ;
                infoLabel.Click += this.ShowContactForm;
                this.tableLayoutPanel2.Controls.Add(infoLabel, 1, 4);
                infoLabel.Dock = DockStyle.Fill;
            }
        }

        private void ShowContactForm(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Label)
            {
                AddContactForm addContactForm = new AddContactForm(loggedInUser);
                addContactForm.Show();
            }
        }

        private void tableLayoutPane2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
        }

        private void addEventBtn_Click(object sender, EventArgs e)
        {
            string title = this.eventTitleTxtBox.Text;
            string location = this.locationTextBox.Text;
            string description = this.descriptionTxtBox.Text;
            string startingTime = this.startTimePicker.Value.ToString("HH:mm");
            string startingDate = this.startDatePicker.Value.ToString("yyyy-MM-dd");
            string endingTime = this.endTimePicker.Value.ToString("HH:mm");
            string endingDate = this.endDatePicker.Value.ToString("yyyy-MM-dd");
            IList<Contact> contactList = new List<Contact>();

            int eventType = 0;
            if (tastRadBtn.Checked)
            {
                eventType = 1;
            }
            else if (appointmentRadBtn.Checked)
            {
                eventType = 2;
            }

            foreach (ListItem item in checkedListBox1.CheckedItems)
            {
                ContactModel contactModel = new ContactModel();
                Contact contact = contactModel.getContactUsingContactId(Convert.ToInt32(item.Value));
                contactList.Add(contact);
            }
            DateTime startingDateTime = DateTime.Parse(startingDate + " " + startingTime);
            DateTime endingDateTime = DateTime.Parse(endingDate + " " + endingTime);

            if (validations())
            {
                Boolean isTimeNotInRange = validateEnteredDate(startingDateTime, endingDateTime);

                if (isTimeNotInRange)
                {
                    ContactModel contactModel = new ContactModel();
                    EventModel eventModel = new EventModel();
                    IList<int> contactIdList = contactModel.getContactsSelected(evntToEdit.EventId);
                    foreach (int cId in contactIdList)
                    {
                        eventModel.deleteEventContacts(evntToEdit.EventId, cId);
                    }

                    Boolean validate = false;
                    Boolean validate1 = false;
                    Event evnt = new Event();
                    evnt.EventId = evntToEdit.EventId;
                    evnt.EventTitle = title;
                    evnt.EventDescription = description;
                    evnt.StartingDateTime = startingDateTime;
                    evnt.EndingDateTime = endingDateTime;
                    evnt.EventFlag = eventType;
                    evnt.Location = location;
                    evnt.UserId = loggedInUser.UserId;
                    evnt.ContactList = contactList;
                    validate = eventModel.updateEvent(evnt);
                    if (isSetContacts)
                    {
                        validate1 = eventModel.updateContactsSelected(evnt);
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
            }
        }

        private Boolean validateEnteredDate(DateTime startDateTime, DateTime endDateTime)
        {
            EventModel eventModel = new EventModel();
            IList<Event> evntList = eventModel.getAllEventDetails(loggedInUser.UserId);
            Boolean startInRange = false;
            Boolean endInRange = false;

            DateTime srtTime = new DateTime();
            DateTime endTime = new DateTime();

            foreach (Event evnt in evntList)
            {
                if (evnt.EventId != evntToEdit.EventId)
                {
                    if (startDateTime >= evnt.StartingDateTime && startDateTime < evnt.EndingDateTime)
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
            }


            if (!startInRange && !endInRange)
            {
                return true;
            }
            else
            {
                MessageBox.Show("There is another event scheduled from " + srtTime.ToString() + " to " + endTime.ToString() +
                    ". Please select another time range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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
            if (isSetContacts)
            {
                if (checkedListBox1.CheckedIndices.Count == 0)
                {
                    isOkay = false;
                    errorProviderEvent.SetError(checkedListBox1, "Select atleast one contact!");
                }
                else
                {
                    errorProviderEvent.SetError(checkedListBox1, "");
                }
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

        private void addContact_Click(object sender, EventArgs e)
        {
            AddContactForm addContactForm = new AddContactForm(loggedInUser);
            this.Hide();
            addContactForm.Show();
            this.Close();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedInUser);
            this.Hide();
            homeForm.Show();
            this.Close();
        }
    }
}
