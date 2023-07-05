using InstituteManagement.Models.Interfaces;
using InstituteManagement.Models.ViewModels;
using InstituteManagement_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text;
using InstituteManagement_Models.Migrations;
using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Identity.UI.Services;

namespace InstituteManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;
       // private readonly IEmailSender emailSender;
        private readonly RoleManager<IdentityRole> roleManager;
        //private readonly IAccountRepo accountRepo;
        private readonly IUserAccountConfirm userAccountConfirm;
        public AccountController(UserManager<ApplicationUser> userManager, 
                                  SignInManager<ApplicationUser> signInManager, 
                                  IWebHostEnvironment hostingEnvironment,
                                  //IEmailSender emailSender,
                                  ILogger<AccountController> logger,
                                  RoleManager<IdentityRole> roleManager,
                                  IUserAccountConfirm userAccountConfirm)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            _logger= logger;
            this.roleManager = roleManager;
            this.userAccountConfirm= userAccountConfirm;
            //this.accountRepo= accountRepo;
            //this.emailSender= emailSender;

        }

        [HttpGet]
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //returnUrl ??= Url.Content("~/Home/Dashboard");
            if (ModelState.IsValid)
            {

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await userManager.GetUserAsync(User);
                    
                        return RedirectToAction("Index", "Home");
                    
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        [Authorize(Roles ="Admin,Institute")]
        public async Task<IActionResult> Register(string itemid)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "https://regionapi.sircltech.com/";
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("getstates");
                if (Res.IsSuccessStatusCode)
                {
                    var states = Res.Content.ReadAsStringAsync().Result;
                    List<string> stateslist = JsonConvert.DeserializeObject<List<string>>(states);
                    ViewBag.States = stateslist.Select(s => new SelectListItem { Text = s, Value = s }).OrderBy(s => s.Text).ToList();

                }


            }
            //fetch userby id
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == itemid);
            if (user != null)
            {
                RegisterViewModel model = new RegisterViewModel()
                {

                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    ContactNo_1 = user.ContactNo_1,
                    ContactNo_2 = user.ContactNo_2,
                    AddedBy = user.AddedBy,
                    UANNo = user.UANNo,
                    UpdatedBy = user.UpdatedBy,
                    UpdatedOn = user.UpdatedOn,
                    UserName = user.UserName,
                    BankAccName = user.BankAccName,
                    PanNo = user.PanNo,
                    ParentId = user.ParentId,
                    IfscCode = user.IfscCode,
                    InstituteName = user.InstituteName,
                    AddedOn = user.UpdatedOn,
                    BankAccNo = user.BankAccNo,
                    TermsAndCondition = user.TermsAndCondition,
                    Pincode = user.Pincode,
                    MapPath = user.MapPath,
                    Branch = user.Branch,
                    GSTNo = user.GSTNo,
                    Password = user.UserPassword,

                };
                
                return View(model);
            }



            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var crntuser = await userManager.GetUserAsync(User);
            
                string uniqueLogoName = null;
                if (model.Logo != null)
                {
                    uniqueLogoName = UploadFile(model);
                }
            else
            {
                uniqueLogoName = crntuser.Logo;
            }
                string signatureName = null;
                if (model.Signature != null)
                {
                    signatureName = UploadSign(model);
                }
            else
            {
                signatureName = crntuser.Signature;
            }
               if(crntuser!=null)
                {

                crntuser.Email = model.Email;
                crntuser.FirstName = model.FirstName;
                crntuser.LastName = model.LastName;
                crntuser.PhoneNumber = model.PhoneNumber;
                crntuser.ContactNo_1 = model.ContactNo_1;
                crntuser.ContactNo_2 = model.ContactNo_2;
                crntuser.State = model.State;
                crntuser.District = model.District;
                crntuser.City = model.City;
                crntuser.Pincode = model.Pincode;
                crntuser.Address = model.Address;
                crntuser.UserName = model.UserName;
                crntuser.InstituteName = model.InstituteName;
                crntuser.StateCode = model.StateCode;
                crntuser.GSTNo = model.GSTNo;
                crntuser.PanNo = model.PanNo;
                crntuser.UANNo = model.UANNo;
                crntuser.BankAccName = model.BankAccName;
                crntuser.BankAccNo = model.BankAccNo;
                crntuser.IfscCode = model.IfscCode;
                crntuser.Branch = model.Branch;
                crntuser.Logo = uniqueLogoName;
                crntuser.Signature = signatureName;
                crntuser.TermsAndCondition = model.TermsAndCondition;
                crntuser.ParentId = crntuser.Id;
                crntuser.MapPath = model.MapPath;
                crntuser.EmailConfirmed = true;
                 
                crntuser.UpdatedOn = System.DateTime.Today;
                crntuser.AddedBy = crntuser.AddedBy;
                crntuser.UserName = crntuser.UserName;
                crntuser.UpdatedBy = model.UpdatedBy;
                    

                };
                var result = await userManager.UpdateAsync(crntuser);
                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Admin");
                    }
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminViewModel model)
        {
            model.UserName = model.Email;
            model.ContactNo_1 = model.PhoneNumber;

            
          
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = false,
                    AddedOn = System.DateTime.Today,
                    UpdatedOn = System.DateTime.Today,
                    ContactNo_1 = model.ContactNo_1,
                    UserPassword = model.ConfirmPassword,
                     TermsAndCondition=model.TermsAndCondition
                };
                var result = await userManager.CreateAsync(user, model.Password);
              if (result.Succeeded)
              {
                    //Assign user to a role as per the modelview Selection
                    await userManager.AddToRoleAsync(user, "Institute");



                UserAccountConfirmations confirmations = new UserAccountConfirmations()
                {
                    UserAccountId = user.Id,
                    EmailConfirmation=user.EmailConfirmed,
                    IsConfirmed=false,
                };

                await userAccountConfirm.AddPendingConfirmation(confirmations);
                    var userId = await userManager.GetUserIdAsync(user);

                     
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListRole", "Admin");
                    }
                    //await signInManager.SignInAsync(user, isPersistent: false);

                    
                    return RedirectToAction("RegistrationSuccess", "Account");
              }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Admin,Institute")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ResetPassword",
                //    pageHandler: null,
                //    values: new { area = "Identity", code },
                //    protocol: Request.Scheme);

                //await emailSender.SendEmailAsync(
                //    model.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction("ForgotPasswordConfirmation");
            }

           
        

            return View(model);
        }
        [HttpGet]
        public IActionResult ForgotPasswordAdmin(string mail)
        {
            ViewBag.Mail = mail;
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ForgotPasswordAdmin(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPasswordAdmin", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    // Log the password reset link
                    _logger.Log(LogLevel.Warning, passwordResetLink);

                    //await softHomeRepository.SendResetPasswordMail(passwordResetLink, user.Email);

                    // Send the user to Forgot Password Confirmation view
                    ViewBag.ResetLink = passwordResetLink;
                    return View("ForgotPasswordResetLink");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin,Institute")]
        public IActionResult ManageAccount()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            var userName = await userManager.GetUserNameAsync(user);
            var phoneNumber = await userManager.GetPhoneNumberAsync(user);

            ProfileViewModel model = new ProfileViewModel()
            {
                UserName = userName,
                PhoneNumber= phoneNumber,
                AccountStatus="Active"
              
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            var userName = await userManager.GetUserNameAsync(user);
            var phoneNumber = await userManager.GetPhoneNumberAsync(user);

            
            if(model.PhoneNumber!=phoneNumber)
            {

                var setPhoneResult= await userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    model.AccountStatus = "Unexpected error when trying to set phone number.";
                    model.UserName = userName;
                    return View(model);
                }
                //return RedirectToAction("ManageAccount");
            }
            await signInManager.RefreshSignInAsync(user);
            model.UserName = userName;
             model.AccountStatus= "Your profile has been updated";
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> Email()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            EmailViewModel model = new EmailViewModel()
            {
                Email = user.Email,
                NewEmail=user.Email,
                IsEmailConfirmed=user.EmailConfirmed

            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> ChangeEmail(EmailViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            var email = await userManager.GetEmailAsync(user);

            if (model.NewEmail != email)
            {

                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ChangeEmailConfirmation","Account",
                    values: new { area = "Identity", userId = userId, email = email, code = code },
                    protocol: Request.Scheme);
                //await emailSender.SendEmailAsync(
                //    model.NewEmail,
                //    "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                model.StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return View(model);
               
            }
            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var hasPassword = await userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return View("Setpassword");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            model.StatusMessage = "Your password has been changed.";
            await signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> SetPassword(ChangePasswordViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }


            var addPasswordResult = await userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            model.StatusMessage = "Your password has been Set.";
            await signInManager.RefreshSignInAsync(user);
        _logger.LogInformation("User set their password successfully.");
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            TwoFactorAuthenticationViewModel model = new TwoFactorAuthenticationViewModel()
            {
                HasAuthenticator = await userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = await userManager.GetTwoFactorEnabledAsync(user),
                IsMachineRemembered = await signInManager.IsTwoFactorClientRememberedAsync(user),
                RecoveryCodesLeft = await userManager.CountRecoveryCodesAsync(user),
            };
            return View(model);
        }
        [Authorize(Roles = "Admin,Institute")]
        private string UploadFile(RegisterViewModel model)
        {
            string uniqueFileName;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "assets/images/institute");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Logo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            model.Logo.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }
        [Authorize(Roles = "Admin,Institute")]
        private string UploadSign(RegisterViewModel model)
        {
            string uniqueFileName;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "assets/images/institute");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Signature.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            model.Signature.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> FetchDistrict(string id)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "https://regionapi.sircltech.com/";

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"getdistricts/{id}");

                if (Res.IsSuccessStatusCode)
                {//HELLO CODE
                    var districts = Res.Content.ReadAsStringAsync().Result;

                    List<string> districtslist = JsonConvert.DeserializeObject<List<string>>(districts);
                    var list = districtslist.Select(s => new SelectListItem { Text = s, Value = s }).OrderBy(s => s.Text).ToList();
                    ViewBag.District = list;
                    return Json(list);
                }
                return null;
            }
        }
        [Authorize(Roles = "Admin,Institute")]
        public async Task<IActionResult> FetchCity(string id)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "https://regionapi.sircltech.com/";

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"getcities/{id}");

                if (Res.IsSuccessStatusCode)
                {//HELLO CODE
                    var city = Res.Content.ReadAsStringAsync().Result;

                    List<string> citylist = JsonConvert.DeserializeObject<List<string>>(city);
                    var list = citylist.Select(s => new SelectListItem { Text = s, Value = s }).OrderBy(s => s.Text).ToList();
                    ViewBag.City = list;
                    return Json(list);
                }
                return null;
            }
        }
    }
}
