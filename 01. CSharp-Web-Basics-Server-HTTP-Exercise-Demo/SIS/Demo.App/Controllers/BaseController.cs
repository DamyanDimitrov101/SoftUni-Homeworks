using SIS.HTTP.Attributes.Action;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses.Contracts;
using SIS.HTTP.Security.Contracts;
using SIS.WebServer.Result;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Demo.App.Controllers
{
    public abstract class BaseController
    {
        public HttpRequest Request { get; protected set; }

        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText("Views" + controllerName + "/" + viewName + ".html");

            return new HtmlResult(viewContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);
        }

        public bool IsAuthorized(IIdentity user)
        {
            var attributes = Attribute.GetCustomAttributes(typeof(BaseController));

            foreach (var att in attributes)
            {
                if (att is AuthorizeAttribute)
                {
                    AuthorizeAttribute auth = (AuthorizeAttribute)att;
                    var role = auth.GetRole();
                    if (role is null)
                    {
                        return false;
                    }
                }
            }

            if (!this.IsIdentityPresent(user))
            {
                return false;
            }

            return this.IsIdentityInRole(user);
        }

        public IIdentity Identity
                => (IIdentity)this.Request.Session.GetParameter("auth");

        protected void SignIn(IIdentity auth)
        {
            this.Request.Session.AddParameter("auth", auth);
        }

        protected void SignOut()
        {
            this.Request.Session.ClearParameters();
        }

        private bool IsIdentityPresent(IIdentity identity)
            => identity.IsValid;

        private bool IsIdentityInRole(IIdentity identity)
        {
            if (IsIdentityPresent(identity))
            {
                var attributes = Attribute.GetCustomAttributes(typeof(BaseController));

                foreach (var att in attributes)
                {
                    if (att is AuthorizeAttribute)
                    {
                        AuthorizeAttribute auth = (AuthorizeAttribute)att;
                        var role = auth.GetRole();
                        if (identity.Roles.Contains(role))
                        {
                            return true;
                        }
                    }
                }                
            }
            return false;
        }
    }
}
