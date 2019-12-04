using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EADCourseworkTwo.model
{
    class Event
    {
        private string eventTitle;
        private string eventDescription;
        private DateTime startingDateTime;
        private DateTime endingDateTime;
        private IList<Contact> contactList;
        private int eventFlag;
        private int recurringFlag;

        public string EventTitle { get => eventTitle; set => eventTitle = value; }
        public string EventDescription { get => eventDescription; set => eventDescription = value; }
        public DateTime StartingDateTime { get => startingDateTime; set => startingDateTime = value; }
        public DateTime EndingDateTime { get => endingDateTime; set => endingDateTime = value; }
        public int EventFlag { get => eventFlag; set => eventFlag = value; }
        public int RecurringFlag { get => recurringFlag; set => recurringFlag = value; }
        internal IList<Contact> ContactList { get => contactList; set => contactList = value; }
    }
}
