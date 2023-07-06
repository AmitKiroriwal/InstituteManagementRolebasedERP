using InstituteManagement_Models.Subscriptions;

namespace InstituteManagement.Models.Interfaces
{
    public interface ISubscriptionRepo
    {
        public Task<Plans> CreatePlans(Plans plans);
        public Task<Plans> UpdatePlans(Plans plans);
        public Task<Plans> DeletePlans(int id);
        public Task<IEnumerable<Plans>> GetPlans();
        public Task<Plans> GetPlanById(int id);
        public Task<Subscription> SubscriptionById(int id);
        public Task<Subscription> SubscriptionByUserId(string Uid);
        public Task<IEnumerable<Subscription>> GetSubscriptions();
        public Task<Subscription> CreateSubscription(Subscription subscription);
        public Task<Subscription> UpdateSubscription(Subscription subscription);
        public Task<Subscription> DeleteSubscription(int id);
        public Task<IEnumerable<Payment>> GetPayments();
        public Task<Payment> AddPayment(Payment payment);
        public Task<IEnumerable<Subscription>> pendingSubscriptions();
        public Task<Payment> GetPaymentByUserId(string userId);
    }
}
