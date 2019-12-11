using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EADCourseworkTwo.model;
using System.Web.UI.WebControls;

namespace EADCourseworkTwo
{
    public partial class AddEventControl : UserControl
    {
        User loggedInUser;
        int recurringAmount = 0;
        Boolean isSetContacts = true;

        public AddEventControl(User user)
        {
            InitializeComponent();
            loggedInUser = user;
           // this.tableLayoutPanel2.CellPaint += tableLayoutPane2_CellPaint;
            this.startDatePicker.MinDate = DateTime.Now;
            this.endDatePicker.MinDate = DateTime.Now;
            populateCheckedList();
            this.startTimePicker.CustomFormat = "HH:mm";
            this.endTimePicker.CustomFormat = "HH:mm";
            this.tableLayoutPanel5.Controls.Add(startTimePicker, 0, 1);
            this.tableLayoutPanel6.Controls.Add(endTimePicker, 0, 1);
        }

        private void tableLayoutPane2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
        }

        private void populateCheckedList()
        {
            ContactModel contactModel = new ContactModel();
            IList<Contact> contactList = contactModel.getContact(loggedInUser.UserId);
            if (contactList.Count > 0)
            {
                foreach (Contact contact in contactList)
                {
                    checkedListBox1.Items.Add(new ListItem(contact.ContactName, contact.Id.ToString()));
                }
            }
            else
            {
                checkedListBox1.Visible = false;
                isSetContacts = false;
                System.Windows.Forms.Label infoLabel = new System.Windows.Forms.Label();
                infoLabel.Text = "There are no contacts added!";
                infoLabel.Margin = new Padding(3, 3, 3, 3);
                infoLabel.ForeColor = ColorTranslator.FromHtml("#b33939");
                infoLabel.BackColor = ColorTranslator.FromHtml("#f7f1e3");
                infoLabel.TextAlign = ContentAlignment.MiddleCenter;
                infoLabel.Font = new Font("Arial", 12, FontStyle.Bold); ;
                this.tableLayoutPanel2.Controls.Add(infoLabel, 1, 4);
                infoLabel.Dock = DockStyle.Fill;
            }
        }

        public string EventTitle
        {
            get
            {
                return eventTitleTxtBox.Text;
            }
        }

        public string EventDescription
        {
            get
            {
                return descriptionTxtBox.Text;
            }
        }

        public DateTime StartingDateTime
        {
            get
            {
                string startingTime = this.startTimePicker.Value.ToString("HH:mm");
                string startingDate = this.startDatePicker.Value.ToString("yyyy-MM-dd");
                DateTime startingDateTime = DateTime.Parse(startingDate + " " + startingTime);
                return startingDateTime;
            }
        }

        public DateTime EndingDateTime
        {
            get
            {
                string endingTime = this.endTimePicker.Value.ToString("HH:mm");
                string endingDate = this.endDatePicker.Value.ToString("yyyy-MM-dd");
                DateTime endingDateTime = DateTime.Parse(endingDate + " " + endingTime);
                return endingDateTime;
            }
        }

        public IList<Contact> ContactsSelected
        {
            get
            {
                IList<Contact> contactList = new List<Contact>();
                foreach (ListItem item in checkedListBox1.CheckedItems)
                {
                    ContactModel contactModel = new ContactModel();
                    Contact contact = contactModel.getContactUsingContactId(Convert.ToInt32(item.Value));
                    contactList.Add(contact);
                }
                return contactList;
            }
        }

        public int RecurringFlag
        {
            get
            {
                int recurring = 0;
                if (dailyRadioBtn.Checked)
                {
                    recurring = 1;
                }
                else if (monthlyRadioBtn.Checked)
                {
                    recurring = 2;
                }
                else if (oneOffRadioBtn.Checked)
                {
                    recurring = 3;
                }
                return recurring;
            }
        }

        public int RecurringAmount
        {
            get
            {
                return recurringAmount = Convert.ToInt32(this.numericUpDown1.Value);
            }
        }

        public int EventFlag
        {
            get
            {
                int eventType = 0;
                if (tastRadBtn.Checked)
                {
                    eventType = 1;
                }
                else if (appointmentRadBtn.Checked)
                {
                    eventType = 2;
                }
                return eventType;
            }
        }

        public string EventLocation
        {
            get
            {
                return locationTextBox.Text;
            }
        }


        public Boolean IsValidated
        {
            get
            {
                return validations();
            }
        }

        public Boolean IsDateInRange
        {
            get
            {
                return validateEnteredDate(this.StartingDateTime, this.EndingDateTime);
            }
        }

        public Boolean IsContactsSet
        {
            get
            {
                return isSetContacts;
            }
        }

        private void addEventBtn_Click(object sender, EventArgs e)
        {
            //validations();
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

        private void oneOffRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = false;
        }

        private void dailyRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = true;
        }

        private void monthlyRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = true;
        }
    }
}
