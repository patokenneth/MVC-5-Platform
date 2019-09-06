using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SimpleApp.Infrastructure
{
    public class TotalTimeModule : IHttpModule
    {
        private static float totalTime = 0;
        private static int requestCount = 0;

        public void Dispose()
        {
            //throw new NotImplementedException();
            //Do nothing else
        }

        public void Init(HttpApplication app)
        {
            IHttpModule module = app.Modules["Timer"];

            if (module != null && module is TimerModule)
            {
                TimerModule timer = (TimerModule)module;
                timer.RequestTimed += (src, args) =>
                {
                    totalTime += args.Duration;
                    requestCount++;
                };
            }

            app.EndRequest += (src, args) =>
            {
                app.Context.Response.Write(CreateSummary());
            };
           

        
        }

        private void App_EndRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private string CreateSummary()
            {
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class,
                "table table-bordered");
             htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "success");

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                   htmlWriter.Write("Requests");
                   htmlWriter.RenderEndTag();
                   htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                   htmlWriter.Write(requestCount);
                   htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "success");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                   htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                   htmlWriter.Write("Total Time");
                   htmlWriter.RenderEndTag();
                   htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                   htmlWriter.Write("{0:F5} seconds", totalTime);
                   htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

             htmlWriter.RenderEndTag();

                return stringWriter.ToString();
            }

      
    }
}