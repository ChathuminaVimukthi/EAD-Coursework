using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                        XmlTextWriter xwriter = new XmlTextWriter(Environment.CurrentDirectory + "\\contactDetails.xml", Encoding.Unicode);
                        xwriter.WriteStartDocument();
                        xwriter.WriteStartElement("Contact");
                        xwriter.WriteStartElement("Name");
                        xwriter.WriteString(contact.ContactName);
                        xwriter.WriteEndElement();
                        xwriter.WriteStartElement("Number");
                        xwriter.WriteString(contact.ContactNumber.ToString());
                        xwriter.WriteEndElement();
                        xwriter.WriteStartElement("Email");
                        xwriter.WriteString(contact.Email);
                        xwriter.WriteEndElement();
                        xwriter.WriteStartElement("UserId");
                        xwriter.WriteString(contact.UserId.ToString());
                        xwriter.WriteEndElement();
                        xwriter.WriteEndElement();
                        xwriter.WriteEndDocument();
                        xwriter.Close();

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

        //Get all contacts
        public IList<Contact> getContact(int userId)
        {
            IList<Contact> contactList = new List<Contact>();
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

                    Contact contact = new Contact();
                    contact.Id = id;
                    contact.ContactName = contactName;
                    contact.ContactNumber = contactNumber;
                    contact.Email = email;
                    contact.UserId = userId;
                    contactList.Add(contact);

                }
            }
            return contactList;
        }

        //Get contact using contact name
        public Contact getContactUsingContactName(string contactName)
        {
            Contact contact = new Contact();
            string queryString = "SELECT * FROM Contact WHERE ContactName= '" + contactName + "'";
            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    contact.Id = Convert.ToInt32(row["Id"]);
                    contact.ContactName = row["ContactName"].ToString();
                    contact.ContactNumber = Convert.ToInt32(row["ContactNumber"]);
                    contact.Email = row["Email"].ToString();
                    contact.UserId = Convert.ToInt32(row["UserId"]);
                }

                return contact;
            }
        }

        //Get contact using Id
        public Contact getContactUsingContactId(int contacId)
        {
            Contact contact = new Contact();
            string queryString = "SELECT * FROM Contact WHERE Id= '" + contacId + "'";
            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    contact.Id = Convert.ToInt32(row["Id"]);
                    contact.ContactName = row["ContactName"].ToString();
                    contact.ContactNumber = Convert.ToInt32(row["ContactNumber"]);
                    contact.Email = row["Email"].ToString();
                    contact.UserId = Convert.ToInt32(row["UserId"]);
                }

                return contact;
            }
        }

        //Update contact details
        public Boolean updateContact(Contact contact)
        {
            string queryStringEvent = "UPDATE Contact SET ContactName = @param1, ContactNumber = @param2, Email = @param3 Where Id = @param4";
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

                    sqlCommand.Parameters.AddWithValue("@param1", contact.ContactName);
                    sqlCommand.Parameters.AddWithValue("@param2", contact.ContactNumber);
                    sqlCommand.Parameters.AddWithValue("@param3", contact.Email);
                    sqlCommand.Parameters.AddWithValue("@param4", contact.Id);

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

        //Delete contact
        public Boolean deleteContact(int contactId,int userId)
        {
            string queryStringEvent = "DELETE FROM Contact WHERE Id = '" + contactId + "' AND UserId = '"+userId+"'";
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
