using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
   
    public class Television: Visitor 
    {
        public string name;
        public Television(string _name)
        {
            this.name = _name;
        }

        public override string Visit(Airport airport)
        {
            return "An image of " + airport.name + " airport";
        }

        public override string Visit(CargoPlane cargoPlane)
        {
            return"An image of " + cargoPlane.model + " cargo plane";
        }

        public override string Visit(PassengerPlane passengerPlane)
        {
            return"An image of " + passengerPlane.model + " passenger plane";
        }

       
    }

    public class Radio :Visitor
    {
        public string name;

        public Radio(string _name)
        {
            this.name = _name;
        }

        public override string Visit(Airport airport)
        {
            return"Reporting for " + name + ", Ladies and gentelman, we are at the " + airport.name + " airport.";
        }

        public override string Visit(CargoPlane cargoPlane)
        {
            return"Reporting for " + name + ", Ladies and gentelman, we are seeing the " + cargoPlane.serial +
                " aircraft fly above us.";
        }

        public override string Visit(PassengerPlane passengerPlane)
        {
            return"Reporting for " + name + ", Ladies and gentelman, we've just witnessed " + passengerPlane.serial +
                " take off.";
        }
        
    }
    public class Newspapper : Visitor
    {
        public string name;

        public Newspapper(string _name) 
        {
            this.name = _name;
        }
        public override string Visit(Airport airport)
        {
            return name + " - A report from the " + airport.name + " airport " + airport.country;
        }

        public override string Visit(CargoPlane cargoPlane)
        {
            return name + " - An interview with the crew of " + cargoPlane.serial;
        }

        public override string Visit(PassengerPlane passengerPlane)
        {
            return name + " - Breaking news! " + passengerPlane.model +
                " aircraft loses EASA fails certification after inspection of " + passengerPlane.serial;
        }
        
    }
}
