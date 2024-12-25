using APITest.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITest.Test
{
    [TestClass]
    public class UsersTest: BaseTest
    {
        [TestMethod]
        public void VerifyGetListUsers()
        {
            var randomPage = new Random().Next(1, 3);

            var request = new RestRequest("api/users?page=" + randomPage, Method.Get);
            RestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responsedata = JsonConvert.DeserializeObject<ListUsersModel>(response.Content);
            Assert.AreEqual(randomPage, responsedata.page);
            
            int dataCount = responsedata.data.Count;
            Assert.AreNotSame(0, dataCount);
        }

        [TestMethod]
        public void VerifyCreateUser()
        {
            var requestBody = new CreateUserRequestModel();
            requestBody.name = "Tram";
            requestBody.job = "QA Automation";

            var request = new RestRequest("api/user", Method.Post);

            request.AddJsonBody(requestBody);

            RestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responsedata = JsonConvert.DeserializeObject<CreateUserResponseModel>(response.Content);

            Assert.AreEqual(requestBody.name, responsedata.name);

            Assert.AreEqual(requestBody.job, responsedata.job);

        }
    }
}
