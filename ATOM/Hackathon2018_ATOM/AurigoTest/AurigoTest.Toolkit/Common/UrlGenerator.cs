//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AurigoTest.Toolkit.Common.Helpers
//{
//    class UrlHelper
//    {
//    }
//}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AurigoTest.Toolkit.Common
{
    public class UrlGenerator
    {
        private const bool DEFAULT_OVERRIDE_FOR_ADD = true;

        #region Private Properties

        private string _inputURL;
        public string InputURL
        {
            get { return _inputURL; }
            set
            {
                _inputURL = value;

                if (_inputURL != null)
                    _isInputURLHavingParam = _inputURL.Contains("?"); //true means it already has params}
            }
        }

        /// <summary>
        /// DO NOT SET THIS VARIABLE except from InputURL property
        /// </summary>
        private bool _isInputURLHavingParam;
        /// <summary>
        /// This will be set automaticall every time InputURL is set
        /// </summary>
        public bool IsInputURLHavingParam { get { return _isInputURLHavingParam; } }


        private NameValueCollection KeyValueParams { get; set; } = new NameValueCollection();
        #endregion Private Properties

        #region Helper Methods : Private

        private void ConvertInputURL_ToParameters()
        {
            int indexOfQ = InputURL.IndexOf('?');

            if (indexOfQ >= 0 && InputURL.Length > indexOfQ + 1)
            {
                string dummyURL = "http://localhost";
                string newUrlString = InputURL;
                if (InputURL.StartsWith("~"))
                    newUrlString = InputURL.Remove(0, 1).Insert(0, dummyURL); //StringBuilder urlSbr = new StringBuilder(InputURL);urlSbr.Remove(0, 1);urlSbr.Insert(0, );newUrlString = urlSbr.ToString();
                else if (InputURL.StartsWith("/"))
                    newUrlString = InputURL.Insert(0, dummyURL);
                else if (InputURL.StartsWith("?"))
                    newUrlString = InputURL.Insert(0, dummyURL + "/");
                else if (InputURL.IndexOf("//") < 0)
                    newUrlString = InputURL.Insert(0, dummyURL + "/");

                Uri myUri = new Uri(newUrlString);
                NameValueCollection nvc = HttpUtility.ParseQueryString(myUri.Query);

                string onlyURL = InputURL.Substring(0, indexOfQ);

                this.InputURL = onlyURL;

                //foreach (var keyItem in nvc.AllKeys)
                //{ this.KeyValueParams[keyItem] = nvc[keyItem]; }

                foreach (var keyItem in this.KeyValueParams.AllKeys)
                { nvc[keyItem] = this.KeyValueParams[keyItem]; }

                var tempList = this.KeyValueParams;
                this.KeyValueParams = nvc;

                tempList.Clear();
            }
        }

        private string CleanURL(string url)
        {
            if (url == null)
                url = string.Empty;

            url = url.Trim();

            if (url.EndsWith("?"))
                url = url.Remove(url.Length - 1);

            return url;
        }

        #endregion Helper Methods : Private

        #region Constructor
        public UrlGenerator(string url)
        {
            this.InputURL = this.CleanURL(url);
            this.ConvertInputURL_ToParameters();
        }

        public UrlGenerator(string url, NameValueCollection parameters) : this(url)
        {
            if (parameters != null)
                this.KeyValueParams = parameters;
        }
        #endregion Constructor

        #region Add methods : Key, Value
        public UrlGenerator Add(string key, string value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD)
        {
            if (isOverride)
            {
                if (this.IsInputURLHavingParam && this.InputURL.IndexOf(key, StringComparison.InvariantCultureIgnoreCase) > 0)
                    this.ConvertInputURL_ToParameters();

                KeyValueParams.Set(key, value);
            }
            else
            {
                if (this.IsInputURLHavingParam && this.InputURL.IndexOf(key, StringComparison.InvariantCultureIgnoreCase) > 0)
                    this.Remove(key);
            }
            return this;
        }
        public UrlGenerator Add(string key, short value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, int value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, long value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, double value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, float value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, decimal value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, DateTime value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        public UrlGenerator Add(string key, Guid value, bool isOverride = DEFAULT_OVERRIDE_FOR_ADD) { return this.Add(key, value.ToString(), isOverride); }
        #endregion Add methods : Key, Value

        #region Merge methods : Using Response/Request or existing params
        public UrlGenerator Merge(NameValueCollection copySourceParams)
        {
            foreach (string key in copySourceParams)
            {
                // Overwrite any entry already there
                this.Add(key, copySourceParams[key]);
            }

            return this;
        }

        #endregion Merge methods : Using Response/Request or existing params

        #region AddIfExists
        public UrlGenerator AddIfKeyExists(string keyToCheck, string value)
        {
            if (KeyValueParams.AllKeys.Any(t => t.Equals(keyToCheck, StringComparison.InvariantCultureIgnoreCase)))
                KeyValueParams[keyToCheck] = value;

            if (this.IsInputURLHavingParam)
            {
                int indexOfQ = InputURL.IndexOf('?');

                if (indexOfQ >= 0 && InputURL.Substring(indexOfQ).IndexOf(keyToCheck, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    this.Remove(keyToCheck);
                    KeyValueParams[keyToCheck] = value;
                }
            }

            return this;
        }
        public UrlGenerator AddIfKeyExists(string key, short value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, int value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, long value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, double value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, float value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, decimal value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, DateTime value) { return this.AddIfKeyExists(key, value.ToString()); }
        public UrlGenerator AddIfKeyExists(string key, Guid value) { return this.AddIfKeyExists(key, value.ToString()); }

        #endregion AddIfExists

        #region Remove Methods
        public UrlGenerator RemoveAll()
        {
            KeyValueParams.Clear();

            if (this.IsInputURLHavingParam)
            {
                int indexOfQ = InputURL.IndexOf('?');

                if (indexOfQ >= 0)
                    InputURL = InputURL.Substring(0, indexOfQ);
            }
            return this;
        }

        public UrlGenerator Remove(params string[] keys)
        {
            if (this.IsInputURLHavingParam)
                this.ConvertInputURL_ToParameters();

            foreach (string keyItem in keys)
            {
                KeyValueParams.Remove(keyItem);
            }

            return this;

            #region Old COde
            //int indexOfQ = InputURL.IndexOf('?');

            //if (indexOfQ >= 0)
            //    this.ConvertInputURL_ToParameters();

            //int indexOfKey = InputURL.IndexOf(keys, StringComparison.InvariantCultureIgnoreCase);

            //if (indexOfQ >= 0 && indexOfKey > 0 && indexOfKey > indexOfQ)
            //{
            //    ConvertInputURL_ToParameters();

            //    KeyValueParams.Remove(keys);


            //    //string dummyURL = "http://localhost";
            //    //string newUrlString = InputURL;
            //    //if (InputURL.StartsWith("~"))
            //    //    newUrlString = InputURL.Remove(0, 1).Insert(0, dummyURL); //StringBuilder urlSbr = new StringBuilder(InputURL);urlSbr.Remove(0, 1);urlSbr.Insert(0, );newUrlString = urlSbr.ToString();
            //    //else if (InputURL.StartsWith("/"))
            //    //    newUrlString = InputURL.Insert(0, dummyURL);
            //    //else if (InputURL.StartsWith("?"))
            //    //    newUrlString = InputURL.Insert(0, dummyURL + "/");
            //    //else if (InputURL.IndexOf("//") < 0)
            //    //    newUrlString = InputURL.Insert(0, dummyURL + "/");

            //    //Uri myUri = new Uri(newUrlString);
            //    //NameValueCollection nvc = HttpUtility.ParseQueryString(myUri.Query);

            //    //nvc.Remove(key);

            //    //string onlyURL = InputURL.Substring(0, indexOfQ);

            //    ////return new UrlGenerator(onlyURL, nvc);//this can also be done but will create unnecessary object

            //    //this.InputURL = onlyURL;

            //    //foreach (var keyItem in nvc.AllKeys)
            //    //{ this.KeyValueParams[keyItem] = nvc[keyItem]; }

            //}

            //return this;
            #endregion Old COde
        }
        #endregion Remove Methods

        #region Check If Exists

        private bool ContainsHelper_IsExistsInInputUrl(string str)
        {
            if (this.IsInputURLHavingParam)
            {
                int indexOfQ = InputURL.IndexOf('?');

                return (indexOfQ >= 0 && InputURL.Substring(indexOfQ).IndexOf(str, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            return false;
        }

        public bool ContainsKey(string key)
        {
            if (KeyValueParams.AllKeys.Any(t => t.Equals(key, StringComparison.InvariantCultureIgnoreCase)))
                return true;

            if (this.IsInputURLHavingParam)
            {
                string keyValueToSearch = HttpUtility.UrlEncode(key) + "=";
                return ContainsHelper_IsExistsInInputUrl(keyValueToSearch);
            }

            return false;
        }

        public bool ContainsKeyWithValue(string key, string value)
        {
            if (KeyValueParams.AllKeys.Any(t => t.Equals(key, StringComparison.InvariantCultureIgnoreCase)) && KeyValueParams[key] == value)
                return true;

            if (this.IsInputURLHavingParam)
            {
                string keyValueToSearch = HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value);
                return ContainsHelper_IsExistsInInputUrl(keyValueToSearch);
            }

            return false;
        }

        #endregion Check If Exists

        #region Final Query Builder Methods
        public string ToQueryString()
        {
            var array = (from key in KeyValueParams.AllKeys
                         from value in KeyValueParams.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();

            string finalUrl = string.Empty;

            if (array.Any())
            {
                if (IsInputURLHavingParam)
                    return this.InputURL + "&" + string.Join("&", array);
                else
                    return this.InputURL + "?" + string.Join("&", array);
            }
            else
                return this.InputURL;
        }

        public override string ToString()
        {
            return this.ToQueryString();
        }
        #endregion Final Query Builder Methods


        #region Get option
        public string GetValueForKey(string key)
        {
            if (KeyValueParams.AllKeys.Any(t => t.Equals(key, StringComparison.InvariantCultureIgnoreCase)))
                return KeyValueParams[key];

            if (this.IsInputURLHavingParam)
            {
                string keyValueToSearch = HttpUtility.UrlEncode(key) + "=";
                bool isContained = ContainsHelper_IsExistsInInputUrl(keyValueToSearch);

                if (isContained)
                {
                    this.ConvertInputURL_ToParameters();

                    return KeyValueParams[key];
                }
            }

            return null;
        }
        #endregion Get option
    }

    //for ref:  http://stackoverflow.com/questions/724526/how-to-pass-multiple-parameters-in-a-querystring
    //          http://stackoverflow.com/questions/24059773/correct-way-to-pass-multiple-values-for-same-parameter-name-in-get-request

    public static class UrlGeneratorExtention
    {
        public static UrlGenerator ToURL(this string urlString)
        {
            return new UrlGenerator(urlString);
        }
    }
}

/*

var nvc = HttpUtility.ParseQueryString(Request.Url.Query);
    nvc.Remove("editFlag");
    string url = Request.Url.AbsolutePath + "?" + nvc.ToString();
     Response.Redirect(url);
*/

