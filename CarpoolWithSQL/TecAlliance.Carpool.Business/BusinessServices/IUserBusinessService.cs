using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface IUserBusinessService
    {
        CarpoolPassengersModelData AddPassengerBusineeService(int carpoolID, int userID);
        UserBaseModelData AddUserBusineeService(UserBaseModelData newUserModel);
        UserBaseModelDto ConvertUserToDto(UserBaseModelData user);
        CarpoolPassengersModelData DeletePassengerFromCarpoolBusinessService(int carpoolID, int userID);
        int DeleteUserBusinessService(int id, string password);
        UserBaseModelData EditUserBusinessService(int id, string password, UserBaseModelData user);
        List<UserBaseModelData> ListAllUserData();
        UserBaseModelDto ListUserDataById(int id);
    }
}