using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace z.Web.Service
{
    //public class Result
    //{
    //    public string ResultSet;
    //    public int Status; // 0:1
    //    public string ErrorMsg;
    //}

    [DataContract]
    public class Result
    {
        public Result()
        {
            Status = 0;
        }

        [DataMember]
        public string ResultSet;
        [DataMember]
        public int Status; // 0:1
        [DataMember]
        public string ErrorMsg;
    }

    [DataContract]
    public class StreamResult : Result
    {
        [DataMember]
        public System.IO.Stream stream;
    }
}
