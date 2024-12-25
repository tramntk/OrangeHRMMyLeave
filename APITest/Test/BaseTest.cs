using Automation.Core.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.Test
{
    [TestClass]
    public class BaseTest
    {
        protected RestClient client;

        [TestInitialize]
        public void SetUpRestClient()
        {
            client = new RestClient(ConfigurationHelpers.GetValue<string>("url"));
        }
    }
}
