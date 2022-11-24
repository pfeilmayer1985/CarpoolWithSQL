using System.Reflection.Metadata;
using System.Xml.Linq;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public class CarpoolsBusinessService : ICarpoolsBusinessService
    {
        private ICarpoolsDataServiceSQL _carpoolsDataServiceSQL;
        private IUsersDataServiceSQL _usersDataServiceSQL;
        List<CarpoolsModelData> carpoolsList;
        List<CarpoolPassengersModelData> passengerAllreadyInTheCarpool;
        UserBaseModelData user;
        CarpoolsModelData carpool;

        public CarpoolsBusinessService(ICarpoolsDataServiceSQL carpoolDataServiceSQL, IUsersDataServiceSQL userDataServiceSQL)
        {
            _carpoolsDataServiceSQL = carpoolDataServiceSQL;
            _usersDataServiceSQL = userDataServiceSQL;
        }

        public CarpoolsBusinessService()
        {

        }

        /// <summary>
        /// This method will return the complete list of carpools
        /// </summary>
        public List<CarpoolsModelData> ListAllCarpoolsBusinessService()
        {
            carpoolsList = _carpoolsDataServiceSQL.ListAllCarpoolsDataService();
            return carpoolsList;
        }


        /// <summary>
        /// This method will return one detailed carpool info based on a search after CarpoolID
        /// </summary>
        public CarpoolsModelDto ListOneCarpoolByIdBusinessService(int carpoolID)
        {
            carpool = _carpoolsDataServiceSQL.ListCarpoolByIDDataService(carpoolID);
            user = _usersDataServiceSQL.ListUserByIdDataService(carpool.DriverID);

            //var findCarpool = carpoolsList.First(e => e.CarpoolID.Equals(carpoolID));
            if (carpool != null)
            {
                CarpoolsModelDto newCarpool = new CarpoolsModelDto
                {
                    CarpoolID = carpool.CarpoolID,
                    Driver = ConvertUserToDto(user),
                    TotalSeatsCount = carpool.TotalSeatsCount,
                    Origin = carpool.Origin,
                    Destination = carpool.Destination,
                    DepartureDate = carpool.DepartureDate
                };
                _carpoolsDataServiceSQL.ListCarpoolByIDDataService(carpoolID);
                return newCarpool;
            }
            else
            {
                return null;
            }
        }

        public UserBaseModelDto ConvertUserToDto(UserBaseModelData user)
        {
            return new UserBaseModelDto(user.ID, user.Email, user.PhoneNo, user.FirstName, user.LastName, user.IsDriver);
        }

        /// <summary>
        /// This method will add a new carpool in the Database
        /// </summary>
        public CarpoolsModelData AddCarpoolBusineeService(int userID, CarpoolsModelData carpool)
        {
            user = _usersDataServiceSQL.ListUserByIdDataService(userID);

            CarpoolsModelData newCarpool = new CarpoolsModelData()
            {
                CarpoolID = carpool.CarpoolID,
                DriverID = userID,
                TotalSeatsCount = carpool.TotalSeatsCount,
                Origin = carpool.Origin,
                Destination = carpool.Destination,
                DepartureDate = carpool.DepartureDate
            };
            if (user != null)
            {
                CarpoolPassengersModelData carpoolPassenger = new CarpoolPassengersModelData()
                { Carpool_ID = (int)newCarpool.CarpoolID, User_ID = (int)user.ID };

                if (user.IsDriver == true)
                {
                    _carpoolsDataServiceSQL.AddCarpoolDataService(newCarpool);
                    return newCarpool;
                }

                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will add a user to an allready existing carpool in the Database
        /// </summary>
        public CarpoolPassengersModelData JoinExistingCarpoolBusineeService(CarpoolPassengersModelData toJoin)
        {
            user = _usersDataServiceSQL.ListUserByIdDataService(toJoin.User_ID);
            carpool = _carpoolsDataServiceSQL.ListCarpoolByIDDataService(toJoin.Carpool_ID);
            passengerAllreadyInTheCarpool = _usersDataServiceSQL.ListPassengerInACarpoolDataService(toJoin.User_ID, toJoin.Carpool_ID);
            var seatsCount = (int)carpool.TotalSeatsCount;
            var seatsOccupied = _carpoolsDataServiceSQL.CountPassengersByCarpoolIDDataService(toJoin.Carpool_ID);
            if (user != null && seatsOccupied < seatsCount && passengerAllreadyInTheCarpool == null && user.ID != carpool.DriverID)
            {
                CarpoolPassengersModelData newUserJoin = new CarpoolPassengersModelData()
                {
                    Carpool_ID = toJoin.Carpool_ID,
                    User_ID = toJoin.User_ID,
                };
                _usersDataServiceSQL.AddPassengerDataService(newUserJoin);
                return newUserJoin;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will delete a carpool based on it's ID
        /// </summary>
        public int DeleteCarpoolByCarpoolIDBusinessService(int carpoolID, int userID, string password)
        {
            carpool = _carpoolsDataServiceSQL.ListCarpoolByIDDataService(carpoolID);
            user = _usersDataServiceSQL.ListUserByIdDataService(userID);

            if (carpool != null && user != null && user.Password == password.ToString())
            {
                int carpoolToDelete = (int)carpool.CarpoolID;
                _carpoolsDataServiceSQL.RemoveCarpoolByIDFromPassengerTableDataService(carpoolToDelete);
                _carpoolsDataServiceSQL.DeleteCarpoolByIDDataService(carpoolToDelete);
                return carpoolToDelete;
            }

            else
            {
                return 0;
            }

        }

        /// <summary>
        /// This method will edit/replace the data of a carpool based on it's ID, driver's ID and driver's password
        /// </summary>
        public CarpoolsModelData EditCarpoolBusinessService(int carpoolID, int userID, string password, CarpoolsModelData eCarpool)
        {
            user = _usersDataServiceSQL.ListUserByIdDataService(userID);
            carpool = _carpoolsDataServiceSQL.ListCarpoolByIDDataService(carpoolID);

            if (user != null && user.Password == password && carpool.DriverID == user.ID)
            {

                CarpoolsModelData editedCarpool = new CarpoolsModelData()
                {
                    CarpoolID = carpoolID,
                    DriverID = userID,
                    TotalSeatsCount = eCarpool.TotalSeatsCount,
                    Origin = eCarpool.Origin,
                    Destination = eCarpool.Destination,
                    DepartureDate = eCarpool.DepartureDate,
                };
                _carpoolsDataServiceSQL.EditCarpoolDataService(editedCarpool);

                return editedCarpool;
            }
            else
            {
                return null;
            }
        }
    }
}