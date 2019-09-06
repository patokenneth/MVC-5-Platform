﻿using SimpleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(GetTimeStamp());
            //HttpContext.Application["event"]
        }

        [HttpPost]

        public ActionResult Index(Color color)
        {
            Color? oldColor = Session["color"] as Color?;

            if (oldColor != null)
            {
                Votes.ChangeVote(color, (Color)oldColor);
            }
            else
            {
                Votes.RecordVote(color);
            }

            ViewBag.SelectedColor = Session["color"] = color;
           // System.Diagnostics.Debugger.Break();
            return View(GetTimeStamp());
            //HttpContext.Application["event"]
        }

        public ActionResult Modules()
        {
            var modules = HttpContext.ApplicationInstance.Modules;
            Tuple<string, string>[] data =
            modules.AllKeys
            .Select(x => new Tuple<string, string>(
            x.StartsWith("__Dynamic") ? x.Split('_', ',')[3] : x,
            modules[x].GetType().Name))
            .OrderBy(x => x.Item1).ToArray();

            return View(data);
        }

        public IList<string> GetTimeStamp()
        {
            IList<string> TimeList = new List<string> {
             string.Format("Application Time : {0}", HttpContext.Application["time_stamp"]),
            string.Format("Request Time : {0}", Session["request_stamp"]) };

            //TimeList.Add(mod);
            //TimeList.Add(modII);

            return TimeList;
        }
    }
}