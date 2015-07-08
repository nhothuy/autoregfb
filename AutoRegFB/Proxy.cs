using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRegFB
{
    class Proxy
    {
        private String host;

        public String Host
        {
            get { return host; }
            set { host = value; }
        }

        private String port;

        public String Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}
