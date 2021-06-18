using SIS.HTTP.Common;
using SIS.HTTP.Cookies.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private List<HttpCookie> _cookies;
        private string HttpCookieStringSeparator = Environment.NewLine;

        public HttpCookieCollection()
        {
            this._cookies = new List<HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            this._cookies.Add(cookie);
        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            bool result = this._cookies.Any(c => c.Key == key);

            return result;
        }

        public HttpCookie GetCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            HttpCookie result = this._cookies.Find(c => c.Key == key);

            return result;
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return _cookies.GetEnumerator();
        }

        public bool HasCookies()
        {
            return this._cookies.Any();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(HttpCookieStringSeparator, _cookies);
        }
    }
}
