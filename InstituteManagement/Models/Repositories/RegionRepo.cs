using InstituteManagement_Models.Context;
using InstituteManagement_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstituteManagement.Models.Interfaces;

namespace InstituteManagement.Models.Repositories
{
    public class RegionRepo:IRegionRepo
    {
        //    Dependency Injection for DB Context

        public readonly AppDbContext db;

        // Constructor 
        public RegionRepo(AppDbContext db)
        {
            this.db = db;
        }


        ///     
        public async Task<IEnumerable<Region>> Regions()
        {
            return await db.RegionList.ToListAsync();
        }

        public async Task<Region> RegionById(int Id)
        {
            return await db.RegionList.Where(w => w.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SelectListItem>> FetchStates()
        {
            var data = db.RegionList.Select(s => new { State = s.State }).Distinct().OrderBy(d => d.State);
            var res = await data.Select(x => new SelectListItem { Text = x.State, Value = x.State }).ToListAsync();
            return res;
        }
        public async Task<IEnumerable<SelectListItem>> FetchDistrict(string state)
        {
            var data = db.RegionList.Where(x => x.State == state).Select(s => new { District = s.District }).Distinct().OrderBy(s => s.District);
            var res = await data.Select(x => new SelectListItem { Text = x.District, Value = x.District }).ToListAsync();
            return res;
        }
        public async Task<bool> IfExistDistrict(string name)
        {
            var data = await db.RegionList.Where(x => x.District.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (data != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IfExistCity(string district, string name)
        {
            var data = await db.RegionList.Where(x => x.District.ToLower() == district.ToLower() && x.City.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        // Get cities by district
        public async Task<IEnumerable<SelectListItem>> FetchCity(string district)
        {
            var data = db.RegionList.Where(x => x.District == district).Select(s => new { City = s.City }).Distinct().OrderBy(c => c.City);
            var res = await data.Select(x => new SelectListItem { Text = x.City, Value = x.City }).ToListAsync();
            return res;
        }
        public async Task<Region> CreateDistrict(Region model)
        {
            await db.RegionList.AddAsync(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Region> CreateCity(Region model)
        {
            await db.RegionList.AddAsync(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Region> Update(Region model)
        {
            var attach = db.RegionList.Attach(model);
            attach.State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        // Delete dealer
        public async Task<Region> Delete(int? id)
        {
            var result = await db.RegionList.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                db.RegionList.Remove(result);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
