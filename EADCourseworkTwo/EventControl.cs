using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EADCourseworkTwo
{
    public partial class EventControl : UserControl
    {
        private int eventId;
        private int eventType;
        private Boolean isRecurring;
        private int recurringId;

        public EventControl()
        {
            InitializeComponent();
            this.pictureBox1.Image = Properties.Resources.info;
            
        }

        public string Title
        {
            set
            {
                this.evntTitleLbl.Text = value;
            }
            get
            {
                return this.evntTitleLbl.Text;
            }
        }

        public string Description
        {
            set
            {
                this.evntDescLbl.Text = value;
            }
            get
            {
                return this.evntDescLbl.Text;
            }
        }

        public string StartingTime
        {
            set
            {
                this.startTimeLbl.Text = value;
            }
            get
            {
                return this.startTimeLbl.Text;
            }
        }

        public string EndingTime
        {
            set
            {
                this.endTimeLbl.Text = value;
            }
            get
            {
                return this.endTimeLbl.Text;
            }
        }

        public int EventId { get => eventId; set => eventId = value; }
        public int EventType { get => eventType; set => eventType = value; }
        public bool IsRecurring { get => isRecurring; set => isRecurring = value; }
        public int RecurringId { get => recurringId; set => recurringId = value; }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if(this.EventType == 1)
            {
                toolTip1.SetToolTip(pictureBox1, "Task");
            }
            else
            {
                toolTip1.SetToolTip(pictureBox1, "Appointment");
            }
        }
    }
}
