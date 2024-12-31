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
        [TestMethod ("TC_API001: Verify that can get list users")]
        public void VerifyGetListUsers()
        {
            var randomPage = new Random().Next(1, 3);

            var request = new RestRequest("api/users?page=" + randomPage, Method.Get);
            RestResponse response = client.Execute(request);

            //Verify return status code 200
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responsedata = JsonConvert.DeserializeObject<ListUsersModel>(response.Content);
            
            //Verify response page value = request page value
            Assert.AreEqual(randomPage, responsedata.page);
            
            //Verify response data count > 0
            int dataCount = responsedata.data.Count;
            Assert.AreNotSame(0, dataCount);
        }

        [TestMethod("TC_API002: Verify that can create user")]
        public void VerifyCreateUser()
        {
            //input request data
            var requestBody = new UserRequestModel();
            requestBody.name = "Tram";
            requestBody.job = "QA Automation";

            var request = new RestRequest("api/user", Method.Post);
            request.AddJsonBody(requestBody);

            RestResponse response = client.Execute(request);

            //Verify return status code 201
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            //Verify response data (name, job) = request date (name, job)
            var responsedata = JsonConvert.DeserializeObject<CreateUserResponseModel>(response.Content);
            Assert.AreEqual(requestBody.name, responsedata.name);
            Assert.AreEqual(requestBody.job, responsedata.job);
        }

        [TestMethod("TC_API003: Verify that can update user")]
        public void VerifyUpdateUser()
        {
            //Input request data
            var randomId = new Random().Next(1, 12 + 1);
            var requestBody = new UserRequestModel();
            requestBody.name = "TramNTK";
            requestBody.job = "QA Automation";

            var request = new RestRequest("api/user/" + randomId, Method.Put);
            request.AddJsonBody(requestBody);

            RestResponse response = client.Execute(request);

            //Verify return status code 200
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //Verify response data (name, job) = request date (name, job)
            var responsedata = JsonConvert.DeserializeObject<UpdateUserRespondModel>(response.Content);
            Assert.AreEqual(requestBody.name, responsedata.name);
            Assert.AreEqual(requestBody.job, responsedata.job);
        }
    }
}
