using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Stml.Infrastructure.Applications.MVC.Http
{
    public class AjaxAttribute : ActionMethodSelectorAttribute
    {
        public string HttpVerb { get; set; }

        public AjaxAttribute(string httpVerb)
        {
            HttpVerb = httpVerb;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return routeContext.HttpContext.Request.IsAjax(HttpVerb);
        }
    }
}
