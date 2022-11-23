using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class UsersDataServiceSQL : IUsersDataServiceSQL
    {

        string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolDB;Integrated Security=True; TrustServerCertificate=True;";

        /// <summary>
        /// This method lists all the users in the Database
        /// </summary>
        public List<UserBaseModelData> ListAllUsersDataService()
        {

            var users = new List<UserBaseModelData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        users.Add(new UserBaseModelData((int)reader["UserID"], reader["Email"].ToString(), reader["PhoneNo"].ToString(), reader["Password"].ToString(), reader["Name"].ToString(), reader["Vorname"].ToString(), (bool)reader["IsDriver"]));
                    }

                }
                finally
                {
                    reader.Close();
                }
            }
            return users;
        }

        /// <summary>
        /// This method lists one selected user from the Database based on UserID
        /// </summary>
        public UserBaseModelData ListUserByIdDataService(int id)
        {

            var users = new UserBaseModelData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@UserID", SqlDbType.VarChar);
                command.Parameters["@UserID"].Value = id;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        return new UserBaseModelData
                        (
                            (int)reader["UserID"],
                            reader["Email"].ToString().ToLower(),
                            reader["PhoneNo"].ToString(),
                            reader["Password"].ToString(),
                            reader["Vorname"].ToString(),
                            reader["Name"].ToString(),
                            (bool)reader["IsDriver"]
                        );
                    }

                }
                finally
                {
                    reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// This method adds a new User to the Database
        /// </summary>
        public void AddUserDataService(UserBaseModelData user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Users(Email,PhoneNo,Password,Name,Vorname,IsDriver) VALUES('{user.Email.ToLower()}','{user.PhoneNo}','{user.Password}','{user.LastName}','{user.FirstName}',{Convert.ToInt32(user.IsDriver)})";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method replaces saved data with new ones for a defined user in the Database
        /// </summary>
        public void EditUserDataService(UserBaseModelData user)
        {
            if (user != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = $"Update Users SET Email ='{user.Email.ToLower()}'," +
                        $"PhoneNo = '{user.PhoneNo}'," +
                        $"Password = '{user.Password}'," +
                        $"Name = '{user.LastName}'," +
                        $"Vorname = '{user.FirstName}'," +
                        $"IsDriver = {Convert.ToInt32(user.IsDriver)}" +
                        $"WHERE Email = '{user.Email}'";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// This method deletes/removes an existing user from the Users Database
        /// </summary>
        public void DeleteUserDataService(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM Users WHERE UserID = '{id}'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// This method lists all the passengers in the CarpoolPassengers Database
        /// </summary>
        public List<CarpoolPassengersModelData> ListAllPassengersDataService()
        {

            var passengers = new List<CarpoolPassengersModelData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM CarpoolPassengers";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        passengers.Add(new CarpoolPassengersModelData((int)reader["CarpoolID"], (int)reader["DriverID"]));
                    }

                }
                finally
                {
                    reader.Close();
                }
            }
            return passengers;
        }

        /// <summary>
        /// This method adds a new User to the CarpoolPassengers Database
        /// </summary>
        public void AddPassengerDataService(CarpoolPassengersModelData passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO CarpoolPassengers (CarpoolID,PassengerID) VALUES('{passenger.Carpool_ID}','{passenger.User_ID}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method deletes/removes an existing passenger from the CarpoolPassengers Database
        /// </summary>
        public void DeletePassengerFromCarpoolDataService(CarpoolPassengersModelData passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM CarpoolPassengers WHERE PassengerID = '{passenger.User_ID}' AND CarpoolID = '{passenger.Carpool_ID}'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


    }
}