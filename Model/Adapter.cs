using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACSNM
{
    class Adapter
    {
        private int index;
        private String address;
        private String description;
        private String name;


        public String Name { get => name; set => name=value; }
        public String Address { get => address; set => address = value; }
        public String Description { get => description; set => description = value; }
        public int Index { get => index; set => index = value; }

        public static string ByteArrayToString(byte[] bts)
        {
            return BitConverter.ToString(bts).Replace("-", " - ");
        }
    }
}
