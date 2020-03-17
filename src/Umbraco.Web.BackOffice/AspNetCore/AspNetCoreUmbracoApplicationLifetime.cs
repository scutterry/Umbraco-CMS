using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Umbraco.Net;

namespace Umbraco.Web.AspNet
{
    public class AspNetCoreUmbracoApplicationLifetime : IUmbracoApplicationLifetime
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public AspNetCoreUmbracoApplicationLifetime(IHttpContextAccessor httpContextAccessor, IHostApplicationLifetime hostApplicationLifetime)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public bool IsRestarting { get; set; }
        public void Restart()
        {
            IsRestarting = true;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                // unload app domain - we must null out all identities otherwise we get serialization errors
                // http://www.zpqrtbnk.net/posts/custom-iidentity-serialization-issue
                httpContext.User = null;
            }

            Thread.CurrentPrincipal = null;
            _hostApplicationLifetime.StopApplication();
        }
    }
}