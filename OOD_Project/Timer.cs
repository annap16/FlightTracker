using FlightTrackerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OOD_Project
{
    public class Timer
    {
        public System.Timers.Timer timer;
        public AllLists lists;

        public Timer(AllLists _lists)
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            lists = _lists;
        }
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            FlightConvertion flightConvertion = new FlightConvertion(lists.flightList, lists.airportList);
            FlightsGUIData flightsGUIData = new FlightsGUIData(flightConvertion.flightGUIList);
            Runner.UpdateGUI(flightsGUIData);
        }

    }


}
