using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class IsCompletedBoolWraper
    {
        public bool isCompleted { get; set; }
        public IsCompletedBoolWraper()
        {
            isCompleted = false;
        }
    }
    public class ConsoleDataHandling
    {
        public ConsoleDataHandling() { }

        public string CreateFileName()
        {
            DateTime now = DateTime.Now;
            return $"snapshot_{now.Hour:D2}_{now.Minute:D2}_{now.Second:D2}.json";
        }
        public void WaitForInput(OnNewDataReadyClass delegateClass, bool delete)
        {
            if(delete == true)
            {
                string folderPath = Directory.GetCurrentDirectory();
                string[] filesToDelete = Directory.GetFiles(folderPath, "snapshot*");
                foreach (string filePath in filesToDelete)
                {
                    File.Delete(filePath);
                }


            }
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    string? input = Console.ReadLine();
                    if (input == "print")
                    {
                        delegateClass.mut.WaitOne();
                        string jsonFileName = CreateFileName();
                        SerializationJSON serialization = new SerializationJSON();
                        serialization.Serialize(delegateClass.objectsList, jsonFileName);
                        delegateClass.mut.ReleaseMutex();
                    }
                    else if (input == "exit")
                    {  
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input!!! Write 'print' to download data " +
                            "or 'exit' to terminate the app");
                    }
                }
            }
           
        }
    }
}
