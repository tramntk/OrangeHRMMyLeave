using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Helpers
{
    public  class APIHelpers
    {
        private static RestClient client;

        // Handle Get method
        public static T ExecuteGet<T>(string path) where T : new()
        {
            client = new RestClient(ConfigurationHelpers.GetValue<string>("url"));

            var request = new RestRequest(path, Method.Get);

            RestResponse response = client.Execute(request);

            return (T)Convert.ChangeType(response, typeof(T));
        }

        // Handle Post method
        public static T ExecutePost<T>(string path, string json) where T : new()
        {
            client = new RestClient(ConfigurationHelpers.GetValue<string>("url"));

            var request = new RestRequest(path, Method.Post);

            request.AddJsonBody(json);

            RestResponse response = client.Execute(request);

            return (T)Convert.ChangeType(response, typeof(T));
        }

        // Handle Put method
        public static T ExecutePut<T>(string path, string json) where T : new()
        {
            client = new RestClient(ConfigurationHelpers.GetValue<string>("url"));

            var request = new RestRequest(path, Method.Put);

            request.AddJsonBody(json);

            RestResponse response = client.Execute(request);

            return (T)Convert.ChangeType(response, typeof(T));
        }

        // Handle Delete method
        public static T ExecuteDelete<T>(string path) where T : new()
        {
            client = new RestClient(ConfigurationHelpers.GetValue<string>("url"));

            var request = new RestRequest(path, Method.Delete);

            RestResponse response = client.Execute(request);

            return (T)Convert.ChangeType(response, typeof(T));
        }
    }
}
