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
        int DeleteUserBusinessService(int userID, string password);
        UserBaseModelData EditUserBusinessService(int userID, string password, UserBaseModelData userM);
        List<UserBaseModelData> ListAllUserData();
        UserBaseModelDto ListUserDataById(int userID);
    }
}