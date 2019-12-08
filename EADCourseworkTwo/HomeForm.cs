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
        IList<Event> eventList;
        DateTime dateTime = DateTime.Now;
        //DateTime dateTime = new DateTime(2020, 01, 02);
        public HomeForm(User user)
        {

            InitializeComponent();
            logedInUser = user;
            this.nameLbl.Text = user.UserName;
            this.addContactBtn.Text = Properties.Resources.addContactBtn;
            this.addEventBtn.Text = Properties.Resources.addEventBtn;
            this.viewContactBtn.Text = Properties.Resources.viewContactButton;
            this.viewEventBtn.Text = Properties.Resources.viewEventsBtn;
            EventModel eventModel = new EventModel();
            eventList = eventModel.getAllEventDetails(user.UserId);
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
            string time = dateTime.ToString("HH:mm");
            string date = dateTime.ToString("yyyy-MM-dd");
            DateTime currentDateTime = DateTime.Parse(date + " " + time);
            foreach (Event evnt in eventList)
            {
                if (eventList.Count > 0)
                {
                    if(evnt.StartingDateTime > currentDateTime)
                    {
                        isSetEvents = true;
                        break;
                    }
                }
            }
            
        }

        private void populateTableLayout(User user)
        {
            IList<Event> monthlyevntList = new List<Event>();
            foreach(Event ev in eventList)
            {
                if(ev.StartingDateTime.Month == dateTime.Month)
                {
                    monthlyevntList.Add(ev);
                }
            }

            label1.Text = dateTime.ToString("MMMM") + " " + dateTime.ToString("yyyy");

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

            DateTime start = new DateTime(dateTime.Year, dateTime.Month, 1);
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
                foreach(Event ev in monthlyevntList)
                {
                    if (ev.StartingDateTime.Day == day)
                    {
                        if (ev.RecurringFlag == 3)
                        {
                            toolTip1.SetToolTip(label, ev.EventTitle + " at " + ev.Location);
                            label.BackColor = ColorTranslator.FromHtml("#ff5252");
                        }
                        else
                        {
                            toolTip1.SetToolTip(label, ev.EventTitle + " at " + ev.Location);
                            label.BackColor = ColorTranslator.FromHtml("#40407a");
                            label.ForeColor = Color.White;
                        }
                    }
                }
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
                foreach (Event ev in monthlyevntList)
                {
                    if (ev.StartingDateTime.Day == day)
                    {
                        if(ev.RecurringFlag == 3)
                        {
                            toolTip1.SetToolTip(label, ev.EventTitle+" at "+ev.Location);
                            label.BackColor = ColorTranslator.FromHtml("#ff5252");
                        }
                        else
                        {
                            //toolTip1.SetToolTip(label, ev.EventTitle + " at " + ev.Location);
                            label.BackColor = ColorTranslator.FromHtml("#40407a");
                            label.ForeColor = Color.White;
                        }
                    }
                }
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
            addContactForm.ShowDialog();
            this.Close();
        }

        private void addEventBtn_Click(object sender, EventArgs e)
        {
            /*AddEventForm addEventForm = new AddEventForm(logedInUser);
            this.Hide();
            addEventForm.Show();
            this.Close();*/

            AddEventsForm addEventsForm = new AddEventsForm(logedInUser);
            this.Hide();
            addEventsForm.ShowDialog();
            this.Close();
        }

        private void viewContactBtn_Click(object sender, EventArgs e)
        {
            if (isSetContacts)
            {
                ViewContactListForm viewContactListForm = new ViewContactListForm(logedInUser);
                this.Hide();
                viewContactListForm.ShowDialog();
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
                viewEventsForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("There are no previously added events to view! Please add an event first!");
            }
        }

        private void generateReport_Click(object sender, EventArgs e)
        {
            Boolean loading = false;
            EventModel eventModel = new EventModel();
            string time = dateTime.ToString("HH:mm");
            string date = dateTime.ToString("yyyy-MM-dd");
            DateTime currentDateTime = DateTime.Parse(date + " " + time);
            foreach (Event evnt in eventList)
            {
                if (eventList.Count > 0)
                {
                    if (evnt.StartingDateTime.Day < currentDateTime.Day)
                    {
                        loading = true;
                        break;
                    }
                }
            }

            if (loading)
            {
                ReportForm reportForm = new ReportForm(logedInUser);
                this.Hide();
                reportForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("There are no past events to genearte a prediction!");
            }
            
        }

        private void logOutBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }
    }
}
