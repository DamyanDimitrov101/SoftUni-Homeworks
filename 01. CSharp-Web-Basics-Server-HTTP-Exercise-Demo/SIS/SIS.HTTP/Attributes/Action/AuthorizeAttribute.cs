using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Attributes.Action
{
    public class AuthorizeAttribute : Attribute
    {
        private readonly string role;

        public AuthorizeAttribute() { }

        public AuthorizeAttribute(string role)
        {
            this.role = role;
        }

        public string GetRole()
              => this.role;
    }
}
