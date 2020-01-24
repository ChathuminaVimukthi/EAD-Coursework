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
            this.tableLayoutPanel3.CellPaint += tableLayoutPane2_CellPaint;
            generateReport();
        }

        private void tableLayoutPane2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
        }

        private void generateReport()
        {
            //get the current time
            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString("HH:mm");
            string date = dateTime.ToString("yyyy-MM-dd");
            DateTime currentDateTime = DateTime.Parse(date+" "+time);

            EventModel eventModel = new EventModel();
            IList<Event> pastEventList = new List<Event>();//past events list
            IList<Event> allevents = eventModel.getAllEventDetails(loggedInUser.UserId);//all events list
            foreach(Event evnt in allevents)
            {
                if(evnt.StartingDateTime < currentDateTime && evnt.EndingDateTime < currentDateTime)
                {
                    pastEventList.Add(evnt);
                    Console.WriteLine(evnt.EventId + "");
                }
            }

            //get total number of past events
            totalNumOfEvents = pastEventList.Count;

            foreach(Event evnt in pastEventList)
            {
                int difference = (int)evnt.EndingDateTime.Subtract(evnt.StartingDateTime).TotalMinutes;
                totalTimeUsageForEvents += difference;//calculating the total time usage for all past events
            }

            //calculating the average time usage per event
            averageTimeUsagePerEvent = totalTimeUsageForEvents / totalNumOfEvents;

            //calculating the number of weeks from first event's date upto current date
            totalNumberOfweeks = (currentDateTime - pastEventList.FirstOrDefault().StartingDateTime).TotalDays / 7;

            //calculating the number of events perweek
            numberOfEventsPerWeek = totalNumOfEvents / totalNumberOfweeks;

            //calculating the predicted time usage per week
            timeUsagePerWeek = numberOfEventsPerWeek * averageTimeUsagePerEvent;

            //calculating the predicted time usage for upcoming month
            timeUsagePerMonth = timeUsagePerWeek * 4;

            totalHours = (int)timeUsagePerMonth / 60;
            totalMinutes = (int)timeUsagePerMonth % 60;

            this.totalNumberOfEventsLbl.Text = totalNumOfEvents.ToString()+" Events";
            this.totalTimePastEvntLbl.Text = (Convert.ToInt32(totalTimeUsageForEvents) / 60)+" Hours and "+ (Convert.ToInt32(totalTimeUsageForEvents) % 60) +" Minutes";
            this.timeUsagePerEventLbl.Text = (Convert.ToInt32(averageTimeUsagePerEvent) / 60) + " Hours and " + (Convert.ToInt32(averageTimeUsagePerEvent) % 60) + " Minutes";
            this.timeUsagePerWeekLbl.Text = (Convert.ToInt32(timeUsagePerWeek) / 60) + " Hours and " + (Convert.ToInt32(timeUsagePerWeek) % 60) + " Minutes";
            this.predictionLabel.Text = totalHours + " Hours and " + totalMinutes + " Minutes";
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(loggedInUser);
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private async void generateReportBtn_ClickAsync(object sender, EventArgs e)
        {
            SaveFileDialog locate = new SaveFileDialog();

            locate.Filter = "txt files (*.txt)|*.txt|dat files (*.dat)|*.dat";
            locate.FilterIndex = 2;
            locate.RestoreDirectory = true;

            if (locate.ShowDialog() == DialogResult.OK)
            {

                Console.WriteLine("Start");
                var time = await Task.Run(() => this.writeFile(locate.FileName));
                MessageBox.Show("Successfully Genarate File");

            }
        }

        private int writeFile(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            try
            {
                sw.WriteLine("Today is : " + DateTime.Now + "\n" + "Name :" + loggedInUser.UserName + "\n\n");
                sw.WriteLine("--------------PREDICTION RESULT-------------");
                sw.WriteLine("\n");
                sw.WriteLine("Number Of Past Events = " + Convert.ToString(totalNumOfEvents));
                sw.WriteLine("\n");
                sw.WriteLine("Average time usage per Event = " + Convert.ToString(Convert.ToInt32(averageTimeUsagePerEvent) / 60) + "Hours and " + Convert.ToString(Convert.ToInt32(averageTimeUsagePerEvent) % 60) + "  minutes");
                sw.WriteLine("\n");
                sw.WriteLine("Average Events per Week = " + Convert.ToString(numberOfEventsPerWeek) + "  Events/Week");
                sw.WriteLine("\n");
                sw.WriteLine("Time Usage Per Week = " + Convert.ToString(Convert.ToInt32(timeUsagePerWeek) / 60) + "Hours and" + Convert.ToString(Convert.ToInt32(timeUsagePerWeek) % 60) + " minutes");
                sw.WriteLine("\n");
                sw.WriteLine("Predicted time usage per Month = " + totalHours + ":" + totalMinutes + "  Hours/Month");
                sw.WriteLine("\n");

            }
            catch (Exception ex)
            {
                MessageBox.Show("File Write Error");
            }
            finally { sw.Close(); }
            return 1;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
