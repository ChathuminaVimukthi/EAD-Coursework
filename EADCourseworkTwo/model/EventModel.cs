using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EADCourseworkTwo.model
{
    class EventModel
    {
        string connectionString;
        SqlConnection sqlConnection;

        public EventModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EADCourseworkTwo.Properties.Settings.AppointmentsConnectionString"].ConnectionString;
        }

        //Add Event to DB
        public Boolean addEvent(Event evnt)
        {
            string queryStringEvent = "INSERT INTO Event (Title, Description, StartingTime, EndingTime, EventFlag, RecurringFlag, Location, UserId, RecurringId) " +
                                     "Values (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9)";
            using (sqlConnection = new SqlConnection(connectionString)) 
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(queryStringEvent, sqlConnection, transaction)
                    {
                        CommandType = CommandType.Text,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@param1", evnt.EventTitle);
                    sqlCommand.Parameters.AddWithValue("@param2", evnt.EventDescription);
                    sqlCommand.Parameters.AddWithValue("@param3", evnt.StartingDateTime);
                    sqlCommand.Parameters.AddWithValue("@param4", evnt.EndingDateTime);
                    sqlCommand.Parameters.AddWithValue("@param5", evnt.EventFlag);
                    sqlCommand.Parameters.AddWithValue("@param6", evnt.RecurringFlag);
                    sqlCommand.Parameters.AddWithValue("@param7", evnt.Location);
                    sqlCommand.Parameters.AddWithValue("@param8", evnt.UserId);
                    sqlCommand.Parameters.AddWithValue("@param9", evnt.RecurringId);

                    int rowsAdded = sqlCommand.ExecuteNonQuery();

                    transaction.Commit();

                    if (rowsAdded > 0)
                    { 
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        //Add ContactsSelcted
        public Boolean addContactsSelected(Event evnt)
        {
            int evId = getEventId();
            
            string queryStringSelectedContacts = "INSERT INTO ContactsSelected (ContactId, EventId) " +
                                     "Values (@param10, @param11)";

            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                try
                {
                    int rowsAdded = 0;
                    foreach(Contact contact in evnt.ContactList)
                    {
                        SqlCommand sqlCommand = new SqlCommand(queryStringSelectedContacts, sqlConnection, transaction)
                        {
                            CommandType = CommandType.Text,
                            Connection = sqlConnection
                        };

                        sqlCommand.Parameters.AddWithValue("@param10", contact.Id);
                        sqlCommand.Parameters.AddWithValue("@param11", evId);

                        rowsAdded += sqlCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    if (rowsAdded > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        //Get event Id
        public int getEventId()
        {
            int eventId = 0;
            string queryString = "SELECT top 1 * FROM Event order by Id Desc";

            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    eventId = Convert.ToInt32(row["Id"]);
                }

                return eventId;
            }

        }

        //Get all events
        public IList<Event> getAllEventDetails(int userId, DateTime startingTime, DateTime endingTime)
        {
            IList<Event> evntList = new List<Event>();
            string queryString = "SELECT * FROM Event WHERE UserId='" + userId + "'";

            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Event evnt = new Event();
                    evnt.EventId = Convert.ToInt32(row["Id"]);
                    evnt.EventTitle = row["Title"].ToString();
                    evnt.EventDescription = row["Description"].ToString();
                    evnt.StartingDateTime = Convert.ToDateTime(row["StartingTime"]);
                    evnt.EndingDateTime = Convert.ToDateTime(row["EndingTime"]);
                    evnt.EventFlag = Convert.ToInt32(row["EventFlag"]);
                    evnt.RecurringFlag = Convert.ToInt32(row["RecurringFlag"]);
                    evnt.Location = row["Location"].ToString();
                    evnt.UserId = Convert.ToInt32(row["UserId"]);
                    evnt.RecurringId = Convert.ToInt32(row["RecurringId"]);
                    evntList.Add(evnt);
                }

                return evntList;
            }
        }

        //Delete Event
        public Boolean deleteEvent(int eventId)
        {
            string queryStringEvent = "DELETE FROM Event WHERE Id = '" + eventId + "'";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(queryStringEvent, sqlConnection, transaction)
                    {
                        CommandType = CommandType.Text,
                        Connection = sqlConnection
                    };

                    int rowsAdded = sqlCommand.ExecuteNonQuery();

                    transaction.Commit();

                    if (rowsAdded > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}
