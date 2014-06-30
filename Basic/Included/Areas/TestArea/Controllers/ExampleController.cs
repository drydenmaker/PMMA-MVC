using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Included.Areas.TestArea.Controllers
{
    public class ExampleController : Controller
    {
        //
        // GET: /TestArea/Example/
        public ActionResult Index()
        {
            ViewBag.Message = "SUCCESS!!!";
            return View();
        }
	}
}