using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class DatabaseTest : BaseTest
    {
        DatabaseService databaseService;
        public DatabaseTest()
        {
            databaseService = new DatabaseService();
        }

        [TestMethod]
        public void VerifyCourseInfo() {
            var result = databaseService.GetCourseInformation();
            Assert.AreEqual(result.Name, "SQL Basic");
            Assert.AreEqual(result.IdCourse, 1);
        }
    }
}
