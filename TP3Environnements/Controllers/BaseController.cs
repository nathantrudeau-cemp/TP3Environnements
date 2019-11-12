using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TP3Environnements.Controllers
{
    public class BaseController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SetCulture(string culture)
        {
            // En utilisant la session
            Session["environnementCourant"] = culture;

            // En utilisant un cookie
            HttpCookie cookie = Request.Cookies["environnementCourant"];
            if (cookie != null)
                cookie.Value = culture;
            else
            {
                cookie = new HttpCookie("environnementCourant");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            // Redirection à la page où l'utilisateur était déjà
        }

        [AllowAnonymous]
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string culture = "";

            //// En utilisant la session   
            //if (Session["culture"] == null)
            //    Session["culture"] = "fr";
            //culture = (string)Session["culture"];

            // En utilisant un cookie
            HttpCookie cultureCookie = Request.Cookies["environnementCourant"];
            if (cultureCookie != null)
                culture = cultureCookie.Value;
            else
            {
                SetCulture(Request.Url.ToString().Contains("dev") ? "dev" : "master");
            }


            return base.BeginExecuteCore(callback, state);
        }
    }
}