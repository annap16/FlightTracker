using Avalonia.Controls.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class Visitor 
    {
        public abstract string Visit(Airport airport);
        public abstract string Visit(CargoPlane cargoPlane);

        public abstract string Visit(PassengerPlane passengerPlane);

    }
  

}

