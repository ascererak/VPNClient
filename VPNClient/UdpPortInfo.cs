using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNClient
{
    class UdpPortInfo : PortInfo
    {
        public override string Type { get; set; }

        public UdpPortInfo(int portNumber) : base(portNumber)
        {
            Type = "UDP";
        }
    }
}
