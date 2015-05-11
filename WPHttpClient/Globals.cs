using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace WPHttpClient
{
    //this class is used for using local storage
    public class Globals<T>
    {

        string tag;
        T defaultValue;

        public Globals(string tag, T defaultValue)
        {
            this.tag = tag;
            this.defaultValue = defaultValue;
        }
        public T Value
        {
            get
            {
                T valueFromIsolatedStorage;

                if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue(tag, out valueFromIsolatedStorage))
                {
                    IsolatedStorageSettings.ApplicationSettings[tag] = defaultValue;
                    return defaultValue;
                }
                else
                    return valueFromIsolatedStorage;
            }

            set
            {
                IsolatedStorageSettings.ApplicationSettings[tag] = value;
            }
        }
    }

    //this class the methods that we use to make different requests to the api, each method is for a given request type ie, POST, GET and PUT
   public  class ApiCall
    {
        public static string api_url = "http://yourjsonapiurl/";
        public string content_type = "application/vnd.api+json";
        public string requestData;
        public string resource_url;

        public async Task<string> postRequest()
        {
            using (var client = new HttpClient())
            {
                Globals<string> access_token = new Globals<string>("user_token", "");
                Debug.WriteLine(access_token.Value + "the access token");
                var contentData = new StringContent(requestData, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(api_url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.PostAsync(resource_url, contentData);
                string contentString = await message.Content.ReadAsStringAsync();
                return contentString;
            }
        }

        public async Task<string> getRequest()
        {
            using (var client = new HttpClient())
            {
                Globals<string> access_token = new Globals<string>("user_token", "");
                client.BaseAddress = new Uri(api_url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "token=" + access_token.Value);
                HttpResponseMessage message = await client.GetAsync(resource_url);
                string contentString = await message.Content.ReadAsStringAsync();
                return contentString;
            }
        }

        public async Task<string> putRequest()
        {
            using (var client = new HttpClient())
            {
                Globals<string> access_token = new Globals<string>("user_token", "");
                client.BaseAddress = new Uri(api_url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "token=" + access_token.Value);
                var contentData = new StringContent(requestData, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync(resource_url, contentData);
                string contentString = await message.Content.ReadAsStringAsync();
                return contentString;
            }
        }

    }
   class Login
   {
       public class User
       {
           public int id { get; set; }
           public string email { get; set; }
           public object firstname { get; set; }
           public object lastname { get; set; }
           public object phone { get; set; }
           public int role { get; set; }
           public string href { get; set; }
       }

       public class Links
       {
           public User user { get; set; }
       }

       public class Sessions
       {
           public int id { get; set; }
           public string access_token { get; set; }
           public Links links { get; set; }
       }

       public class RootObject
       {
           public Sessions sessions { get; set; }
       }
   }
}
