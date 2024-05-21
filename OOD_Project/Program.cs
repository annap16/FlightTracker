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
            Logger.NewLog();

            string filePath = "./../../../example_data.ftr";
            string filePathUpdate = "./../../../example.ftre";
            string FTRNameJson = "AllDataFTR.json";

            NetworkSourceSimulator.NetworkSourceSimulator networkSource =
                new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 10, 15);
            NetworkSourceSimulator.NetworkSourceSimulator networkSourceUpdates =
                new NetworkSourceSimulator.NetworkSourceSimulator(filePathUpdate, 100, 500);

            Publisher publisher = new Publisher();
            OnNewDataReadyClass delegateClass = new OnNewDataReadyClass();
            delegateClass.publisher = publisher;

            (AllLists lists, bool type) = ConsoleDataHandling.ChooseDataSource(filePath, FTRNameJson, networkSource, delegateClass, publisher);
            publisher.flightList = lists.flightList;
            AncillaryFunctions.SetStructures(lists.flightList, lists);
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

                ConsoleDataHandling.WaitForInput(delegateClass, true, ifFinished2, newsGenerator, lists);

                timerObject.timer.Stop();
                timerObject.timer.Dispose();
            });
            refreshApp.Start();

            IfFinishedTask ifFinishedUpdate = new IfFinishedTask();
            networkSourceUpdates.OnIDUpdate += publisher.NotifyIDChanged;
            networkSourceUpdates.OnPositionUpdate += publisher.NotifyPositionChanged;
            networkSourceUpdates.OnContactInfoUpdate += publisher.NotifyContactInfoChanged;
            Task instanceCallerUpdates = new Task(() => { networkSourceUpdates.Run(); ifFinishedUpdate.finished = true; });
            instanceCallerUpdates.Start();

            refreshApp.Wait();
        }
    }
}