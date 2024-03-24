using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class IfFinishedTask
    {
        public bool finished;
        public IfFinishedTask()
        {
            finished = false;
        }
    }

    public static class ConsoleDataHandling
    {
        public static string CreateFileName()
        {
            DateTime now = DateTime.Now;
            return $"snapshot_{now.Hour:D2}_{now.Minute:D2}_{now.Second:D2}.json";
        }
        
        public static AllLists ChooseDataSource(string filePath, string FTRNameJson, 
            NetworkSourceSimulator.NetworkSourceSimulator networkSource)
        {
            AllLists lists = new AllLists();

            Console.WriteLine("Choose data source: FTR or TCP");
            string? input = Console.ReadLine();
            
            if (input == "FTR")
            {

                FileReaderFTR fileReaderFTR = new FileReaderFTR();
                List<DataType>? listFTR = fileReaderFTR.ReadFile(filePath, lists);
                SerializationJSON serializationJSON = new SerializationJSON();
                if (serializationJSON != null)
                {
                    serializationJSON.Serialize(listFTR, FTRNameJson);
                }

            }
            else if (input == "TCP")
            {
                Console.WriteLine("Downloading data...");
                OnNewDataReadyClass delegateClass = new OnNewDataReadyClass();
                IfFinishedTask ifFinished = new IfFinishedTask();
                networkSource.OnNewDataReady += delegateClass.OnNewDataReadyDelegate;
                Task instanceCaller = new Task(() => { networkSource.Run(); ifFinished.finished = true; });
                instanceCaller.Start();
                WaitForInput(delegateClass, true, ifFinished);
                lists = delegateClass.lists;
            }
            else
            {
                throw new Exception("Invalid option");
            }

            return lists;
        }
        public static void WaitForInput(OnNewDataReadyClass delegateClass, bool delete, IfFinishedTask ifFinished)
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
            while (!ifFinished.finished)
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
            Console.WriteLine("Downloading data completed");
            return;
        }
    }
}
