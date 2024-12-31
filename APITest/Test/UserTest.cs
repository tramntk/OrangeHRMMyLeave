using APITest.Model;
using Automation.Core.Helpers;
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
    public class UserTest
    {
        [TestMethod("TC_API001: Verify that can get list users")]
        public void VerifyGetListUsers()
        {
            var randomPage = new Random().Next(1, 3);
            string path = "api/users?page=" + randomPage;

            var response = APIHelpers.ExecuteGet<RestResponse>(path);         

            // Verify return status code 200
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responsedata = JsonConvert.DeserializeObject<ListUsersModel>(response.Content);

            // Verify response page value = request page value
            Assert.AreEqual(randomPage, responsedata.page);

            // Verify response data count > 0
            int dataCount = responsedata.data.Count;
            Assert.AreNotSame(0, dataCount);
        }

        [TestMethod("TC_API002: Verify that can create user")]
        public void VerifyCreateUser()
        {
            // Input request data     
            string filePath = "Data/userdata.json"; 
            string json = File.ReadAllText(filePath);

            var requestBody = JsonConvert.DeserializeObject<UserRequestModel>(json);

            string path = "api/user";

            var response = APIHelpers.ExecutePost<RestResponse>(path, json);

            // Verify return status code 201
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // Verify response data (name, job) = request date (name, job)
            var responsedata = JsonConvert.DeserializeObject<CreateUserResponseModel>(response.Content);
            Assert.AreEqual(requestBody.name, responsedata.name);
            Assert.AreEqual(requestBody.job, responsedata.job);
        }

        [TestMethod("TC_API003: Verify that can update user")]
        public void VerifyUpdateUser()
        {
            // Input request data         
            string filePath = "Data/userdata.json";
            string json = File.ReadAllText(filePath);

            var requestBody = JsonConvert.DeserializeObject<UserRequestModel>(json);

            var randomId = new Random().Next(1, 12 + 1);

            string path = "api/user/" + randomId;           

            var response = APIHelpers.ExecutePut<RestResponse>(path, json);

            // Verify return status code 200
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Verify response data (name, job) = request date (name, job)
            var responsedata = JsonConvert.DeserializeObject<UpdateUserRespondModel>(response.Content);
            Assert.AreEqual(requestBody.name, responsedata.name);
            Assert.AreEqual(requestBody.job, responsedata.job);
        }

        [TestMethod("TC_API004: Verify that can delete user")]
        public void VerifyDeleteUser()
        {
            var randomId = new Random().Next(1, 12 + 1);

            string path = "api/user/" + randomId;

            var response = APIHelpers.ExecuteDelete<RestResponse>(path);

            // Verify return status code 204
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
