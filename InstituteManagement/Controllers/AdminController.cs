using InstituteManagement.Models.Interfaces;
using InstituteManagement.Models.ViewModels;
using InstituteManagement_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using InstituteManagement_Models.Subscriptions;

namespace InstituteManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccountConfirm userAccountConfirm;
        private readonly ISubscriptionRepo subscriptionRepo;
        public AdminController(RoleManager<IdentityRole> roleManager,
                               UserManager<ApplicationUser> userManager,
                                IUserAccountConfirm userAccountConfirm,
                                ISubscriptionRepo subscriptionRepo
                               )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userAccountConfirm = userAccountConfirm;
            this.subscriptionRepo= subscriptionRepo;
            
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Admin");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {

                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("PageNotFound");
            }
            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            var user = await userManager.GetUserAsync(User);

           var Userlist = await userManager.Users.Where(w => w.EmailConfirmed == true&& w.AddedBy==user.Id).ToListAsync();

            foreach (var uservm in Userlist)
            {
                if (await userManager.IsInRoleAsync(uservm, role.Name))
                {
                    model.Users.Add(uservm.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {

                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("PageNotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var currentuser = await userManager.GetUserAsync(User);

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("PageNotFound");
            }

            var model = new List<UserRoleViewModel>();
            var usr = await userManager.Users.Where(w => w.AddedBy == currentuser.Id).ToListAsync();

            foreach (var user in usr)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("PageNotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpPost]

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRole");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPendingAccountConfirmation()
        {
            var model = await userAccountConfirm.GetPendingConfirmation();
                  
            return View(model);
        }
       // [HttpPost]
        public async Task<IActionResult> ConfirmAccount(int id)
        {
            var currentuser = await userManager.GetUserAsync(User);
            var result= await userAccountConfirm.ConfirmAccount(id);
            if(result!=null)
            {
                var user = await userManager.FindByIdAsync(result.UserAccountId);
                 var token= await userManager.GenerateEmailConfirmationTokenAsync(user);
                if(token!=null)
                {
                    await userManager.ConfirmEmailAsync(user, token);
                    user.AddedBy = currentuser.Id;
                    user.Status = Status.Active;
                    await userManager.UpdateAsync(user);

                    //Create Initial Free Subscription on account confirmation
                    var sub = new Subscription
                    {
                        UserId = user.Id,
                        Name= "Free Subscription",
                        PlanId = 5,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        IsActive= true,
                        IsPaymentComplete= true,
                        AmountPaid=0,
                        PaymentDate=DateTime.UtcNow,
                        CreationDate=DateTime.UtcNow,
                    };
                    //Add subscription to subscription table
                    await subscriptionRepo.CreateSubscription(sub);

                  SendEmail(user.Email,user.UserName,user.UserPassword, sub.Name);
                }
            } 
            return RedirectToAction("GetPendingAccountConfirmation");
        }

       
        
        public async void  SendEmail(string email ,string username,string password, string subscription)
         {
            try
            {

          
             var smtpClient = new SmtpClient("smtp.gmail.com", 587)
             {
               UseDefaultCredentials = false,
               EnableSsl = true,
               Credentials = new NetworkCredential("ocstech002@gmail.com", "xdbvpqergacmpfly")
             };

              var mailMessage = new MailMessage
              {
               From = new MailAddress("ocstech002@gmail.com"),
               Subject = "Account confirmation ",
               Body = $"Your Username={username}  password={password} Subscription={subscription}"
              };

              mailMessage.To.Add(email);

              smtpClient.Send(mailMessage);
              

            }
            catch (Exception ex)
            { 
                throw ex;
                
            }

        }

   


        public IActionResult ManageAccount()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in await roleManager.Roles.ToListAsync())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }


        
        public async Task<IActionResult> ListUsers()
        {
            //if (userManager != null)

            var user = await userManager.Users.Where(x=>x.EmailConfirmed==true ).ToListAsync();
                if (user == null)
                {
                    return View("Error");
                }
                return View(user);
            
            //return null;
        }

        public async Task<ActionResult> ViewUser(string Id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    Response.StatusCode = 200;
                    return View("Error", Id);
                }
                return View(user);
            }
            catch
            {
                return RedirectToAction("InternalServerError");
            }
        }

        
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                //user.Status = Status.Delete;
                //var result = await userManager.UpdateAsync(user);
                var result= await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }

        public async Task<IActionResult> UserStatus(string Id)
        {
            var user= await userManager.FindByIdAsync(Id);
            if(user!=null)
            {
                if (user.Status == Status.Active)
                {
                    user.Status = Status.Inactive;
                }
                else
                {
                    user.Status = Status.Active;
                }
            }
            await userManager.UpdateAsync(user);
            return RedirectToAction("ListUsers");
        }

    }
}
