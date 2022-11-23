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
        UserBaseModelData user;

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
        public CarpoolsModelDto ListOneCarpoolByIdBusinessService(int id)
        {
            carpoolsList = _carpoolsDataServiceSQL.ListAllCarpoolsDataService();
            user = _usersDataServiceSQL.ListUserByIdDataService(id);

            var findCarpool = carpoolsList.First(e => e.CarpoolID.Equals(id));
            if (findCarpool != null)
            {
                CarpoolsModelDto newCarpool = new CarpoolsModelDto
                {
                    CarpoolID = findCarpool.CarpoolID,
                    Driver = ConvertUserToDto(user),
                    FreeSeatsRemaining = findCarpool.FreeSeatsRemaining,
                    Origin = findCarpool.Origin,
                    Destination = findCarpool.Destination,
                    DepartureDate = findCarpool.DepartureDate
                };
                _carpoolsDataServiceSQL.ListCarpoolByIDDataService(id);
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
        public CarpoolsModelData AddCarpoolBusineeService(int userID, bool designatedDriver, CarpoolsModelData carpool)
        {
            user = _usersDataServiceSQL.ListUserByIdDataService(userID);

            CarpoolsModelData newCarpool = new CarpoolsModelData()
            {
                CarpoolID = carpool.CarpoolID,
                DriverID = userID,
                FreeSeatsRemaining = carpool.FreeSeatsRemaining,
                Origin = carpool.Origin,
                Destination = carpool.Destination,
                DepartureDate = carpool.DepartureDate
            };
            if (user != null)
            {
                CarpoolPassengersModelData carpoolPassenger = new CarpoolPassengersModelData()
                { Carpool_ID = (int)newCarpool.CarpoolID, User_ID = (int)user.ID };

                if (user.IsDriver == true && designatedDriver == true)
                {
                    _carpoolsDataServiceSQL.AddCarpoolWithDriverDataService(newCarpool);
                }

                if (!designatedDriver)
                {
                    _carpoolsDataServiceSQL.AddCarpoolNODriverDataService(newCarpool);
                    
                    //
                    //
                    //newCarpool.CarpoolID
                    //
                    //

                    _usersDataServiceSQL.AddPassengerDataService(carpoolPassenger);
                }

                if (user.IsDriver == false && designatedDriver == true)
                {
                    //_carpoolsDataServiceSQL.AddCarpoolNODriverDataService(newCarpool);
                    //_usersDataServiceSQL.AddPassengerDataService(carpoolPassenger);
                    return null;

                }
                return newCarpool;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will add a user to an allready existing carpool in the Database
        /// </summary>
        public CarpoolsModelData JoinExistingCarpoolBusineeService(int userID, int carpoolID)
        {
            user = _usersDataServiceSQL.ListUserByIdDataService(userID);
//
            
        }
    }
}