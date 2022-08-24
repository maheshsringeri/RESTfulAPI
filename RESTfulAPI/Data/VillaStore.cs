using RESTfulAPI.Models;
using RESTfulAPI.Models.Dto;

namespace RESTfulAPI.Data
{
    public static class VillaStore
    {

        public static List<VillaDTO> villaList = new List<VillaDTO> {
                new VillaDTO{Id=1,Name="Pool View",Sqft=400,Occupancy=3 },
                new VillaDTO{Id=2,Name="Beach View",Sqft=500,Occupancy=4}
            };
    }
}
