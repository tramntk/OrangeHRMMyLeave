using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Model
{
    public class Messages
    {
        public Messages()
        {

        }
        public string LoginFail { get; set; }
        public string RequiredErr { get; set; }
        public string DateFormatErr { get; set; }
        public string FromDateToDateErr { get; set; }
        public string NoRecords { get; set; }
        public string SuccessSubmit { get; set; }
    }
}
