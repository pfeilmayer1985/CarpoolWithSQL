using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface ICarpoolsBusinessService
    {
        CarpoolsModelData AddCarpoolBusineeService(int userID, CarpoolsModelData carpool);
        UserBaseModelDto ConvertUserToDto(UserBaseModelData user);
        CarpoolPassengersModelData JoinExistingCarpoolBusineeService(CarpoolPassengersModelData whereToJoinUserAndCarpool);
        List<CarpoolsModelData> ListAllCarpoolsBusinessService();
        CarpoolsModelDto ListOneCarpoolByIdBusinessService(int carpoolID);
    }
}