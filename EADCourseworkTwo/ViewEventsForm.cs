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

        private void populateList()
        {
            EventModel eventModel = new EventModel();
            IList<Event> eventList = eventModel.getAllEventDetails(loggedInUser.UserId);

            foreach (Event evnt in eventList)
            {
                EventControl  eventControl = new EventControl(evnt.EventFlag);

                if (evnt.RecurringFlag == 1 || evnt.RecurringFlag == 2)
                {
                    eventControl.BackColor = ColorTranslator.FromHtml("#706fd3");
                }
                else if (evnt.RecurringFlag == 3)
                {
                    eventControl.BackColor = ColorTranslator.FromHtml("#33d9b2");
                }

                eventControl.Title = evnt.EventTitle;
                eventControl.Description = evnt.EventDescription;
                eventControl.StartingTime = evnt.StartingDateTime.ToString();
                eventControl.EndingTime = evnt.EndingDateTime.ToString();
                eventControl.EventId = evnt.EventId;
                eventControl.EventType = evnt.EventFlag;
                flowLayoutPanel1.Controls.Add(eventControl);
            }
        }
    }
}
