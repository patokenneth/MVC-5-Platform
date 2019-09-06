using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonModules
{
    public class InfoModule : IHttpModule

    {
        public void Init(HttpApplication app)
        {
            app.EndRequest += (src, obj) => { HttpContext ctx = HttpContext.Current;

                ctx.Response.Write(string.Format("<div class='alert alert-success'> URL: {0} Status: {1}</div>", ctx.Request.RawUrl, ctx.Response.Status));
            };
        }

        public void Dispose()
        {
            //there is nothing to dispose
        }
    }
}
