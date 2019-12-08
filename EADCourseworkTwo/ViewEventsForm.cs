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
    public partial class ViewEventsForm : Form
    {
        User loggedInUser;
        public ViewEventsForm(User user)
        {
            InitializeComponent();
            loggedInUser = user;
            populateList();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button temp = (Button)sender;
                TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)temp.Parent;
                TableLayoutPanel tableLayoutPanel1 = (TableLayoutPanel)tableLayoutPanel.Parent;
                EventControl eventControl = (EventControl)tableLayoutPanel1.Parent;
                if (eventControl.IsRecurring)
                {
                    DialogResult result = MessageBox.Show("Do you want to delete all recurring events?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        EventModel eventModel = new EventModel();
                        ContactModel contactModel = new ContactModel();
                        IList<Event> recuringEvntList = eventModel.getRecurringEventLis(eventControl.RecurringId);
                        IList<int> contactIdList = contactModel.getContactsSelected(eventControl.EventId);
                 
                        foreach (Event evnt in recuringEvntList)
                        {
                            foreach (int cId in contactIdList)
                            {
                                eventModel.deleteEventContacts(evnt.EventId, cId);
                            }

                            eventModel.deleteRecurringEvent(loggedInUser.UserId, evnt.EventId);
                            foreach (EventControl evContrl in flowLayoutPanel1.Controls.OfType<EventControl>())
                            {
                                if (evContrl.EventId == evnt.EventId)
                                {
                                    evContrl.Dispose();
                                }
                            }
                        }

                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to delete event?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        EventModel eventModel = new EventModel();
                        ContactModel contactModel = new ContactModel();
                        IList<int> contactIdList = contactModel.getContactsSelected(eventControl.EventId);
                        foreach (int cId in contactIdList)
                        {
                            eventModel.deleteEventContacts(eventControl.EventId, cId);
                        }

                        Boolean isDeleted = eventModel.deleteEvent(eventControl.EventId, loggedInUser.UserId);
                        if (isDeleted)
                        {
                            eventControl.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Delete failed due to database connection error !");
                        }
                    }
                }
                
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button temp = (Button)sender;
                TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)temp.Parent;
                TableLayoutPanel tableLayoutPanel1 = (TableLayoutPanel)tableLayoutPanel.Parent;
                EventControl eventControl = (EventControl)tableLayoutPanel1.Parent;
                EventModel eventModel = new EventModel();
                EditEventForm editEventForm = new EditEventForm(eventModel.getSingleEvent(eventControl.EventId),loggedInUser);
                editEventForm.Show();
            }
        }

        private void populateList()
        {
            EventModel eventModel = new EventModel();
            IList<Event> eventList = eventModel.getAllEventDetails(loggedInUser.UserId);

            foreach (Event evnt in eventList)
            {
                EventControl  eventControl = new EventControl();

                if (evnt.RecurringFlag == 1 || evnt.RecurringFlag == 2)
                {
                    eventControl.editEvntBtn.Visible = false;
                    eventControl.IsRecurring = true;
                    eventControl.BackColor = ColorTranslator.FromHtml("#706fd3");
                }
                else if (evnt.RecurringFlag == 3)
                {
                    eventControl.IsRecurring = false;
                    eventControl.BackColor = ColorTranslator.FromHtml("#33d9b2");
                }

                eventControl.Title = evnt.EventTitle;
                eventControl.Description = evnt.EventDescription;
                eventControl.StartingTime = evnt.StartingDateTime.ToString();
                eventControl.EndingTime = evnt.EndingDateTime.ToString();
                eventControl.EventId = evnt.EventId;
                eventControl.EventType = evnt.EventFlag;
                eventControl.RecurringId = evnt.RecurringId;
                eventControl.dltEvntbtn.Click += DeleteBtn_Click;
                eventControl.editEvntBtn.Click += EditBtn_Click;
                flowLayoutPanel1.Controls.Add(eventControl);
            }
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedInUser);
            this.Hide();
            homeForm.Show();
            this.Close();
        }

        private void addEvent_Click(object sender, EventArgs e)
        {
            AddEventForm addEventForm = new AddEventForm(loggedInUser);
            this.Hide();
            addEventForm.Show();
            this.Close();
        }
    }
}
