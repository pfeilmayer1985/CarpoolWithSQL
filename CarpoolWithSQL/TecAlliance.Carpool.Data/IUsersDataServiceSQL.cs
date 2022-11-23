using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IUsersDataServiceSQL
    {
        void AddPassengerDataService(CarpoolPassengersModelData passenger);
        void AddUserDataService(UserBaseModelData user);
        void DeletePassengerFromCarpoolDataService(CarpoolPassengersModelData passenger);
        void DeleteUserDataService(int id);
        void EditUserDataService(UserBaseModelData user);
        List<CarpoolPassengersModelData> ListAllPassengersDataService();
        List<UserBaseModelData> ListAllUsersDataService();
        UserBaseModelData ListUserByIdDataService(int id);
    }
}