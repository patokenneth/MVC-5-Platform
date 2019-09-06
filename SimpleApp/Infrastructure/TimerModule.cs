using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SimpleApp.Infrastructure
{
    public class RequestTimerEventArgs : EventArgs
    {
        public float Duration { get; set; }
    }
    public class TimerModule : IHttpModule
    {
        public event EventHandler<RequestTimerEventArgs> RequestTimed;

        private Stopwatch timer;

        void IHttpModule.Dispose()
        {
           //do nothing. No resource to dispose.
        }

        public void Init(HttpApplication context)

        {
            context.BeginRequest += HandleEvent;
            context.EndRequest += HandleEvent;
        }

        private void HandleEvent(object src, EventArgs arg)
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.CurrentNotification == RequestNotification.BeginRequest )   
            {
                timer = Stopwatch.StartNew();
                
            }
            else
            {
                float duration = ((float)timer.ElapsedTicks) / Stopwatch.Frequency;
                ctx.Response.Write(string.Format(
                 "<div class='alert alert-success'>Elapsed: {0:F5} seconds</div>",
             duration));

                //if (RequestTimed != null)
                //{
                //    RequestTimed(this, new RequestTimerEventArgs { Duration = duration });
                //}

                RequestTimed?.Invoke(this, new RequestTimerEventArgs { Duration = duration });
            }
        }
    }
}