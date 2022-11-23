using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IUsersDataServiceSQL
    {
        void AddPassengerDataService(CarpoolPassengersModelData passenger);
        void AddUserDataService(UserBaseModelData user);
        void DeletePassengerAllCarpoolsDataService(int userID);
        void DeletePassengerFromSpecificCarpoolDataService(CarpoolPassengersModelData passenger);
        void DeleteUserDataService(int id);
        void EditUserDataService(UserBaseModelData user);
        List<CarpoolPassengersModelData> ListAllPassengersDataService();
        List<UserBaseModelData> ListAllUsersDataService();
        List<CarpoolPassengersModelData> ListPassengerInACarpoolDataService(int userID, int carpoolID);
        UserBaseModelData ListUserByIdDataService(int id);
    }
}