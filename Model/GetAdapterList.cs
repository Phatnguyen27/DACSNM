using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DACSNM
{
    
    class GetAdapterList
    {
        List<Adapter> data;
        public GetAdapterList()
        {
            this.data = new List<Adapter>();
        }
        public List<Adapter> Data { get => data; }

        public void getAdaptersList()
        {
            long IP_ADAPTER_INFOstructSize = Marshal.SizeOf(typeof(NativeStructure.IP_ADAPTER_INFO));
            IntPtr pArray = Marshal.AllocHGlobal(new IntPtr(IP_ADAPTER_INFOstructSize));

            int adapters = NativeMethods.GetAdaptersInfo(pArray, ref IP_ADAPTER_INFOstructSize);

            if (adapters == NativeStructure.ERROR_BUFFER_OVERFLOW) // ERROR_BUFFER_OVERFLOW == 111
            {
                pArray = Marshal.ReAllocHGlobal(pArray, new IntPtr(IP_ADAPTER_INFOstructSize));
                adapters = NativeMethods.GetAdaptersInfo(pArray, ref IP_ADAPTER_INFOstructSize);
            }

            if (adapters == 0)
            {
                // Call Succeeded
                IntPtr pEntry = pArray;
                do
                {
                    // Retrieve the adapter info from the memory address
                    NativeStructure.IP_ADAPTER_INFO entry = (NativeStructure.IP_ADAPTER_INFO)Marshal.PtrToStructure(pEntry, typeof(NativeStructure.IP_ADAPTER_INFO));
                    Adapter newAdapter = new Adapter();
                    newAdapter.Name = entry.AdapterName;
                    newAdapter.Description = entry.AdapterDescription;
                    newAdapter.Address = Adapter.ByteArrayToString(entry.Address);
                    data.Add(newAdapter);
                    pEntry = entry.Next;
                }
                while (pEntry != IntPtr.Zero);
                Marshal.FreeHGlobal(pArray);
            } // if
            else
            {
                Marshal.FreeHGlobal(pArray);
                throw new InvalidOperationException("GetAdaptersInfo failed: " + adapters);
            }
        }
        public NativeStructure.IP_INTERFACE_INFO getInterfaceInfo()
        {
            int size = 0;
            int interfaceInfo = NativeMethods.GetInterfaceInfo(null, ref size);
            Byte[] buffer = new Byte[size];
            interfaceInfo = NativeMethods.GetInterfaceInfo(buffer, ref size);
            if (interfaceInfo != 0)
                throw new Exception("GetInterfaceInfo returned an error.");
            NativeStructure.IP_INTERFACE_INFO info = NativeStructure.IP_INTERFACE_INFO.FromByteArray(buffer);
            return info;
        }
        public void getInterfaceIndex()
        {
            NativeStructure.IP_ADAPTER_INDEX_MAP[] temp = this.getInterfaceInfo().adapter;
            foreach(NativeStructure.IP_ADAPTER_INDEX_MAP i in temp)
            {
                foreach( Adapter j in this.data)
                {
                    String name = i.Name.Remove(0,14);
                    if (name.Equals(j.Name))
                    {
                        j.Index = i.Index;
                    }
                }
            }
        }
        public void run()
        {
            this.getAdaptersList();
            this.getInterfaceIndex();
        }
    }
}

