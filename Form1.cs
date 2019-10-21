using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACSNM
{
    public partial class Form1 : Form
    {
        GetIPForwardTable IPForwardTable;
        GetAdapterList adapterListHelper;
        List<Route> routeList;
        List<Adapter> adapterList;
        public Form1()
        {
            InitializeComponent();
            IPForwardTable = new GetIPForwardTable();
            adapterListHelper = new GetAdapterList();
            adapterListHelper.run();
            IPForwardTable.run();
            IPForwardTable.getInterfaceIP();
            routeList = IPForwardTable.getRoutes();
            adapterList = adapterListHelper.Data;
        }

        private void RoutePrintButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = routeList;
        }
        private void adaptersListButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = adapterList;
        }
    }
}
