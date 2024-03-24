using System.Collections.Generic;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Xml;
using Avalonia.Rendering;
using FlightTrackerGUI;

namespace OOD_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "./../../../example_data.ftr";
            string FTRNameJson = "AllDataFTR.json";

            NetworkSourceSimulator.NetworkSourceSimulator networkSource =
                new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 10, 15);

            AllLists lists = ConsoleDataHandling.ChooseDataSource(filePath, FTRNameJson, networkSource);
           
            Task runApp = new Task(() => { Runner.Run(); });
            runApp.Start();
            
            Task refreshApp = new Task(() => {
                Timer timerObject = new Timer(lists); 
                Console.WriteLine("Press the Enter key to exit the application...\n");
                Console.ReadLine();
                timerObject.timer.Stop();
                timerObject.timer.Dispose();
            });
            refreshApp.Start();

            refreshApp.Wait();
        }
    }
}