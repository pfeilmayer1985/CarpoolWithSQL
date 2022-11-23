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
                        carpools.Add(new CarpoolsModelData((int)reader["CarpoolID"], (int)reader["DriverID"], (int)reader["FreeSeats"], reader["Origin"].ToString(), reader["Destination"].ToString(), (DateTime)reader["DepartureDate"]));
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
        public CarpoolsModelData ListCarpoolByIDDataService(int id)
        {

            var carpools = new CarpoolsModelData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Carpools WHERE CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CarpoolID", SqlDbType.VarChar);
                command.Parameters["@CarpoolID"].Value = id;
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
                            (int)reader["FreeSeats"],
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
        public void AddCarpoolWithDriverDataService(CarpoolsModelData carpool)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Carpools(DriverID,FreeSeats,Origin,Destination,DepartureDate) VALUES('{carpool.DriverID}','{carpool.FreeSeatsRemaining}','{carpool.Origin}','{carpool.Destination}','{carpool.DepartureDate}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddCarpoolNODriverDataService(CarpoolsModelData carpool)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Carpools(FreeSeats,Origin,Destination,DepartureDate) VALUES(4,'{carpool.Origin}','{carpool.Destination}','{carpool.DepartureDate}') Select Scope_Identity()";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}