using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data;
using System.Linq;

namespace TecAlliance.Carpool.Business
{
    public class UserBusinessService : IUserBusinessService
    {
        private IUsersDataServiceSQL _userDataServiceSQL;
        private ICarpoolsDataServiceSQL _carpoolsDataServiceSQL;
        List<UserBaseModelData> userList;
        List<CarpoolsModelData> carpoolsList;
        List<CarpoolPassengersModelData> passengersList;
        UserBaseModelData user;
        CarpoolsModelData carpool;

        public UserBusinessService(IUsersDataServiceSQL userDataServiceSQL, ICarpoolsDataServiceSQL carpoolDataServiceSQL)
        {
            _userDataServiceSQL = userDataServiceSQL;
            _carpoolsDataServiceSQL = carpoolDataServiceSQL;

        }

        /// <summary>
        /// This method will return a detailed list with the Users in the Database
        /// </summary>
        public List<UserBaseModelData> ListAllUserData()
        {
            userList = _userDataServiceSQL.ListAllUsersDataService();
            return userList;
        }

        public UserBaseModelDto ConvertUserToDto(UserBaseModelData user)
        {
            return new UserBaseModelDto(user.ID, user.Email, user.PhoneNo, user.FirstName, user.LastName, user.IsDriver);
        }

        /// <summary>
        /// This method will return one detailed user info based on a search after his IDs
        /// </summary>
        public UserBaseModelDto ListUserDataById(int userID)
        {
            user = _userDataServiceSQL.ListUserByIdDataService(userID);
            //var findUser = userList.FirstOrDefault(e => e.ID.Equals(userID));
            if (user != null)
            {
                return ConvertUserToDto(_userDataServiceSQL.ListUserByIdDataService(userID));
            }

            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will add a new user in the Database
        /// </summary>
        public UserBaseModelData AddUserBusineeService(UserBaseModelData newUserModel)
        {
            userList = _userDataServiceSQL.ListAllUsersDataService();

            UserBaseModelData newUser = new UserBaseModelData()
            {
                ID = newUserModel.ID,
                Email = newUserModel.Email.ToLower(),
                PhoneNo = newUserModel.PhoneNo,
                Password = newUserModel.Password,
                LastName = newUserModel.LastName,
                FirstName = newUserModel.FirstName,
                IsDriver = newUserModel.IsDriver
            };
            _userDataServiceSQL.AddUserDataService(newUser);
            return newUser;

        }

        /// <summary>
        /// This method will edit/replace the data of a user based on his ID
        /// </summary>
        public UserBaseModelData EditUserBusinessService(int userID, string password, UserBaseModelData userM)
        {
            user = _userDataServiceSQL.ListUserByIdDataService(userID);

            if (user != null && user.Password == password)
            {

                UserBaseModelData editedUser = new UserBaseModelData()
                {
                    ID = user.ID,
                    Email = userM.Email.ToLower(),
                    PhoneNo = userM.PhoneNo,
                    Password = userM.Password,
                    LastName = userM.LastName,
                    FirstName = userM.FirstName,
                    IsDriver = userM.IsDriver
                };
                _userDataServiceSQL.EditUserDataService(editedUser);

                return editedUser;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will delete a user based on his ID
        /// </summary>
        public int DeleteUserBusinessService(int userID, string password)
        {
            user = _userDataServiceSQL.ListUserByIdDataService(userID);
            passengersList = _userDataServiceSQL.ListAllPassengersDataService();
            var isUserAlsoPassenger = passengersList.FirstOrDefault(e => e.User_ID.Equals(userID));

            if (user != null && user.Password == password && isUserAlsoPassenger != null)
            {
                int userToDelete = (int)user.ID;
                _userDataServiceSQL.DeletePassengerAllCarpoolsDataService(userToDelete);
                _userDataServiceSQL.DeleteUserDataService(userToDelete);
                return userToDelete;
            }
            else if (user != null && user.Password == password)
            {
                int userToDelete = (int)user.ID;
                _userDataServiceSQL.DeleteUserDataService(userToDelete);
                return userToDelete;
            }
            else
            {
                return 0;
            }

        }

        /*
        /// <summary>
        /// This method will add a new user in the Database
        /// </summary>
        public CarpoolPassengersModelData AddPassengerBusineeService(int carpoolID, int userID)
        {
            user = _userDataServiceSQL.ListUserByIdDataService(userID);
            carpool = _carpoolsDataServiceSQL.ListCarpoolByIDDataService(carpoolID);
            passengersList = _userDataServiceSQL.ListPassengerInACarpoolDataService(userID, carpoolID);
            if (user != null && userID != carpool.DriverID && userID == null)
            {
                CarpoolPassengersModelData newPassenger = new CarpoolPassengersModelData()
                {
                    Carpool_ID = carpoolID,
                    User_ID = userID
                };
                _userDataServiceSQL.AddPassengerDataService(newPassenger);
                return newPassenger;
            }
            else
            {
                return null;
            }
        }
        */

        /// <summary>
        /// This method will delete a passenger from a Carpool (ID) based on his UserID
        /// </summary>
        public CarpoolPassengersModelData DeletePassengerFromCarpoolBusinessService(int carpoolID, int userID, string password)
        {
            passengersList = _userDataServiceSQL.ListAllPassengersDataService();
            carpool = _carpoolsDataServiceSQL.ListCarpoolByIDDataService(carpoolID);

            var identifyPassenger = passengersList.First(p => p.User_ID.Equals(userID));

            if (identifyPassenger != null && user.Password == password && carpool != null)
            {
                CarpoolPassengersModelData passengerToDelete = new CarpoolPassengersModelData
                {
                    Carpool_ID = carpoolID,
                    User_ID = userID
                };

                _userDataServiceSQL.DeletePassengerFromSpecificCarpoolDataService(passengerToDelete);
                return passengerToDelete;
            }
            else
            {
                return null;

            }

        }
    }
}