using SIS.HTTP.Common;
using SIS.HTTP.Sessions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private IDictionary<string,object> _parameters;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this._parameters = new Dictionary<string, object>();
            this.Id = id;
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            this._parameters.Add(name,parameter);
        }

        public void ClearParameters()
        {
            this._parameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this._parameters.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this._parameters.Select(n=> n.Key==name);
        }
    }
}
