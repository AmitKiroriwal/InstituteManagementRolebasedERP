using InstituteManagement.Models.Interfaces;
using InstituteManagement.Models.ViewModels;
using InstituteManagement_Models.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace InstituteManagement.Controllers
{
    public class InstituteController : Controller
    {
        private readonly ISubscriptionRepo subscriptionRepo;

        public InstituteController(ISubscriptionRepo subscriptionRepo)
        {
            this.subscriptionRepo = subscriptionRepo;
        }

        public async Task<IActionResult> ShowPlans()
        {
           var plans= await subscriptionRepo.GetPlans();
            var planViewModels = plans.Select(p => new PlansViewModel
            {
                Name = p.PlanName,
                Price = p.Price,
                Discount = p.Discount,
                Features= p.Features.Split(',').ToList(),
                SubscribeUrl = Url.Action("ViewPlan", "Subscriptions", new { planId = p.PlanID})
            }).ToList();
            return View(planViewModels);
        }
    }
}
