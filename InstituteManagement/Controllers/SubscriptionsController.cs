using CCA.Util;
using InstituteManagement.Models.Interfaces;
using InstituteManagement.Models.ViewModels;
using InstituteManagement_Models;
using InstituteManagement_Models.Subscriptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InstituteManagement.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionRepo subscriptionRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        //public static decimal payment;
       
        public static decimal commonamount;
        public static string orderid;
        public static string razorpayid;
        public SubscriptionsController(ISubscriptionRepo subscriptionRepo, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.subscriptionRepo = subscriptionRepo;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index(string? message)
        {
            ViewBag.Message = message;
            var model = await subscriptionRepo.GetPlans();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePlan(string? message)
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlan(Plans plans)
        {
            plans.CreationDate = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await subscriptionRepo.CreatePlans(plans);
                    if (result != null)
                    {
                        string message = "Plan Created Successfully!";
                        return RedirectToAction("CreatePlan", new {message});
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View();
        }

        
        public async Task<IActionResult> DeletePlan(int id)
        {
            var model = await subscriptionRepo.GetPlanById(id);
            try
            {
                if (model != null)
                {
                    model.IsActive = false;
                    await subscriptionRepo.UpdatePlans(model);
                    var message = "Plan Deleted Successfully!";
                    return RedirectToAction("Index", new {message});
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewPlan(int planId)
        {
            try
            {
                var model = await subscriptionRepo.GetPlanById(planId);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditPlan(int id, string? message)
        {
            ViewBag.Message = message;
            var model= await subscriptionRepo.GetPlanById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPlan(Plans plans)
        {
            if (plans != null)
            {
                try
                {

                    await subscriptionRepo.UpdatePlans(plans);
                    var message = "Plan Updated Successfully!";
                    return RedirectToAction("Index", new {message});

                }
                catch (Exception ex)
                {
                    var message = "Error In Updating Plan ! Re-try After Some Time";
                    return View("Index", new {message});
                }
            }
            return View();
        }

        public async Task<IActionResult> GetSubscriptions(string? message)
        {
            ViewBag.Message=message;
            var model= await subscriptionRepo.GetSubscriptions();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSubscription()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(Subscription subscription)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = await subscriptionRepo.CreateSubscription(subscription);
                    if (result != null)
                    {
                        ViewBag.Message = "Subscription Created Successfully!";
                        return View();
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.Message=ex.Message;
                    return View(subscription);
                }
            }
            return View(subscription);
        }

        public async Task<IActionResult> DeleteSubscription(int Id)
        {
            var model = await subscriptionRepo.SubscriptionById(Id);
            try
            {
                if (model != null)
                {
                    model.IsActive = false;
                    await subscriptionRepo.UpdateSubscription(model);
                    ViewBag.Message = "Subscrption Deleted Successfully!";

                    return RedirectToAction("GetSubscriptions");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
            return RedirectToAction("GetSubscriptions");
        }

        [HttpGet]
        public async Task<IActionResult> EditSubscription(int id)
        {
            var model = await subscriptionRepo.SubscriptionById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubscription(Subscription subscription)
        {
            if (subscription != null)
            {
                try
                {

                    await subscriptionRepo.UpdateSubscription(subscription);
                    ViewBag.Message = "Subscription Updated Successfully!";
                    return RedirectToAction("GetSubscriptions");

                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(subscription);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Payment(Subscription model)
        {
            
            
            // Retrieve the plan details, discounts, and calculate the final payment amount

            // Redirect to the payment page with the amount and other necessary parameters
            //return Redirect($"/payment/checkout?amount={amount}&discounts={discount}");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubscribePlan(int itemid)
        {
            
            string strencrequest = configuration["strencrequest"];
            string strAccessCode = configuration["strAccessCode"];
            string merchantdesc = configuration["merchantdesc"];
            string merchantname = configuration["merchantname"];
            string merchant_id = configuration["merchant_id"];
            string workingKey = configuration["workingKey"];
            string redirect_url = configuration["redirect_url"];
            string cancel_url = configuration["cancel_url"];

            var plan = await subscriptionRepo.GetPlanById(itemid);
            if (plan != null)
            {
                decimal Amount = plan.Price;
                decimal discount = plan.Discount;
                decimal discountAmount = Amount * (discount / 100);
                decimal finalAmount = Amount - discountAmount;
                var user = await userManager.GetUserAsync(User);
                var crntsub = await subscriptionRepo.SubscriptionByUserId(user.Id);
                if (crntsub != null)
                {
                    crntsub.Plans = plan;
                    crntsub.ApplicationUser = user;
                    crntsub.UserId= user.Id;
                    crntsub.IsActive = false;
                    crntsub.Name = plan.PlanName;
                    crntsub.StartDate=DateTime.UtcNow;
                    crntsub.EndDate = DateTime.UtcNow.AddMonths(Convert.ToInt32(plan.Duration));
                    crntsub.CreationDate = DateTime.UtcNow;
                    crntsub.PlanId = plan.PlanID;
                    crntsub.IsPaymentComplete = false;
                    crntsub.AmountPaid = 0;
                }
                await subscriptionRepo.UpdateSubscription(crntsub);

                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    Merchant_id = merchant_id,
                    Billing_address= user.Address,
                    Billing_country="india",
                    Billing_state=user.State,
                    Billing_city=user.City,
                    Billing_email=user.Email,
                    Billing_name=user.UserName,
                    Currency="INR",
                    AppUserId=user.Id,
                    Order_id= Convert.ToString(crntsub.Id),
                    Amount=finalAmount.ToString(),
                    Billing_zip=user.Pincode,
                    Billing_tel=user.PhoneNumber,
                    planId=plan.PlanID

                   
                };
                OrderData orderData = new OrderData();
                orderData.Merchant_id = merchant_id;
                orderData.Redirect_url = redirect_url;
                orderData.Order_id = crntsub.Id.ToString();
                orderData.Amount = finalAmount.ToString();
                orderData.Currency = "INR";
                orderData.Cancel_url = cancel_url;
                orderData.planId = plan.PlanID;
                
                orderData.Redirect_url = redirect_url;
                return RedirectToAction("Fee", orderData);
            }
            return View();
        }

        public IActionResult Fee(OrderData orderData)
        {
            //tid=1677824662277&merchant_id=2110484&order_id=123654789&amount=1.00&currency=INR&redirect_url=http://192.168.0.89/MCPG.ASP.net.2.0.kit/ccavResponseHandler.aspx&cancel_url=http://192.168.0.96/mcpg_new/iframe/ccavResponseHandler.php&
            CCACrypto crypto = new CCACrypto();
            string ccareq = $"tid={orderData.tid}&merchant_id={orderData.Merchant_id}&order_id={orderData.Order_id}&" +
                $"amount={orderData.Amount}&currency={orderData.Currency}&" +
                $"redirect_url={orderData.Redirect_url}&cancel_url={orderData.Cancel_url}";
            SendRequest sendReq = new SendRequest();
            sendReq.RequestData = crypto.Encrypt(ccareq, "83275763A245F9315B80080FBD2168E4");
            sendReq.AccessCode = "AVEF23KB72BU51FEUB";
            return View(sendReq);
        }
        [HttpPost]
        public async Task<IActionResult> Response()
        {

            CCACrypto crypto = new CCACrypto();
            var str = crypto.Decrypt(Request.Form["encResp"], "83275763A245F9315B80080FBD2168E4");
           

            var dict = HttpUtility.ParseQueryString(str);
            string json = new JavaScriptSerializer().Serialize(
                                dict.AllKeys.ToDictionary(k => k, k => dict[k])

                       );
            //string str = crypto.Decrypt(outputString.ToString(), "83275763A245F9315B80080FBD2168E4");

            ResponseData response = JsonConvert.DeserializeObject<ResponseData>(json);


            return RedirectToAction("CallBackSuccess", response);

        }

        public async Task<IActionResult> CallBackSuccess(ResponseData Id)
        {

            //var SrNo = root.data[0].srNo;
            if ( Id.order_id == "" || Id.status_message != "Y")
            {
                Id.order_status = "Failure";
                return View("Response", Id);
            }

            else
            {

                ResponseStatusApi res = await getdata(Id.order_id.ToString());       //compare payment data

                if (res.resstatus != "0" || res.order_status == "Aborted" || res.order_status == "Failure" || res.order_bank_response != "Y" )
                {
                    Id.order_status = "failed";

                    return View("Response", Id);

                }

                var subRes = await subscriptionRepo.SubscriptionById(Convert.ToInt32(Id.order_id));
                string razorkey = configuration["razorkey"];
                string razorsecret = configuration["razorsecret"];

              
                    //var succesid = success.razorpay_payment_id;
                    //var order = success.razorpay_order_id;
                    //var sig = success.razorpay_signature;
                    Payment update = new Payment();


                    update.srNo = Convert.ToInt32(res.order_no);
                    update.AppUserId = subRes.UserId;
                    update.PlanId = subRes.PlanId;
                    update.SubscriptionId = subRes.Id;
                    update.status = res.order_status;
                    update.amountPaid = res.order_amt.ToString();
                    long ticks = DateTime.UtcNow.Ticks;

                    // generate a random number between 1000 and 9999
                    Random rnd = new Random();
                    int randomNumber = rnd.Next(1000, 9999);

                    // concatenate the timestamp and the random number to create a unique order ID
                    string orderId = $"{ticks}{randomNumber}";
                    update.razorpay_Payment_Id = Id.tracking_id;
                    update.razorpay_Order_Id = orderId;
                    update.razorpay_Signature = res.sig;
                    //update.schoolId = root.data[0].schoolID;
                    //update.sessionId = root.data[0].sessionID;
                    string merchantdesc = configuration["merchantdesc"];
                    update.authat = res.order_status_date_time;
                    update.createdat = res.order_bank_response;
                    update.capat = res.order_delivery_details;
                    update.authcode = res.order_status_date_time;
                    update.desc = merchantdesc;
                    update.method = res.order_card_name;
                    update.currency = res.order_currncy;
                    update.ctype = res.order_device_type;




               
                    restapi resapi = new restapi();
                   
                       

                        Id.order_id = res.order_no;
                        Id.amount = res.order_amt.ToString();
                         await subscriptionRepo.AddPayment(update);
                        // resapi = Newtonsoft.Json.JsonConvert.DeserializeObject<resapi>(response);
                        return View("Response", Id);
                

            }
            return View("Response", Id);

        }

        public async Task<ResponseStatusApi> getdata(string orderid = "225")
        {


            ResponseStatusApi res = new ResponseStatusApi();

            string workingKey = configuration["workingKey"];
            string strAccessCode = configuration["strAccessCode"];

            CCACrypto crypto = new CCACrypto();


            JObject jsonObject = new JObject();
            jsonObject.Add("order_no", orderid);

            // var jsonObject = JsonConvert.DeserializeObject(ccareq);

            // string ccreq = $"order_no=\"{orderid}\"";
            //  ccareq = "{" + ccreq.Replace("=", "\":\"").Replace("\"", "\\\"") + "}";



            // string enc = "d75e52938653c0c80cdce13238b1a9b41d5cb5d186034445f13185985c2b6782";

            var enc = crypto.Encrypt(jsonObject.ToString(), workingKey);



            using (var client = new HttpClient())
            {
                var baseurl = $"https://logintest.ccavenue.com";
                //Passing service base url
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent("");
                HttpResponseMessage Res = await client.PostAsync($"/apis/servlet/DoWebTrans?enc_request={enc}&access_code={strAccessCode} &command=orderStatusTracker&request_type=JSON&response_type=JSON&command=orderStatusTracker&version=1.2", content);




                //Checking the response is successful or not which is sent using HttpClient
              //  RootStudent root = new RootStudent();
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;



                    var dict = HttpUtility.ParseQueryString(EmpResponse);
                    var encr = dict[1];
                    if (dict[0] != "0")
                    {
                        return res;
                    }

                    string outputString = encr.Replace("\r\n", "");

                    //string json = new JavaScriptSerializer().Serialize(
                    //                    dict.AllKeys.ToDictionary(k => k, k => dict[k])
                    //           );

                    //StatusApi response = JsonConvert.DeserializeObject<StatusApi>(json);

                    //string req =$"{response.enc_response}";

                    string str = crypto.Decrypt(outputString.ToString(), "83275763A245F9315B80080FBD2168E4");

                    //var dicts = HttpUtility.ParseQueryString(str);
                    //string jsonres = new JavaScriptSerializer().Serialize(
                    //                    dicts.AllKeys.ToDictionary(k => k, k => dicts[k])
                    //           );

                    res = JsonConvert.DeserializeObject<ResponseStatusApi>(str);
                    res.resstatus = dict[0];
                    res.sig = encr;
                    //Deserializing the response recieved from web api and storing into the Employee list

                }
                //returning the employee list to view



            }
            return res;

        }

        public IActionResult CallBackCancel()
        {
            return View();
        }

    }
}
