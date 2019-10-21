using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACSNM
{
    class Route
    {
        private string destinationIP;
        private string subneskMask;
        private string gateWay;
        private string Metric;
        private string interfaceIP;

        public string DestinationIP { get => destinationIP; set => destinationIP = value; }
        public string SubneskMask { get => subneskMask; set => subneskMask = value; }
        public string GateWay { get => gateWay; set => gateWay = value; }
        public string Metric1 { get => Metric; set => Metric = value; }
        public string InterfaceIP { get => interfaceIP; set => interfaceIP = value; }
    }
}
