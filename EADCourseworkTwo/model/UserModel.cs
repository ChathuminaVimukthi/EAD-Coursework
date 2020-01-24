using EADCourseworkTwo.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EADCourseworkTwo
{
    class UserModel
    {
        string connectionString;
        SqlConnection sqlConnection;

        public UserModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EADCourseworkTwo.Properties.Settings.AppointmentsConnectionString"].ConnectionString;

        }

        //Register user method
        public Boolean addUser(User user)
        {
            string queryStringEvent = "INSERT INTO Users (UserName, Password, FirstName, LastName, Email) " +
                                     "Values (@param1, @param2, @param3, @param4, @param5)";

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

                    sqlCommand.Parameters.AddWithValue("@param1", user.UserName);
                    sqlCommand.Parameters.AddWithValue("@param2", user.Password);
                    sqlCommand.Parameters.AddWithValue("@param3", user.FirstName);
                    sqlCommand.Parameters.AddWithValue("@param4", user.LastName);
                    sqlCommand.Parameters.AddWithValue("@param5", user.Email);

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

        //Validate user for Login
        public Boolean validateUser(string userName, string password)
        {
            string queryString = "SELECT * FROM Users WHERE UserName = '" + userName + "'";
            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString,sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach(DataRow row in dataTable.Rows)
                {
                    string pass = row["Password"].ToString();
                    if (PasswordHandler.Validate(password,pass))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false; 
        }

        public User getUser(string userName)
        {
            User user = new User();
            string queryString = "SELECT * FROM Users WHERE UserName = '" + userName + "'";
            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id"]);
                    string username = row["UserName"].ToString();
                    string pass = row["Password"].ToString();
                    string email = row["Email"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string lastName = row["LastName"].ToString();

                    user.UserId = id;
                    user.UserName = username;
                    user.Email = email;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Password = pass;
                    
                }
            }
            return user;
        }

        public List<User> getAllUsers()
        {
            List<User> userList = new List<User>();
            string queryString = "SELECT * FROM Users";
            using (sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    User user = new User();
                    int id = Convert.ToInt32(row["Id"]);
                    string username = row["UserName"].ToString();
                    string pass = row["Password"].ToString();
                    string email = row["Email"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string lastName = row["LastName"].ToString();

                    user.UserId = id;
                    user.UserName = username;
                    user.Email = email;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Password = pass;
                    userList.Add(user);
                }
            }
            return userList;
        }
    }
}
