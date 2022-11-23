using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolsDataServiceSQL
    {
        void AddCarpoolDataService(CarpoolsModelData carpool);
        int CountPassengersDataService(int carpoolID);
        List<CarpoolsModelData> ListAllCarpoolsDataService();
        CarpoolsModelData ListCarpoolByIDDataService(int carpoolID);
    }
}