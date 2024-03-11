using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Xml;

namespace OOD_Project
{
    public delegate void Action(string[] input); 
    class Program
    {
        
        static void Main(string[] args)
        {
            string filePath = "./../../../example_data.ftr";
            NetworkSourceSimulator.NetworkSourceSimulator networkSource =
                new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 10, 15);
            OnNewDataReadyClass delegateClass = new OnNewDataReadyClass();

            networkSource.OnNewDataReady += delegateClass.OnNewDataReadyDelegate;

            Task instanceCaller = new Task( () => { networkSource.Run();});

            instanceCaller.Start();

            ConsoleDataHandling consoleDataHandling = new ConsoleDataHandling();

            consoleDataHandling.WaitForInput(delegateClass, true);  
            
        }
    }
}