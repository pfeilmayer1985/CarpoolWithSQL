using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class CarpoolsDataServiceSQL : ICarpoolsDataServiceSQL
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolDB;Integrated Security=True; TrustServerCertificate=True;";

        /// <summary>
        /// This method lists all the carpools in the Database
        /// </summary>
        public List<CarpoolsModelData> ListAllCarpoolsDataService()
        {

            var carpools = new List<CarpoolsModelData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Carpools";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        carpools.Add(new CarpoolsModelData((int)reader["CarpoolID"], (int)reader["DriverID"], (int)reader["TotalSeatsCount"], reader["Origin"].ToString(), reader["Destination"].ToString(), (DateTime)reader["DepartureDate"]));
                    }

                }
                finally
                {
                    reader.Close();
                }
            }
            return carpools;
        }

        /// <summary>
        /// This method lists one selected carpool from the Database based on CarpoolID
        /// </summary>
        public CarpoolsModelData ListCarpoolByIDDataService(int carpoolID)
        {

            var carpools = new CarpoolsModelData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Carpools WHERE CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        return new CarpoolsModelData
                        (
                            (int)reader["CarpoolID"],
                            (int)reader["DriverID"],
                            (int)reader["TotalSeatsCount"],
                            reader["Origin"].ToString(),
                            reader["Destination"].ToString(),
                            (DateTime)reader["DepartureDate"]

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
        public void AddCarpoolDataService(CarpoolsModelData carpool)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Carpools(DriverID,TotalSeatsCount,Origin,Destination,DepartureDate) VALUES('{carpool.DriverID}','{carpool.TotalSeatsCount}','{carpool.Origin}','{carpool.Destination}','{carpool.DepartureDate}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method counts the actual number of members in a Carpool
        /// </summary>
        public int CountPassengersByCarpoolIDDataService(int carpoolID)
        {
            var carpools = new CarpoolsModelData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT COUNT(*) as OccupiedSeats FROM CarpoolPassengers WHERE CarpoolID = @CarpoolID;";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.VarChar);
                command.Parameters["@CarpoolID"].Value = carpoolID;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int activePassengers = -1;
                try
                {
                    while (reader.Read())
                    {
                        activePassengers = (int)reader["OccupiedSeats"];
                        return activePassengers;
                    }
                }
                finally
                {
                    reader.Close();
                }
                return activePassengers;
            }

        }

        /// <summary>
        /// This method deletes/removes an existing carpool from the Carpools Database
        /// </summary>
        public void DeleteCarpoolByCarpoolIDDataService(int carpoolID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM Carpools WHERE CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// This method deletes/removes an existing carpool from the Carpools Database based on the driver ID
        /// </summary>
        public void DeleteCarpoolByDriverIDDataService(int driverID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM Carpools WHERE DriverID = @DriverID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@DriverID", SqlDbType.Int);
                command.Parameters["@DriverID"].Value = driverID;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method deletes/removes an existing carpool from the CarpoolPassengers Database
        /// </summary>
        public void RemoveCarpoolByIDFromPassengerTableDataService(int carpoolID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM CarpoolPassengers WHERE CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method replaces saved data with new ones for a defined user in the Database
        /// </summary>
        public void EditCarpoolDataService(CarpoolsModelData carpool)
        {
            if (carpool != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = $"Update Carpools SET TotalSeatsCount = @TotalSeatsCount," +
                        $" Origin = @Origin," +
                        $" Destination = @Destination," +
                        $" DepartureDate = @DepartureDate" +
                        $" WHERE CarpoolID = @CarpoolID";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@TotalSeatsCount", SqlDbType.Int);
                    command.Parameters["@TotalSeatsCount"].Value = carpool.TotalSeatsCount;
                    command.Parameters.Add("@Origin", SqlDbType.VarChar);
                    command.Parameters["@Origin"].Value = carpool.Origin;
                    command.Parameters.Add("@Destination", SqlDbType.VarChar);
                    command.Parameters["@Destination"].Value = carpool.Destination;
                    command.Parameters.Add("@DepartureDate", SqlDbType.VarChar);
                    command.Parameters["@DepartureDate"].Value = carpool.DepartureDate;
                    command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                    command.Parameters["@CarpoolID"].Value = carpool.CarpoolID;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}