using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNClient
{
    class TcpPortInfo : PortInfo
    {
        public override string Type { get; set; }

        public TcpPortInfo(int portNumber, string local, string remote, string state)
            : base(portNumber, local, remote, state)
        {
            Type = "TCP";
        }
    }
}
