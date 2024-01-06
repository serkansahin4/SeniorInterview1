using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LoggerWindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Interval = 60000;
            timer.Elapsed += new ElapsedEventHandler(WriteLog);
            timer.Enabled = true;
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void WriteLog(object sender, ElapsedEventArgs e)
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string folderPath = Path.Combine(rootPath, "VeriketApp");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string logFilePath = Path.Combine(folderPath, "VeriketAppTest.txt");
            if (!File.Exists(logFilePath))
                File.Create(logFilePath);
            
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine(GetLogTemplate());
                writer.Close();
            }
        }

        private string GetLogTemplate()
        {
            var template = new StringBuilder();

            template.Append(DateTime.Now);
            template.Append(", ");
            template.Append(System.Environment.MachineName);
            template.Append(", ");
            template.Append(System.Environment.UserName);

            return template.ToString();
        }
    }
}
