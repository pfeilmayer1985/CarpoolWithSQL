using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface ICarpoolsBusinessService
    {
        CarpoolsModelData AddCarpoolBusineeService(int userID, CarpoolsModelData carpool);
        UserBaseModelDto ConvertUserToDto(UserBaseModelData user);
        int DeleteCarpoolByCarpoolIDBusinessService(int carpoolID, int userID, string password);
        CarpoolsModelData EditCarpoolBusinessService(int carpoolID, int userID, string password, CarpoolsModelData eCarpool);
        CarpoolPassengersModelData JoinExistingCarpoolBusineeService(CarpoolPassengersModelData toJoin);
        List<CarpoolsModelData> ListAllCarpoolsBusinessService();
        CarpoolsModelDto ListOneCarpoolByIdBusinessService(int carpoolID);
    }
}