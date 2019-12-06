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

        public EventControl(int evType)
        {
            InitializeComponent();
            updateIcon(evType);
            
        }

        private void updateIcon(int evType)
        {
            if(evType == 1)
            {
                this.pictureBox1.Image = Properties.Resources.info;
            }
            else
            {
                this.pictureBox1.Image = Properties.Resources.info;
            }
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


    }
}
