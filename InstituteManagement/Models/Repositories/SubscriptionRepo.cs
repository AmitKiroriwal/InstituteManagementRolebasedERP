using InstituteManagement.Models.Interfaces;
using InstituteManagement_Models.Context;
using InstituteManagement_Models.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace InstituteManagement.Models.Repositories
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private readonly AppDbContext _context;

        public SubscriptionRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Plans> CreatePlans(Plans plans)
        {
            await _context.Plans.AddAsync(plans);
            await  _context.SaveChangesAsync();
            return plans;
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<Plans> DeletePlans(int id)
        {
            var model= await _context.Plans.FirstOrDefaultAsync(x=>x.PlanID==id);
            if(model!=null)
            {
                _context.Plans.Remove(model);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<Subscription> DeleteSubscription(int id)
        {
            var model = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            if (model != null)
            {
                _context.Subscriptions.Remove(model);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<Plans> GetPlanById(int id)
        {
            var model=await _context.Plans.FirstOrDefaultAsync(p=>p.PlanID==id);
            return model;
        }

        public async Task<IEnumerable <Plans>> GetPlans()
        {
            var model= await _context.Plans.Where(p=>p.IsActive).ToListAsync();
            return model;
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await _context.Subscriptions.Include(s=>s.Plans).Include(x=>x.ApplicationUser).Where(a=>a.IsActive).ToListAsync();
        }

        public async Task<Subscription> SubscriptionById(int id)
        {
            var model= await _context.Subscriptions.Include(s=>s.Plans).Include(u=>u.ApplicationUser).FirstOrDefaultAsync(s=>s.Id==id);
            return model;
        }
        public async Task<Subscription> SubscriptionByUserId(string Uid)
        {
            var model = await _context.Subscriptions.FirstOrDefaultAsync(s =>s.UserId==Uid );
            return model;
        }

        public async Task<Plans> UpdatePlans(Plans plans)
        {
            var model = await _context.Plans.FindAsync(plans.PlanID);
            if (model != null)
            {
                model.PlanName = plans.PlanName;
                model.PlanDescription = plans.PlanDescription;
                model.Price = plans.Price;
                model.Duration = plans.Duration;
                model.IsActive = plans.IsActive;
                model.Features = plans.Features;
                model.Discount = plans.Discount;
            }
            await _context.SaveChangesAsync();       
            return model;
        }

        public async Task<Subscription> UpdateSubscription(Subscription subscription)
        {
            var model = await _context.Subscriptions.FindAsync(subscription.Id);
            if (model != null)
            {
                model.Name = subscription.Name;
                model.StartDate = subscription.StartDate;
                model.ApplicationUser = subscription.ApplicationUser;
                model.Plans = subscription.Plans;
                model.EndDate = subscription.EndDate;
                model.PaymentDate = subscription.PaymentDate;
                model.AmountPaid = subscription.AmountPaid;
                model.PlanId = subscription.PlanId;
                model.IsPaymentComplete = subscription.IsPaymentComplete;
                model.UserId = subscription.UserId;  
                
                
            }
            await _context.SaveChangesAsync();
            return subscription;
        }
        public async Task<Payment> AddPayment(Payment payment)
        {

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPayment()
        {
            return await _context.Payments.ToListAsync();     
        }
    }
}
