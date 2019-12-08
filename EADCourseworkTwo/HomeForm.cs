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
    public partial class HomeForm : Form
    {
        User logedInUser;
        Boolean isSetContacts = false;
        Boolean isSetEvents = false;
        public HomeForm(User user)
        {
            InitializeComponent();
            logedInUser = user;
            this.addContactBtn.Text = Properties.Resources.addContactBtn;
            this.addEventBtn.Text = Properties.Resources.addEventBtn;
            this.viewContactBtn.Text = Properties.Resources.viewContactButton;
            this.viewEventBtn.Text = Properties.Resources.viewEventsBtn;
            setGlobalFlags();
            populateTableLayout(user);
        }

        public void setGlobalFlags()
        {
            ContactModel contactModel = new ContactModel();
            IList<Contact> contactList = contactModel.getContact(logedInUser.UserId);
            if (contactList.Count > 0)
            {
                isSetContacts = true;
            }

            EventModel eventModel = new EventModel();
            IList<Event> eventList = eventModel.getAllEventDetails(logedInUser.UserId);
            if(eventList.Count> 0)
            {
                isSetEvents = true;
            }
        }

        private void populateTableLayout(User user)
        {
            Label labelMonth = new Label();
            labelMonth.Text = "December";
            labelMonth.ForeColor = Color.Black;
            labelMonth.BackColor = Color.White;
            labelMonth.TextAlign = ContentAlignment.MiddleCenter;
            tableLayoutPanel2.Controls.Add(labelMonth, 0, 0);
            labelMonth.Dock = DockStyle.Fill;

            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            for(int i = 0; i < tableLayoutPanel3.ColumnCount; i++)
            {
                Label label = new Label();
                label.Text = days[i];
                label.ForeColor = Color.White;
                label.BackColor = Color.Black;
                label.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel3.Controls.Add(label, i, 0);
                label.Dock = DockStyle.Fill;
            }

            DateTime dt = DateTime.Now;

            DateTime start = new DateTime(dt.Year, dt.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);

            string startDay = start.DayOfWeek.ToString();
            int numOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int startingColumn = 0;
            //string startDay = "Monday";
            //int numOfDays = 31;
            int topRow = 0;
            for(int i = 0; i < days.Length; i++)
            {
                topRow = 7 - i;
                if (startDay.Equals(days[i])) {
                    break;
                }
                startingColumn++;
            }

            int count = 1;
            for(int i = startingColumn; i < tableLayoutPanel3.ColumnCount; i++)
            {
                int day = count++;
                Label label = new Label();
                label.Text = day+"";
                label.ForeColor = Color.Black;
                label.BackColor = Color.White;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel3.Controls.Add(label,i, 1);
                label.Dock = DockStyle.Fill;
            }

            int rest = numOfDays - topRow;
            for (int col = 0; col < rest; col++){
                    int day = col + topRow + 1;
                    Label label = new Label();
                    label.Text = day + "";
                    label.ForeColor = Color.Black;
                    label.BackColor = Color.White;
                    label.BorderStyle = BorderStyle.FixedSingle;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    tableLayoutPanel3.Controls.Add(label, col, 2);
                    label.Dock = DockStyle.Fill;
                
            }
        }

        private void addContactBtn_Click(object sender, EventArgs e)
        {
            AddContactForm addContactForm = new AddContactForm(logedInUser);
            this.Hide();
            addContactForm.Show();
            this.Close();
        }

        private void addEventBtn_Click(object sender, EventArgs e)
        {
            /*AddEventForm addEventForm = new AddEventForm(logedInUser);
            this.Hide();
            addEventForm.Show();
            this.Close();*/

            AddEventsForm addEventsForm = new AddEventsForm(logedInUser);
            addEventsForm.Show();
        }

        private void viewContactBtn_Click(object sender, EventArgs e)
        {
            if (isSetContacts)
            {
                ViewContactListForm viewContactListForm = new ViewContactListForm(logedInUser);
                this.Hide();
                viewContactListForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("There are no previously added contacts to view! Please add a contact first!");
            }
            
        }

        private void viewEventBtn_Click(object sender, EventArgs e)
        {
            if (isSetEvents)
            {
                ViewEventsForm viewEventsForm = new ViewEventsForm(logedInUser);
                this.Hide();
                viewEventsForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("There are no previously added events to view! Please add an event first!");
            }
        }

        private void generateReport_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm(logedInUser);
            reportForm.Show();
        }
    }
}
