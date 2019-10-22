using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.MVC.Http
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjax([NotNull]this HttpRequest request, string httpVerb = "")
        {
            Check.NotNull(request, nameof(request));

            if (!string.IsNullOrEmpty(httpVerb) && request.Method != httpVerb)
                return false;

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }
    }
}
