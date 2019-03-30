using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNClient
{
    abstract class PortInfo
    {
        public int PortNumber { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public string State { get; set; }
        public abstract string Type { get; set; }

        public PortInfo(int portNumber, string local, string remote, string state)
        {
            PortNumber = portNumber;
            Local = local;
            Remote = remote;
            State = state;
        }

        public PortInfo(int portNumber)
        {
            PortNumber = portNumber;
            Local = "Not supported";
            Remote = "Not supported";
            State = "Not supported";
        }
    }
}
