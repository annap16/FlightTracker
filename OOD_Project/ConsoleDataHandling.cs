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
        
        public static (AllLists, bool) ChooseDataSource(string filePath, string FTRNameJson, 
            NetworkSourceSimulator.NetworkSourceSimulator networkSource, OnNewDataReadyClass delegateClass,
            Publisher publisher)
        {
            AllLists lists = new AllLists();

            Console.WriteLine("Choose data source: FTR or TCP");
            string? input = Console.ReadLine();
            
            bool type;
            
            if (input == "FTR")
            {

                FileReaderFTR fileReaderFTR = new FileReaderFTR();
                List<DataType>? listFTR = fileReaderFTR.ReadFile(filePath, lists, publisher);
                SerializationJSON serializationJSON = new SerializationJSON();
                if (serializationJSON != null)
                {
                    serializationJSON.Serialize(listFTR, FTRNameJson);
                }
                delegateClass.objectsList = listFTR;

                type = true;

            }
            else if (input == "TCP")
            {
                Console.WriteLine("Downloading data...");
                IfFinishedTask ifFinished = new IfFinishedTask();
                networkSource.OnNewDataReady += delegateClass.OnNewDataReadyDelegate;
                Task instanceCaller = new Task(() => { networkSource.Run(); ifFinished.finished = true; });
                instanceCaller.Start();
                WaitForInput(delegateClass, true, ifFinished, null, null);
                lists = delegateClass.lists;
                type = false;
            }
            else
            {
                throw new Exception("Invalid option");
            }

            return (lists, type);
        }
        public static void WaitForInput(OnNewDataReadyClass delegateClass, bool delete, IfFinishedTask ifFinished, NewsGenerator newsGenerator, AllLists lists)
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
                    else if(input=="report")
                    {
                        string news;
                        while((news = newsGenerator.GenerateNextNews())!=null)
                        {
                            Console.WriteLine(news);
                        }
                    }
                    else
                    { 
                        Chain chainDisplay = new ChainDisplay();
                        Chain chainUpdate = new ChainUpadate();
                        Chain chainDelete = new ChainDelete();
                        Chain chainAdd = new ChainAdd();
                        chainDisplay.SetNextChain(chainUpdate);
                        chainUpdate.SetNextChain(chainDelete);
                        chainDelete.SetNextChain(chainAdd);

                        chainDisplay.ParseAndTransfer(input, lists);
                    }
                }
            }
            Console.WriteLine("Downloading data completed");
            return;
        }
    }
}
