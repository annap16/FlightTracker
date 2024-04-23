using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public static class Logger
    {
        public static void NewLog(string mess = "\nStarting new application...")
        {
           
            DateTime now = DateTime.Now;
            string name = $"log_{now.Day:D2}.{now.Month:D2}.{now.Year:D2}.txt";
            string currentDirectory = Directory.GetCurrentDirectory();
            string logsDirectory = Path.Combine(currentDirectory, "Logs");
            string filePath = Path.Combine(logsDirectory, name);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(mess);
            sw.Close();
        }


    }
}
