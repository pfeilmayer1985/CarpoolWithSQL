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
                        users.Add(new UserBaseModelData((int)reader["UserID"], reader["Email"].ToString(), reader["PhoneNo"].ToString(), reader["Password"].ToString(), reader["FirstName"].ToString(), reader["LastName"].ToString(), (bool)reader["IsDriver"]));
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
        public UserBaseModelData ListUserByIdDataService(int userID)
        {

            var users = new UserBaseModelData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = userID;
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
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
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
                string queryString = $"INSERT INTO Users(Email,PhoneNo,Password,FirstName,LastName,IsDriver) " +
                    $"VALUES(@Email," +
                    $"@PhoneNo," +
                    $"@Password," +
                    $"@FirstName," +
                    $"@LastName," +
                    $"@IsDriver)";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters["@Email"].Value = user.Email.ToLower();
                command.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                command.Parameters["@PhoneNo"].Value = user.PhoneNo;
                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = user.Password;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters["@FirstName"].Value = user.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters["@LastName"].Value = user.LastName;
                command.Parameters.Add("@IsDriver", SqlDbType.Int);
                command.Parameters["@IsDriver"].Value = Convert.ToInt32(user.IsDriver);
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
                    string queryString = $"Update Users SET Email =@Email," +
                        $" PhoneNo = @PhoneNo," +
                        $" Password = @Password," +
                        $" FirstName = @FirstName," +
                        $" LastName = @LastName," +
                        $" IsDriver = @IsDriver" +
                        $" WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@Email", SqlDbType.VarChar);
                    command.Parameters["@Email"].Value = user.Email.ToLower();
                    command.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                    command.Parameters["@PhoneNo"].Value = user.PhoneNo;
                    command.Parameters.Add("@Password", SqlDbType.VarChar);
                    command.Parameters["@Password"].Value = user.Password;
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters["@FirstName"].Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters["@LastName"].Value = user.LastName;
                    command.Parameters.Add("@IsDriver", SqlDbType.Int);
                    command.Parameters["@IsDriver"].Value = Convert.ToInt32(user.IsDriver);
                    command.Parameters.Add("@UserID", SqlDbType.Int);
                    command.Parameters["@UserID"].Value = user.ID;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// This method deletes/removes an existing user from the Users Database
        /// </summary>
        public void DeleteUserDataService(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = userID;
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
                        passengers.Add(new CarpoolPassengersModelData((int)reader["CarpoolID"], (int)reader["PassengerID"]));
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
        /// This method lists all the passengers in the CarpoolPassengers Database
        /// </summary>
        public List<CarpoolPassengersModelData> ListPassengerInACarpoolDataService(int userID, int carpoolID)
        {

            var passengers = new List<CarpoolPassengersModelData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM CarpoolPassengers WHERE PassengerID = @UserID AND CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = userID;
                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;
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

            if (passengers.Count == 0)
            {
                return null;
            }
            else
            {
                return passengers;
            }
        }

        /// <summary>
        /// This method adds a new User to the CarpoolPassengers Database
        /// </summary>
        public void AddPassengerDataService(CarpoolPassengersModelData passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO CarpoolPassengers (CarpoolID,PassengerID) VALUES(@CarpoolID,@PassengerID)";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = passenger.Carpool_ID;
                command.Parameters.Add("@PassengerID", SqlDbType.Int);
                command.Parameters["@PassengerID"].Value = passenger.User_ID;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method deletes an existing passenger from a specific Carpool in the CarpoolPassengers Database
        /// </summary>
        public void DeletePassengerFromSpecificCarpoolDataService(CarpoolPassengersModelData passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM CarpoolPassengers WHERE PassengerID = @CarpoolID AND CarpoolID = @PassengerID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = passenger.Carpool_ID;
                command.Parameters.Add("@PassengerID", SqlDbType.Int);
                command.Parameters["@PassengerID"].Value = passenger.User_ID;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method deletes an existing passenger from all carpools in the CarpoolPassengers Database
        /// </summary>
        public void DeletePassengerAllCarpoolsDataService(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM CarpoolPassengers WHERE PassengerID = @PassengerID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@PassengerID", SqlDbType.Int);
                command.Parameters["@PassengerID"].Value = userID;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
           
    }
}