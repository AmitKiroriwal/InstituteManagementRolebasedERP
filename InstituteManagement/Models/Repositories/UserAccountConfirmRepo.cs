using InstituteManagement.Models.Interfaces;
using InstituteManagement_Models;
using InstituteManagement_Models.Context;
using Microsoft.EntityFrameworkCore;

namespace InstituteManagement.Models.Repositories
{
    public class UserAccountConfirmRepo : IUserAccountConfirm
    {
        private readonly AppDbContext dbContext;

        public UserAccountConfirmRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserAccountConfirmations> AddPendingConfirmation(UserAccountConfirmations user)
        {
            
              dbContext.UserAccountConfirmations.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserAccountConfirmations> ConfirmAccount(int Id)
        {
            var user= await dbContext.UserAccountConfirmations.FirstOrDefaultAsync(x=>x.id==Id);
            if (user != null)
            {



                user.UserAccountId = user.UserAccountId;
                   user.EmailConfirmation = true;
                user.IsConfirmed = true;

             }               
                //var data= dbContext.UserAccountConfirmations.Attach(userAccountConfirmations);
                //data.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbContext.SaveChangesAsync();

            return user;
            
            
        }

        public async Task<IEnumerable<UserAccountConfirmations>> GetConfirmedAccount()
        {
            var model = await dbContext.UserAccountConfirmations.Where(x => x.IsConfirmed).ToListAsync();
            return model;
        }

        public async Task<IEnumerable<UserAccountConfirmations>> GetPendingConfirmation()
        {
            var model = await dbContext.UserAccountConfirmations.Include(e=>e.ApplicationUser).Where(x => x.IsConfirmed==false).ToListAsync();
            
            return model;
        }
    }
}
