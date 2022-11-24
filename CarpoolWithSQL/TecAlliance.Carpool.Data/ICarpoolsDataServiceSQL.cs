using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolsDataServiceSQL
    {
        void AddCarpoolDataService(CarpoolsModelData carpool);
        int CountPassengersByCarpoolIDDataService(int carpoolID);
        void DeleteCarpoolByIDDataService(int carpoolID);
        List<CarpoolsModelData> ListAllCarpoolsDataService();
        CarpoolsModelData ListCarpoolByIDDataService(int carpoolID);
        void RemoveCarpoolByIDFromPassengerTableDataService(int carpoolID);
    }
}