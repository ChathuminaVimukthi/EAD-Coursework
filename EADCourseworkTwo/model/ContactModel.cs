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
    class ContactModel
    {
        string connectionString;
        SqlConnection sqlConnection;

        public ContactModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EADCourseworkTwo.Properties.Settings.AppointmentsConnectionString"].ConnectionString;
        }

        //Add Contact to DB
        public Boolean addContact(Contact contact)
        {
            string queryString = "INSERT INTO Contact (ContactName, ContactNumber, Email, UserId) " +
                                     "Values (@param1, @param2, @param3, @param4)";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection, transaction)
                    {
                        CommandType = CommandType.Text,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@param1", contact.ContactName);
                    sqlCommand.Parameters.AddWithValue("@param2", contact.ContactNumber);
                    sqlCommand.Parameters.AddWithValue("@param3", contact.Email);
                    sqlCommand.Parameters.AddWithValue("@param4", contact.UserId);

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

        //Get contacts
        public Contact getContact(int userId)
        {
            Contact contact = new Contact();
            string queryString = "SELECT * FROM Contact WHERE UserId= '" + userId + "'";
            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id"]);
                    string contactName = row["ContactName"].ToString();
                    int contactNumber = Convert.ToInt32(row["ContactNumber"]);
                    string email = row["Email"].ToString();
                    int user = Convert.ToInt32(row["UserId"]);

                    contact.Id = id;
                    contact.ContactName = contactName;
                    contact.ContactNumber = contactNumber;
                    contact.UserId = userId;

                }
            }
            return contact;
        }
    }
}
