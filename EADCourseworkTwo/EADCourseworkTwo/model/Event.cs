using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EADCourseworkTwo.model
{
    public class Event
    {
        private int eventId;
        private string eventTitle;
        private string eventDescription;
        private DateTime startingDateTime;
        private DateTime endingDateTime;
        private IList<Contact> contactList;
        private string location;
        private int eventFlag;
        private int recurringFlag;
        private int recurringId;
        private int userId;

        public string EventTitle { get => eventTitle; set => eventTitle = value; }
        public string EventDescription { get => eventDescription; set => eventDescription = value; }
        public DateTime StartingDateTime { get => startingDateTime; set => startingDateTime = value; }
        public DateTime EndingDateTime { get => endingDateTime; set => endingDateTime = value; }
        public int EventFlag { get => eventFlag; set => eventFlag = value; }
        public int RecurringFlag { get => recurringFlag; set => recurringFlag = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public int UserId { get => userId; set => userId = value; }
        public string Location { get => location; set => location = value; }
        public int RecurringId { get => recurringId; set => recurringId = value; }
        internal IList<Contact> ContactList { get => contactList; set => contactList = value; }
    }
}
