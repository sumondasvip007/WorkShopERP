using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using HanifWorkShop.Models;
using Microsoft.AspNet.Identity;

namespace HanifWorkShop.Utility
{
    public class SessionManger
    {
        public HttpResponse response;
        //UnitofWork unitOfWork = new UnitofWork();

        public static String LoggedInUser(HttpSessionStateBase session)
        {
            return (String)session["LoggedInUser"];
        }

        public static void SetLoggedInUser(HttpSessionStateBase session, string WorkShopUser, decimal id)
        {
            session["LoggedInUser"] = WorkShopUser;
            session["WorkShopId"] = id;
        }

        public static Decimal WorkShopOfLoggedInUser(HttpSessionStateBase session)
        {
            if ((session["WorkShopId"] == null))
            {
                return 0;
            }
            else
            {
                return (Decimal)session["WorkShopId"];
            }

        }


        public static DateTime LoggedInTime(HttpSessionStateBase session)
        {
            if (session["LoggedInTime"] != null)
            {
                return (DateTime)session["LoggedInTime"];
            }
            else
                return DateTime.Now;

        }

        public static void SetLoggedInTime(HttpSessionStateBase session, DateTime datetime)
        {
            session["LoggedInTime"] = datetime;
        }

        public static bool IsSessionNull(HttpSessionStateBase session)
        {
            if (session.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public static bool isUserLoggedIn()
        {
            decimal? loggedIn = 0;
            string userId = HttpContext.Current.User.Identity.GetUserId();
            HanifWorkShop_DBEntity WorkShopEn = new HanifWorkShop_DBEntity();

            loggedIn = WorkShopEn.tblWorkShopUsers.Where(a => a.UserId == userId).Select(a => a.is_loggedIn).FirstOrDefault();
            if (loggedIn == 1)
                return true;
            else
                return false;
        }

        public static decimal? GetUserLoggedIn(string userId)
        {
            decimal? loggedIn = 0;
            HanifWorkShop_DBEntity WorkShopEn = new HanifWorkShop_DBEntity();

            loggedIn = WorkShopEn.tblWorkShopUsers.Where(a => a.UserId == userId).Select(a => a.is_loggedIn).FirstOrDefault();
            return loggedIn;
        }



        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
        public class CheckUserSessionAttribute : ActionFilterAttribute
        {
            public string user_id { get; set; }

            //private IGenericFactory<tblBrokerUser> brokerUserFactory;
            public UserManager<ApplicationUser> UserManager { get; private set; }
            //private IGenericFactory<tblUserActionMapping> userActionMappingFactory;
            public string PropertyName { get; private set; }

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                //var user = session["BrokerOfLoggedInUser"];
                if ((session["LoggedInUser"] == null))
                {
                    if (GetUserLoggedIn(HttpContext.Current.User.Identity.GetUserId()) == 1)
                    {
                        //RestaurantEntities rce = new RestaurantEntities();
                        //tblRestaurantUser ru = new tblRestaurantUser();  

                        ////brokerUserFactory = new BrokerUserFactory();
                        ////decimal membership_id = SessionManger.BrokerOfLoggedInUser(Session).membership_id;
                        //string user_id = HttpContext.Current.User.Identity.GetUserId();
                        //int RestaurentId = Convert.ToInt32(session["RestaurantId"]);
                        //ru.is_loggedIn = 0;
                        //ru.UserId = user_id;
                        //ru.Restaurant_id = RestaurentId;
                        //rce.tblRestaurantUsers.Attach(ru);
                        //var entry = rce.Entry(ru);
                        //entry.State = EntityState.Modified;
                        //rce.SaveChanges();


                        filterContext.HttpContext.GetOwinContext().Authentication.SignOut();
                        session.RemoveAll();
                        session.Clear();
                        session.Abandon();
                    }

                    //send them off to the login page
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.Content("~/Account/Login");
                    filterContext.Result = new RedirectResult(loginUrl);
                    return;
                }


            }


        }
    }
}