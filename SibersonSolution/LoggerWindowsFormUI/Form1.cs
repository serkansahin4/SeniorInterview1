using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoggerWindowsFormUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string logFilePath = Path.Combine(rootPath, "VeriketApp", "VeriketAppTest.txt");

            DataTable dataTable = new DataTable();
            using (StreamReader reader = new StreamReader(logFilePath))
            {
                DataColumn[] dataColumns= {
                new DataColumn("Date"),
                new DataColumn("ComputerName"),
                new DataColumn("UserName")
                };

                dataTable.Columns.AddRange(dataColumns);
                while (!reader.EndOfStream)
                {
                    string[] rows = reader.ReadLine().Split(',');
                    dataTable.Rows.Add(rows);
                }
            }

            dataGridView1.DataSource = dataTable;
        }
    }
}
