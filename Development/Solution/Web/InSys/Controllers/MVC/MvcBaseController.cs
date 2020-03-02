using InSys.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InSys.Controllers
{
    public abstract class MvcBaseController : Controller
    {
        protected void GenerateToken()
        {
            ViewBag.EncryptionString = EncryptionHandler.CreateClientToken(Request);
        }
    }
}