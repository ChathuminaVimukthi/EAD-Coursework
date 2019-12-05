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
        public void addUser(User user)
        {

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
                    if(pass.Equals(password))
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

        //Get all the upcoming Events' data
        public void getEventData()
        {

        }
    }

    

}
