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


            OnNewDataReadyClass delegateClass = new OnNewDataReadyClass();
           ( AllLists lists, bool type ) = ConsoleDataHandling.ChooseDataSource(filePath, FTRNameJson, networkSource, delegateClass);

            List<Visitor> visitors =
                new List<Visitor>()
            {
                new Television("Telewizja Abelowa"),
                new Television("Kanal TV-sensor"),
                new Radio("Radio Kwantyfikator"),
                new Radio("Radio Shmem"),
                new Newspapper("Gazeta Kategoryczna"),
                new Newspapper("Dziennik Politechniczny")
            };
            List<IReportable> reportedObj = new List<IReportable>();
            reportedObj.AddRange(lists.airportList);
            reportedObj.AddRange(lists.cargoPlaneList);
            reportedObj.AddRange(lists.passengerPlaneList);
            NewsGenerator newsGenerator = new NewsGenerator(visitors, reportedObj);

            Task runApp = new Task(() => { Runner.Run(); });
            runApp.Start();
            
            Task refreshApp = new Task(() => {
                Timer timerObject = new Timer(lists);
                
                IfFinishedTask ifFinished2 = new IfFinishedTask();

                ConsoleDataHandling.WaitForInput(delegateClass, true, ifFinished2, newsGenerator);

                timerObject.timer.Stop();
                timerObject.timer.Dispose();
            });
            refreshApp.Start();

            refreshApp.Wait();
        }
    }
}