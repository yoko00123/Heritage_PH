using InSys.Helpers;
using InSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using z.Controller;
using z.Data;

namespace InSys.Controllers
{
    [NoCache]
    public class HomeController : MvcBaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                //var r = HttpContext.Request.Cookies["XSRF-TOKEN"]?.Value;
                //if (r != null)
                //{
                //    HttpContext.Request.Headers.Add("X-CSRF-Token", r); //r.CompressFromUriEncoded()
                //}

                //if (AuthorizationToken.IsAuthenticated(HttpContext) && Session["Session"] != null)
                //{
                //    var token = EncryptionManager.GenerateAppToken();

                //    ViewBag.EncryptionString = token.ToJson().CompressUriEncoded();
                //    ViewBag.SessionTimeOut = Config.Get("SessionTimeOut");

                //    Session.Add("Token", token.ToJson().CompressUriEncoded());

                //    var skin = HttpContext.Session["Skin"].ToString();


                //    var useMenu = Convert.ToInt32(Config.Get("UseMenu"));
                //    return View("Main", new ActionModel { skin = skin, useMenu = useMenu });
                //}
                //else
                //    return RedirectToAction("Index", "Account");

                GenerateToken();

                if (Session["Session"] != null)
                {
                    ViewBag.SessionTimeOut = Config.Get("SessionTimeOut");

                    var useMenu = Convert.ToInt32(Config.Get("UseMenu"));
                    return View("Main", new ActionModel { useMenu = useMenu });
                } else
                    return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public ActionResult Error(string ex)
        {
            //  AuthorizationToken.Clear(HttpContext);
            // Commented by Yoku 06222018 to Enable the New 404 scenario which the users will not forcely log out
            return View(ex);
        }
    }
}