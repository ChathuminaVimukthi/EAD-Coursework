using EADCourseworkTwo.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EADCourseworkTwo
{
    public partial class ReportForm : Form
    {
        User loggedInUser;

        double totalNumOfEvents = 0;
        double totalTimeUsageForEvents = 0;
        double averageTimeUsagePerEvent = 0;
        double totalNumberOfweeks = 0;
        double numberOfEventsPerWeek = 0;
        double timeUsagePerWeek = 0;
        double timeUsagePerMonth = 0;

        int totalHours = 0;
        int totalMinutes = 0;

        public ReportForm(User user)
        {
            InitializeComponent();
            loggedInUser = user;
            generateReport();
        }

        private void generateReport()
        {

            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString("HH:mm");
            string date = dateTime.ToString("yyyy-MM-dd");
            DateTime currentDateTime = DateTime.Parse(date+" "+time);

            EventModel eventModel = new EventModel();
            IList<Event> pastEventList = new List<Event>();
            IList<Event> allevents = eventModel.getAllEventDetails(loggedInUser.UserId);
            foreach(Event evnt in allevents)
            {
                if(evnt.StartingDateTime < currentDateTime && evnt.EndingDateTime < currentDateTime)
                {
                    pastEventList.Add(evnt);
                    Console.WriteLine(evnt.EventId + "");
                }
            }

            totalNumOfEvents = pastEventList.Count;

            foreach(Event evnt in pastEventList)
            {
                int difference = (int)evnt.EndingDateTime.Subtract(evnt.StartingDateTime).TotalMinutes;
                totalTimeUsageForEvents += difference;
            }

            averageTimeUsagePerEvent = totalTimeUsageForEvents / totalNumOfEvents;

            totalNumberOfweeks = (currentDateTime - pastEventList.FirstOrDefault().StartingDateTime).TotalDays / 7;

            numberOfEventsPerWeek = totalNumOfEvents / totalNumberOfweeks;

            timeUsagePerWeek = numberOfEventsPerWeek * averageTimeUsagePerEvent;

            timeUsagePerMonth = timeUsagePerWeek * 4;

            totalHours = (int)timeUsagePerMonth / 60;
            totalMinutes = (int)timeUsagePerMonth % 60;

            this.totalNumberOfEventsLbl.Text = totalNumOfEvents.ToString();
            this.totalTimePastEvntLbl.Text = (Convert.ToInt32(totalTimeUsageForEvents) / 60)+" Hours and "+ (Convert.ToInt32(totalTimeUsageForEvents) % 60) +" Minutes";
            this.timeUsagePerEventLbl.Text = (Convert.ToInt32(averageTimeUsagePerEvent) / 60) + " Hours and " + (Convert.ToInt32(averageTimeUsagePerEvent) % 60) + " Minutes";
            this.timeUsagePerWeekLbl.Text = (Convert.ToInt32(timeUsagePerWeek) / 60) + " Hours and " + (Convert.ToInt32(timeUsagePerWeek) % 60) + " Minutes";
            this.predictionLabel.Text = totalHours + " Hours and " + totalMinutes + " Minutes";
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedInUser);
            this.Hide();
            homeForm.Show();
            this.Close();
        }

        private async void generateReportBtn_ClickAsync(object sender, EventArgs e)
        {
            var time = await Task.Run(() => this.writeFile());
            MessageBox.Show("File Generated Successfully");
        }

        private int writeFile()
        {
            StreamWriter sw = new StreamWriter("ImpresarioReport.dat", false, Encoding.UTF8);
            try
            {
                sw.WriteLine("Today is : " + DateTime.Now + "\n" + "Name :" + loggedInUser.UserName + "\n\n");
                sw.WriteLine("--------------PREDICTION RESULT-------------");

                sw.WriteLine("\n\n");
                sw.WriteLine("\n");
                sw.WriteLine("Number Of Past Events     |  " + Convert.ToString(totalNumOfEvents));
                sw.WriteLine("\n");
                sw.WriteLine("\n");
                sw.WriteLine("Average time usage per Event   |  " + Convert.ToString(Convert.ToInt32(averageTimeUsagePerEvent) / 60) + ":" + Convert.ToString(Convert.ToInt32(averageTimeUsagePerEvent) % 60) + "  Hours/event");
                sw.WriteLine("\n");
                sw.WriteLine("\n");
                sw.WriteLine("Average Events per Week   |  " + Convert.ToString(numberOfEventsPerWeek) + "  Events/Week");
                sw.WriteLine("\n");
                sw.WriteLine("\n");
                sw.WriteLine("Time Usage Per Week   |  " + Convert.ToString(Convert.ToInt32(timeUsagePerWeek) / 60) + ":" + Convert.ToString(Convert.ToInt32(timeUsagePerWeek) % 60) + "  Hours/Week");
                sw.WriteLine("\n");
                sw.WriteLine("\n");
                sw.WriteLine("***Average Hours per Month   |  " + totalHours + ":" + totalMinutes + "  Hours/Month");
                sw.WriteLine("\n");

            }
            catch (Exception ex)
            {
                MessageBox.Show("File Write Error");
            }
            finally { sw.Close(); }
            return 1;
        }
    }
}
