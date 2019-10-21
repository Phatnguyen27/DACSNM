using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;

namespace DACSNM
{
    class GetIPForwardTable
    {
        List<Route> data;
        public GetIPForwardTable()
        {
            data = new List<Route>();
        }
        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        internal struct IPForwardTable
        {
            public uint Size;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public IPFORWARDROW[] Table;
        };

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        internal struct IPFORWARDROW
        {
            internal uint dwForwardDest;
            internal uint dwForwardMask;
            internal uint dwForwardPolicy;
            internal uint dwForwardNextHop;
            internal uint dwForwardIfIndex;
            internal uint dwForwardType;
            internal uint dwForwardProto;
            internal uint dwForwardAge;
            internal uint dwForwardNextHopAS;
            internal uint dwForwardMetric1;
            internal uint dwForwardMetric2;
            internal uint dwForwardMetric3;
            internal uint dwForwardMetric4;
            internal uint dwForwardMetric5;
        };

        static IPForwardTable ReadIPForwardTable(IntPtr tablePtr)
        {
            var result = (IPForwardTable)Marshal.PtrToStructure(tablePtr, typeof(IPForwardTable));

            IPFORWARDROW[] table = new IPFORWARDROW[result.Size];
            IntPtr p = new IntPtr(tablePtr.ToInt64() + Marshal.SizeOf(result.Size));
            for (int i = 0; i < result.Size; ++i)
            {
                table[i] = (IPFORWARDROW)Marshal.PtrToStructure(p, typeof(IPFORWARDROW));
                p = new IntPtr(p.ToInt64() + Marshal.SizeOf(typeof(IPFORWARDROW)));
            }
            result.Table = table;

            return result;
        }

        public List<Route> getRoutes()
        {
            return this.data;
        }
        public void getIpForwardTable()
        {
            var fwdTable = IntPtr.Zero;
            int size = 0;
            var result = NativeMethods.GetIpForwardTable(fwdTable, ref size, true);
            fwdTable = Marshal.AllocHGlobal(size);
            result = NativeMethods.GetIpForwardTable(fwdTable, ref size, true);
            var forwardTable = ReadIPForwardTable(fwdTable);
            Marshal.FreeHGlobal(fwdTable);
            for (int i = 0; i < forwardTable.Table.Length; ++i)
            {
                /*Console.Write("\tRoute[{0}] If Index: {1}\n", i, forwardTable.Table[i].dwForwardIfIndex);
                Console.Write("\tRoute[{0}] Type: {1}\n", i, forwardTable.Table[i].dwForwardType);
                Console.Write("\tRoute[{0}] Proto: {1}\n", i, forwardTable.Table[i].dwForwardProto);
                Console.Write("\tRoute[{0}] Age: {1}\n", i, forwardTable.Table[i].dwForwardAge);
                Console.Write("\tRoute[{0}] Metric1: {1}\n", i, forwardTable.Table[i].dwForwardMetric1);*/
                Route route = new Route();
                route.DestinationIP = new IPAddress((long)forwardTable.Table[i].dwForwardDest).ToString();
                route.SubneskMask = new IPAddress((long)forwardTable.Table[i].dwForwardMask).ToString();
                route.Metric1 = forwardTable.Table[i].dwForwardMetric1.ToString();
                route.GateWay = new IPAddress((long)forwardTable.Table[i].dwForwardNextHop).ToString();
                route.InterfaceIP = string.Copy(forwardTable.Table[i].dwForwardIfIndex.ToString());
                data.Add(route);
            }
        }
        public void getInterfaceIP()
        {
            int size=0;
            IntPtr pBuffer = IntPtr.Zero;
            NativeMethods.GetIpAddrTable(IntPtr.Zero, ref size, false);
            pBuffer = Marshal.AllocHGlobal(size);
            int ipAddressTable = NativeMethods.GetIpAddrTable(pBuffer, ref size, false);
            if (ipAddressTable != 0)
                throw new System.ComponentModel.Win32Exception(ipAddressTable);
            int entrySize = Marshal.SizeOf(typeof(NativeStructure.MIB_IPADDRROW));
            int nEntries = Marshal.ReadInt32(pBuffer);
            int tableStartAddress = (int)pBuffer + sizeof(int);
            string ipAddress = "";
            int index = 0;
            for (int i=0;i < nEntries;i++)
            {
                IntPtr pEntry = (IntPtr)(tableStartAddress + i * entrySize);
                NativeStructure.MIB_IPADDRROW addressStructure = (NativeStructure.MIB_IPADDRROW)Marshal.PtrToStructure(pEntry, typeof(NativeStructure.MIB_IPADDRROW));
                ipAddress = IPToString((IPAddress.NetworkToHostOrder(addressStructure.address)));
                index = addressStructure.index;
                matchIP(index, ipAddress);
            }
        }
        
        public void matchIP(int index,string ip)
        {
            foreach(Route i in this.data)
            {
                int j = 0;
                Int32.TryParse(i.InterfaceIP, out j);
                if ( j == index)
                    i.InterfaceIP = ip;
            }
        }

        public void run()
        {
            this.getIpForwardTable();
            this.getInterfaceIP();
        }

        public string IPToString(int ipaddr)
        {
            return string.Format("{0}.{1}.{2}.{3}",
            (ipaddr >> 24) & 0xFF, (ipaddr >> 16) & 0xFF,
            (ipaddr >> 8) & 0xFF, ipaddr & 0xFF);
        }
    }
}
