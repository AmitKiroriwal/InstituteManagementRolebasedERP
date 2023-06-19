using InstituteManagement.Models.Interfaces;
using InstituteManagement_Models;
using InstituteManagement_Models.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace InstituteManagement.Models.Repositories
{
    public class AccountRepo:IAccountRepo
    {
        private readonly AppDbContext dbContext;

        public AccountRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Region> RegionById(int Id)
        {
            return await dbContext.RegionList.Where(w => w.Id == Id).FirstOrDefaultAsync();
        }
        //Get State
        public async Task<IEnumerable<SelectListItem>> FetchState()
        {
            var data = dbContext.RegionList.Select(s => new { State = s.State }).Distinct().OrderBy(d => d.State);
            var res = await data.Select(x => new SelectListItem { Text = x.State, Value = x.State }).ToListAsync();
            return res;
        }

        // Get district by state
        public async Task<IEnumerable<SelectListItem>> FetchDistrict(string state)
        {
            var data = dbContext.RegionList.Where(x => x.State == state).Select(s => new { District = s.District }).Distinct().OrderBy(s => s.District);
            var res = await data.Select(x => new SelectListItem { Text = x.District, Value = x.District }).ToListAsync();
            return res;
        }

        // Get cities by district
        public async Task<IEnumerable<SelectListItem>> FetchCity(string district)
        {
            var data = dbContext.RegionList.Where(x => x.District == district).Select(s => new { City = s.City }).Distinct().OrderBy(c => c.City);
            var res = await data.Select(x => new SelectListItem { Text = x.City, Value = x.City }).ToListAsync();
            return res;

        }

    }
}
