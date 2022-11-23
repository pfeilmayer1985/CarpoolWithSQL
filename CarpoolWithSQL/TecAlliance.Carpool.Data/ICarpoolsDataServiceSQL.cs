using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolsDataServiceSQL
    {
        void AddCarpoolNODriverDataService(CarpoolsModelData carpool);
        void AddCarpoolWithDriverDataService(CarpoolsModelData carpool);
        List<CarpoolsModelData> ListAllCarpoolsDataService();
        CarpoolsModelData ListCarpoolByIDDataService(int id);
    }
}