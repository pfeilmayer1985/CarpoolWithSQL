using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolsDataServiceSQL
    {
        void AddCarpoolDataService(CarpoolsModelData carpool);
        int CountPassengersByCarpoolIDDataService(int carpoolID);
        void DeleteCarpoolByCarpoolIDDataService(int carpoolID);
        void DeleteCarpoolByDriverIDDataService(int driverID);
        void EditCarpoolDataService(CarpoolsModelData carpool);
        List<CarpoolsModelData> ListAllCarpoolsDataService();
        CarpoolsModelData ListCarpoolByIDDataService(int carpoolID);
        void RemoveCarpoolByIDFromPassengerTableDataService(int carpoolID);
    }
}