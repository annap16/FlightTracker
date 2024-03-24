using NetworkSourceSimulator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class OnNewDataReadyClass
    {
        public List<DataType> objectsList;
        public AllLists lists;
        public Mutex mut;
        public OnNewDataReadyClass() 
        {
            objectsList = new List<DataType>();
            mut = new Mutex();
            lists = new AllLists();
        }
        public void OnNewDataReadyDelegate(object sender, NetworkSourceSimulator.NewDataReadyArgs args)
        {
            int index = args.MessageIndex;
            NetworkSourceSimulator.NetworkSourceSimulator? mySender = sender as NetworkSourceSimulator.NetworkSourceSimulator;
            if (mySender == null) 
            {
                throw new Exception("sender is null");
            }
            NetworkSourceSimulator.Message message = mySender.GetMessageAt(index);
            byte [] tabBinaryMess = message.MessageBytes;
            FileReaderBinary fileReaderBinary = new FileReaderBinary();
            objectsList.Add(fileReaderBinary.ReadData(tabBinaryMess, lists));       
        }
    }
}
