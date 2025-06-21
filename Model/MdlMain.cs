using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Reflection.Emit;

namespace Model
{

    [Serializable]
    public class MdlMain {

        public string TaskId { get; set; }

        public DateTime? LogTime { get; set; } = null;

        public string LogLevel { get; set; }
        public string Logstr { get; set; }

        public string TaskDll { get; set; }

        public string TaskName { get; set; }

        public string TaskType { get; set; }

        public char TaskUse { get; set; }

        public string TaskRemark { get; set; }

        public string TaskMailToList { get; set; }

        public string TaskMailCcList { get; set; }

        public string TaskMailErrorList { get; set; }

        public string TaskServerDb { get; set; }


        public string DbName{ get; set; }
        public string IpAddress { get; set; }

        public string UserName { get; set; }

 
        public string Password { get; set; }


        public string SecretKey { get; set; }

        public DateTime? TaskLogBeginTime { get; set; } = null;

        public DateTime? TaskLogEndTime { get; set; } = null;

    }


   


}
