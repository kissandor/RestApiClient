using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restApiClient
{
    class Resp
    {
        public Resp() { }
        public string status { get; set; }
        public string status_message { get; set; }
        public string token { get; set; }
    }
}
