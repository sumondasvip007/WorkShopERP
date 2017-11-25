using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using HanifWorkShop;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HanifWorkShop.Models;
using HanifWorkShop.Models.ViewModel;
using HanifWorkShop.Utility;

namespace HanifWorkShop.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        //
        // GET: /Account/Login
        [AllowAnonymous]

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView();
        }

        //
        // POST: /Account/Login
      
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            TempData["errorMessage"] = string.Empty;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);

                    using (HanifWorkShop_DBEntity rc = new HanifWorkShop_DBEntity())
                    {
                        try
                        {
                            tblWorkShopUser WorkShopUser = new tblWorkShopUser();
                            var workShop = unitOfWork.WorkShopInformationRepository.Get().Where(x => x.short_name == model.AgencyName).FirstOrDefault();
                            WorkShopUser.UserId = user.Id;
                            WorkShopUser.is_loggedIn = 1;
                            WorkShopUser.last_logged_time = DateTime.Now;
                            WorkShopUser.workShop_id = workShop.workShop_id;
                            unitOfWork.WorkShopUserRepository.Update(WorkShopUser);
                            unitOfWork.Save();
                            SessionManger.SetLoggedInTime(Session, DateTime.Now);

                            SessionManger.SetLoggedInUser(Session, model.UserName, workShop.workShop_id);
                            return Redirect("/#Index");
                            // return Json(new { success = true, successMessage = "Successfully User Created" });

                        }
                        catch (Exception)
                        {
                            return Redirect("/#Login");
                            throw;

                        }

                    }

                    //return RedirectToLocal(returnUrl);

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    TempData["errorMessage"] = "Invalid username or password.";
                    //return View(model); View(model);
                    //System.Threading.Thread.SpinWait(60); 
                    return Redirect("/#Login");
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(model);
            TempData["errorMessage"] = "Invalid username or password.";
            //return View(model); View(model);
            //System.Threading.Thread.SpinWait(60); 
            return Redirect("/#Login");
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var results = await UserManager.FindByNameAsync(model.UserName);
                if (results != null)
                {
                    return Json(new { success = false, errorMessage = "This User Name already exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Create User
                    var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    HanifWorkShop_DBEntity AgencyCal = new HanifWorkShop_DBEntity();
                    AspNetUser anu = new AspNetUser();

                    //AspNetUser userId = RestaurantCal.AspNetRoles.Select(a => a.Name == model.UserName).FirstOrDefault();
                    AspNetUser searchUser = AgencyCal.AspNetUsers.Where(a => a.UserName == model.UserName).FirstOrDefault();
                    decimal workShopId = SessionManger.WorkShopOfLoggedInUser(Session);
                    if (result.Succeeded)
                    {

                        using (HanifWorkShop_DBEntity rc = new HanifWorkShop_DBEntity())
                        {
                            try
                            {
                                tblWorkShopUser AgencyUser = new tblWorkShopUser();


                                AgencyUser.UserId = searchUser.Id;
                                AgencyUser.workShop_id = workShopId;
                                AgencyUser.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                AgencyUser.CreatedBy = SessionManger.LoggedInUser(Session);
                                AgencyUser.CreatedDateTime = DateTime.Now;
                                AgencyUser.EditedBy = null;
                                AgencyUser.EditedDateTime = null;
                                // RestaurantUser.is_admin = 0;
                                //RestaurantUser.is_loggedIn = null;
                                //RestaurantUser.last_logged_time = null;
                                //RestaurantUser.approve_by = null;
                                //RestaurantUser.approve_date = null;
                                rc.tblWorkShopUsers.Add(AgencyUser);
                                rc.SaveChanges();

                                return Json(new { success = true, successMessage = "User has created Successfully." }, JsonRequestBehavior.AllowGet);

                            }
                            catch (Exception ex)
                            {
                                return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "This Email has already taken." }, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid." }, JsonRequestBehavior.AllowGet);
            }


        
        }



        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddAspNetUser()
        {
            return View();
        }
        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AspNetUserList()
        {
            return View();
        }

        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllUser()
        {
            try
            {
                var userList = (from a in unitOfWork.AspNetUserRepository.Get()
                                select new VM_AspNetUser()
                                {
                                    Id = a.Id,
                                    UserFullName = a.UserFullName,
                                    UserName = a.UserName,
                                    PhoneNo = a.PhoneNumber,
                                    Email = a.Email
                                    //Password = a.PasswordHash
                                }).ToList();
                return Json(new { success = true, result = userList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult DeleteUser(string Id)
        {
            try
            {
                AspNetUser aUser = unitOfWork.AspNetUserRepository.GetByID(Id);
                tblWorkShopUser rUser =
                   unitOfWork.WorkShopUserRepository.Get().Where(x => x.UserId == Id).FirstOrDefault();
                if (aUser == null)
                {
                    return Json(new {errorMessage = "User Delete Failed." }, JsonRequestBehavior.AllowGet);
                }
                unitOfWork.WorkShopUserRepository.Delete(rUser);
                unitOfWork.AspNetUserRepository.Delete(aUser);
                
                
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "User Delete Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateUser(VM_AspNetUser aUser)
        {
            AspNetUser aspNetUser = unitOfWork.AspNetUserRepository.GetByID(aUser.Id);
            aspNetUser.Id = aUser.Id;
            aspNetUser.UserFullName = aUser.UserFullName;
            aspNetUser.UserName = aUser.UserName;
            aspNetUser.PhoneNumber = aUser.PhoneNo;
            aspNetUser.Email = aUser.Email;
            tblWorkShopUser aAgencyUser =
                unitOfWork.WorkShopUserRepository.Get(a => a.UserId == aUser.Id).FirstOrDefault();
            aAgencyUser.EditedBy = SessionManger.LoggedInUser(Session);
            aAgencyUser.EditedDateTime = DateTime.Now;
            //PASSOWRD save+ UserName & Email uniQ check required

            var EmailExist = unitOfWork.AspNetUserRepository.Get()
                .Where(x => x.Email == aUser.Email && x.Id != aUser.Id).ToList();
            var UserNameExist = unitOfWork.AspNetUserRepository.Get()
           .Where(x => x.UserName == aUser.UserName && x.Id != aUser.Id).ToList();

            try
            {

                if (!EmailExist.Any())
                {
                    if (!UserNameExist.Any())
                    {
                        //UPDATE

                        unitOfWork.WorkShopUserRepository.Update(aAgencyUser);
                        unitOfWork.AspNetUserRepository.Update(aspNetUser);
                        unitOfWork.Save();
                        return Json(new { success = true, successMessage = "User has updated Successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        return Json(new { success = false, errorMessage = "This user name already exist." }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {

                    return Json(new { success = false, errorMessage = "This email already exist." }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
            }

        }






        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult UserActionMapping()
        {
            return View();
        }

        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllUserForCurrentAgency()
        {

            decimal workShopId = SessionManger.WorkShopOfLoggedInUser(Session);
            try
            {
                //var users = DropDown.ddlUsers(membershipId);
                var users = unitOfWork.WorkShopUserRepository.Get().Where(x => x.workShop_id == workShopId).Select(
                a => new
                {
                    text = a.AspNetUser.UserName,
                    value = a.UserId
                }).ToList();

                return Json(new { success = true, result = users }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //[AllowAnonymous]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult UserMenuActionForPermission(string userId)
        {
            //decimal id = SessionManger.BrokerOfLoggedInUser(Session).membership_id;
            decimal workShopId = SessionManger.WorkShopOfLoggedInUser(Session);

            //string user_id = User.Identity.GetUserId();
            //var views = userActionMappingFactory.GetAll().Where(a => a.user_id == user_id)
            //    .Select(x => x.action_id).ToList();
            var data = unitOfWork.ModuleRepository.Get()
            .Select(a => new
            {
                a.ModuleId,
                label = a.ModuleName,
                children = a.tblActions.Where(x => x.IsInMenu == true)
                .Select(ac
                    => new
                    {
                        ac.ActionId,
                        label = ac.ActionDisplayName,
                        //a.membership_id,
                        selected = ac.tblUserActionMappings.Where(x => x.user_id == userId).Select(x => x.user_id).FirstOrDefault() == null ? false : true
                    }
                    ).ToList()
            }).ToList();

            return Json(new { success = true, result = data }, JsonRequestBehavior.AllowGet);
        }
        //[AllowAnonymous]
        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult SaveUserPermission(decimal[] id, string userId)
        {
            //userActionMappingFactory = new UserActionMappingFactory();
            //actionFactory = new ActionFactory();
            try
            {
                List<tblAction> actions = new List<tblAction>();
                actions = unitOfWork.ActionRepository.Get().ToList();

                List<tblUserActionMapping> actionMappings = new List<tblUserActionMapping>();
                actionMappings = unitOfWork.UserActonMappingRepository.Get().Where(a => a.user_id == userId).ToList();

                foreach (var items in actionMappings)
                {
                    unitOfWork.UserActonMappingRepository.Delete(items);
                }

                if (id != null)
                {
                    foreach (var items in id)
                    {
                        tblUserActionMapping actionMapping = new tblUserActionMapping();
                        actionMapping.action_id = Int32.Parse(items.ToString());
                        actionMapping.user_id = userId;
                        actionMapping.is_permitted = 1;
                        actionMapping.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                        actionMapping.CreatedBy = SessionManger.LoggedInUser(Session);
                        actionMapping.CreatedDateTime = DateTime.Now;
                        actionMapping.EditedBy = null;
                        actionMapping.EditedDateTime = null;
                        unitOfWork.UserActonMappingRepository.Insert(actionMapping);
                   
                    }
                }
                unitOfWork.Save();

                return Json(new { success = true, successMessage = "Mapped Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }



        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult LoadModules()
        {


            try
            {
                decimal id = SessionManger.WorkShopOfLoggedInUser(Session);
                string user_id = User.Identity.GetUserId();
                var result = unitOfWork.ModuleRepository.Get()
                .Select(a => new
                {
                    a.ModuleId,
                    a.ModuleName,
                    a.ModuleIcon,
                    action = a.tblActions.Where(b => b.IsInMenu == true && b.tblUserActionMappings.Where(am => am.user_id==user_id && am.is_permitted == 1).Any())
                    .Select(ac
                        => new
                        {
                            ac.ActionId,
                            ac.ActionName,
                            ac.ActionDisplayName,
                            ac.ActionUrl
                        }
                        ).ToList()
                }).Where(a => a.action.Count() > 0).ToList();
                return Json(new { result = result, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

















        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //return RedirectToAction("Index", "Home");

            AuthenticationManager.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            //return RedirectToAction("Index", "Home");
            //return Redirect("/#Index");
            return Redirect("/#");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}