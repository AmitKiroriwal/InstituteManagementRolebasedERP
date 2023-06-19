using Microsoft.AspNetCore.Mvc.Rendering;

namespace InstituteManagement.Models.Interfaces
{
    public interface IAccountRepo
    {
        Task<IEnumerable<SelectListItem>> FetchCity(string district);
        Task<IEnumerable<SelectListItem>> FetchDistrict(string state);
        Task<IEnumerable<SelectListItem>> FetchState();
    }
}
