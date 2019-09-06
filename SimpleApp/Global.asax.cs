using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        { 
        //    BeginRequest += (src, args) => RecordEvent("BeginRequest");
        //    AuthenticateRequest += (src, args) => RecordEvent("AuthenticateRequest");
        //    PostAuthenticateRequest += (src, args) => RecordEvent("PostAuthenticateRequest");

            PostAcquireRequestState += (src, args) => TimeStamp();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            TimeStamp();
            
        }

        private void TimeStamp()
        {
            string stamp = Context.Timestamp.ToLongTimeString();

            if (Context.Session != null)
            {
                Session["request_stamp"] = stamp;
            }
            else
            {
                Application["time_stamp"] = stamp;
            }
        }

        //protected void Application_BeginRequest()
        //{
        //    RecordEvent("BeginRequest");

        //}

        //protected void Application_AuthenticateRequest()
        //{
        //    RecordEvent("Authenticate");
        //}

        //protected void Application_PostAuthenticateRequest()
        //{
        //    RecordEvent("PostAuthenticate");
        //}

        private void RecordEvent(string EventName)
        {
            List<string> eventlist = Application["event"] as List<string>;

            if (eventlist == null)
            {
                Application["event"] = eventlist = new List<string>();
            }

            eventlist.Add(EventName);
        }

    }
}
