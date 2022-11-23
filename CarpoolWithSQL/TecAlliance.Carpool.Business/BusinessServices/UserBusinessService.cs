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
        public UserBaseModelDto ListUserDataById(int id)
        {
            userList = _userDataServiceSQL.ListAllUsersDataService();
            var findUser = userList.FirstOrDefault(e => e.ID.Equals(id));
            if (findUser != null)
            {
                return ConvertUserToDto(_userDataServiceSQL.ListUserByIdDataService(id));
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
        public UserBaseModelData EditUserBusinessService(int id, string password, UserBaseModelData user)
        {
            userList = _userDataServiceSQL.ListAllUsersDataService();

            var findUser = userList.FirstOrDefault(e => e.ID.Equals(id));

            if (findUser != null && findUser.Password == password)
            {

                UserBaseModelData editedUser = new UserBaseModelData()
                {
                    Email = user.Email.ToLower(),
                    PhoneNo = user.PhoneNo,
                    Password = user.Password,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    IsDriver = user.IsDriver
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
        public int DeleteUserBusinessService(int id, string password)
        {
            userList = _userDataServiceSQL.ListAllUsersDataService();

            var findUser = userList.FirstOrDefault(e => e.ID.Equals(id));

            if (findUser != null && findUser.Password == password)
            {
                int userToDelete = (int)findUser.ID;
                _userDataServiceSQL.DeleteUserDataService(userToDelete);
                return userToDelete;
            }
            else
            {
                //return null;
                return 0;
            }

        }


        /// <summary>
        /// This method will add a new user in the Database
        /// </summary>
        public CarpoolPassengersModelData AddPassengerBusineeService(int carpoolID, int userID)
        {

            CarpoolPassengersModelData newPassenger = new CarpoolPassengersModelData()
            {
                Carpool_ID = carpoolID,
                User_ID = userID
            };
            _userDataServiceSQL.AddPassengerDataService(newPassenger);

            return newPassenger;
        }


        /// <summary>
        /// This method will delete a passenger from a Carpool (ID) based on his UserID
        /// </summary>
        public CarpoolPassengersModelData DeletePassengerFromCarpoolBusinessService(int carpoolID, int userID)
        {
            passengersList = _userDataServiceSQL.ListAllPassengersDataService();
            carpoolsList = _carpoolsDataServiceSQL.ListAllCarpoolsDataService();

            var identifyPassenger = passengersList.First(p => p.User_ID.Equals(userID));
            var identifyCarpool = carpoolsList.First(c => c.CarpoolID.Equals(carpoolID));

            if (identifyPassenger != null && identifyCarpool != null)
            {
                CarpoolPassengersModelData passengerToDelete = new CarpoolPassengersModelData
                {
                    Carpool_ID = carpoolID,
                    User_ID = userID
                };

                _userDataServiceSQL.DeletePassengerFromCarpoolDataService(passengerToDelete);
                return passengerToDelete;
            }
            else
            {
                return null;

            }

        }
    }
}