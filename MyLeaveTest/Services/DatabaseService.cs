using Automation.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLeaveTest.Model;
using MyLeaveTest.SqlQueries;
namespace MyLeaveTest
{
    public class DatabaseService
    {
        string connectionString = ConfigurationHelpers.GetValue<string>("connectionstring");
        public Course GetCourseInformation()
        {
            string query = Queries.querySelectCourseByStartDate;
            var result = SqlHelpers.ExecuteQuery<Course>(connectionString, query);
            return result.FirstOrDefault();
        }
        public List<Score> GetUserScore()
        {
            string query = Queries.querySelectScore;
            var result = SqlHelpers.ExecuteQuery<Score>(connectionString, query);
            return result;
        }
    }

    
}
