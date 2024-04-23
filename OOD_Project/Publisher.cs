using Avalonia.Controls.Primitives;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Publisher
    {
        public Dictionary<UInt64,DataType> subs;
        public List<Flight>? flightList;
        public Publisher()
        {
            subs = new Dictionary<UInt64, DataType>();
        }

        public void Subscribe(DataType sub)
        {
            if(subs.ContainsKey(sub.ID))
            {
                Console.WriteLine("Unable to add " + sub.ID.ToString() + " to subscribers beacause it is already a subscriber");
            }
            else
                subs.Add(sub.ID, sub);
        }

        public void Unsubscribe(DataType sub)
        {
            if(subs.ContainsKey(sub.ID))
            {
                subs.Remove(sub.ID);
            }
            else
            {
                Console.WriteLine("Unable to unsubscribe " + sub.ID.ToString() + " because object is not a subscriber");
            }
        }

        public void Unsubscribe(UInt64 subID)
        {
            if (subs.ContainsKey(subID))
            {
                subs.Remove(subID);
            }
            else
            {
                Console.WriteLine("Unable to unsubscribe " + subID.ToString() + " because object is not a subscriber");
            }
        }

        public void NotifyIDChanged(object sender, IDUpdateArgs args)
        {
            if (subs.ContainsKey(args.ObjectID) && !subs.ContainsKey(args.NewObjectID))
            {
                DataType obj = subs[args.ObjectID];
                List<int> list = new List<int>();
                obj.Update(args, flightList);
                Logger.NewLog("Object's ID: " + args.ObjectID.ToString() + " changed to ID: " + args.NewObjectID.ToString());
                subs.Remove(args.ObjectID);
                subs.Add(obj.ID, obj);
            }
            else if (!subs.ContainsKey(args.ObjectID))
                Logger.NewLog("Can not update ID, object with given ID: " + args.ObjectID.ToString() + " because it does not exist");
            else
                Logger.NewLog("Unable to change objects ID: " + args.ObjectID.ToString() + " to new ID: " + args.NewObjectID.ToString() +
                    " because new ID is not valid");
        }

        public void NotifyPositionChanged(object sender, PositionUpdateArgs args)
        {
            if (subs.ContainsKey(args.ObjectID))
            {
                
                subs[args.ObjectID].Update(args);
                Flight pom = subs[args.ObjectID] as Flight;
                Logger.NewLog("Object's (" + args.ObjectID.ToString() + ") position changed: lognitude=" + args.Longitude.ToString() +
                    ", latitude=" + args.Latitude.ToString() + ", AMSL=" + args.AMSL.ToString());
            }
            else
                Logger.NewLog("Can not update position for object with given ID: " + args.ObjectID.ToString() + " because it does not exist");

        }

        public void NotifyContactInfoChanged(object sender, ContactInfoUpdateArgs args)
        {
            if (subs.ContainsKey(args.ObjectID))
            {
                subs[args.ObjectID].Update(args);
                Logger.NewLog("Object's (" + args.ObjectID.ToString() + ") contact info changed: email" + args.EmailAddress +
                   ", phone=" + args.PhoneNumber);
            }
            else
               Logger.NewLog("Can not update contact info for object with given ID: " + args.ObjectID.ToString() + " because it does not exist");

        }

    }


}
