using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleApp.Infrastructure
{
    public class DayofWeekHandler : IHttpHandler
    {
        public HttpContext context { get; set; }
        string day = DateTime.Now.DayOfWeek.ToString();

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
           
        }

        public void Handler()
        {
            if (context.Request.CurrentExecutionFilePathExtension == ".json")
            {
                context.Response.ContentType = "application/html";
                context.Response.Write(string.Format("{}", day));

            }
        }
    }
}