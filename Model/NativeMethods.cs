using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DACSNM
{
    public static class NativeStructure
    {
        public const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;
        public const int ERROR_BUFFER_OVERFLOW = 111;
        public const int MAX_ADAPTER_NAME_LENGTH = 256;
        public const int MAX_ADAPTER_ADDRESS_LENGTH = 8;
        public const int MIB_IF_TYPE_OTHER = 1;
        public const int MIB_IF_TYPE_ETHERNET = 6;
        public const int MIB_IF_TYPE_TOKENRING = 9;
        public const int MIB_IF_TYPE_FDDI = 15;
        public const int MIB_IF_TYPE_PPP = 23;
        public const int MIB_IF_TYPE_LOOPBACK = 24;
        public const int MIB_IF_TYPE_SLIP = 28;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct IP_ADDR_STRING
        {
            public IntPtr Next;
            public IP_ADDRESS_STRING IpAddress;
            public IP_ADDRESS_STRING IpMask;
            public Int32 Context;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct IP_ADAPTER_INDEX_MAP
        {
            public int Index;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String Name;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct IP_ADDRESS_STRING
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string Address;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct IP_ADAPTER_INFO
        {
            public IntPtr Next;
            public Int32 ComboIndex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ADAPTER_NAME_LENGTH + 4)]
            public string AdapterName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ADAPTER_DESCRIPTION_LENGTH + 4)]
            public string AdapterDescription;
            public UInt32 AddressLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ADAPTER_ADDRESS_LENGTH)]
            public byte[] Address;
            public Int32 Index;
            public UInt32 Type;
            public UInt32 DhcpEnabled;
            public IntPtr CurrentIpAddress;
            public IP_ADDR_STRING IpAddressList;
            public IP_ADDR_STRING GatewayList;
            public IP_ADDR_STRING DhcpServer;
            public bool HaveWins;
            public IP_ADDR_STRING PrimaryWinsServer;
            public IP_ADDR_STRING SecondaryWinsServer;
            public Int32 LeaseObtained;
            public Int32 LeaseExpires;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct IP_INTERFACE_INFO
        {
            public Int32 numAdapters;
            public IP_ADAPTER_INDEX_MAP[] adapter;
            public static IP_INTERFACE_INFO FromByteArray(Byte[] buffer)
            {
                unsafe
                {
                    IP_INTERFACE_INFO info = new IP_INTERFACE_INFO();
                    int iNumAdapters = 0;
                    Marshal.Copy(buffer, 0, new IntPtr(&iNumAdapters), 4);
                    IP_ADAPTER_INDEX_MAP[] adapters = new IP_ADAPTER_INDEX_MAP[iNumAdapters];
                    info.numAdapters = iNumAdapters;
                    info.adapter = new IP_ADAPTER_INDEX_MAP[iNumAdapters];
                    int offset = sizeof(int);
                    for (int i = 0; i < iNumAdapters; i++)
                    {
                        IP_ADAPTER_INDEX_MAP map = new IP_ADAPTER_INDEX_MAP();
                        IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(map));
                        Marshal.StructureToPtr(map, ptr, false);
                        Marshal.Copy(buffer, offset, ptr, Marshal.SizeOf(map));
                        map = (IP_ADAPTER_INDEX_MAP)Marshal.PtrToStructure(ptr, typeof(IP_ADAPTER_INDEX_MAP));
                        Marshal.FreeHGlobal(ptr);
                        info.adapter[i] = map;
                        offset += Marshal.SizeOf(map);
                    }
                    return info;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MIB_IPADDRROW
        {
            public int address;
            public int index;
            public int mask;
            public int broadcastAddress;
            public int reassemblySize;
            public ushort unused1;
            public ushort type;
        }
    }
    public static class NativeMethods
    {
        //Platform Invoke C++'s GetIpForwardTable
        [DllImport("iphlpapi", CharSet = CharSet.Auto)]
        public extern static int GetIpForwardTable(IntPtr /*PMIB_IPFORWARDTABLE*/ pIpForwardTable, ref int /*PULONG*/ pdwSize, bool bOrder);

        //Platform Invoke C++'s GetAdaptersInfo
        [DllImport("iphlpapi", CharSet = CharSet.Auto)]
        public extern static int GetAdaptersInfo(IntPtr pAdapterInfo,ref Int64 pBufOutLen);

        //Platform Invoke C++'s CreateIpForwardEntry
        [DllImport("iphlpapi", CharSet = CharSet.Auto)]
        public extern static int CreateIpForwardEntry(IntPtr /*PMIB_IPFORWARDROW*/ pRoute);

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto)]
        public static extern int GetInterfaceInfo(Byte[] PIfTableBuffer, ref int size);

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto)]
        public static extern int IpReleaseAddress(ref NativeStructure.IP_ADAPTER_INDEX_MAP AdapterInfo);
        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetIpAddrTable(IntPtr pIpAddrTable, ref int pdwSize, bool bOrder);
    }
}
