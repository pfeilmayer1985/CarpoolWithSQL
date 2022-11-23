using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface ICarpoolsBusinessService
    {
        CarpoolsModelData AddCarpoolBusineeService(int userID, bool designatedDriver, CarpoolsModelData carpool);
        UserBaseModelDto ConvertUserToDto(UserBaseModelData user);
        List<CarpoolsModelData> ListAllCarpoolsBusinessService();
        CarpoolsModelDto ListOneCarpoolByIdBusinessService(int id);
    }
}