using InSys.Helpers;
using InSys.Models;
using System;
using System.Web;
using System.Web.Mvc;
using z.Controller;
using z.Data;

namespace InSys.Controllers
{
    [NoCache]
    public class AccountController : MvcBaseController
    {

        public AccountController()
        {
            
        } 

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
             
            GenerateToken();
             
            return View();
        }

        [AllowAnonymous]
        public ActionResult Companies() //string ID
        {
            GenerateToken();

            if (HttpContext.Session["tmpAuthID"] == null) return Redirect("Index"); //|| ID == null 
            return View(); //new { ID = ID }
        }

        [AllowAnonymous]
        public ActionResult PasswordSetUp()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            AuthorizationToken.Clear(HttpContext);
            Session.Remove("Session");
            return RedirectToAction("Index");
        }



        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}