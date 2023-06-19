using InstituteManagement_Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InstituteManagement.Models.Interfaces
{
    public interface IRegionRepo
    {
        Task<Region> CreateCity(Region model);
        Task<Region> CreateDistrict(Region model);
        Task<Region> Delete(int? id);
        Task<IEnumerable<SelectListItem>> FetchCity(string district);
        Task<IEnumerable<SelectListItem>> FetchDistrict(string state);
        Task<IEnumerable<SelectListItem>> FetchStates();
        Task<bool> IfExistCity(string district, string name);
        Task<bool> IfExistDistrict(string name);
        Task<Region> RegionById(int Id);
        Task<IEnumerable<Region>> Regions();
        Task<Region> Update(Region model);
    }
}
