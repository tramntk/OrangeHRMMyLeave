namespace MyLeaveTest.SqlQueries
{
    public static class Queries
    {
        public static string querySelectCourseByStartDate = "Select IdCourse, Name from Course Where Startdate = '2023-01-01'";

        public static string querySelectScore = "select Fullname, Account, AvgScore from Course c, Mentee m, Score s  where c.IdCourse = s.IdCourse and s.IdMentee = m.IdMentee  and c.IdCourse = 1;";

    }
}
