using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Context<T> where T:IGetFields
    {
        public AllLists allLists;
        public bool[] elementsFlag;
        public List<T> list;
        public bool[] fieldsFlag;
        public string[] fieldsName;
        public List<string> whereQuery;
        public List<string> fieldsQuery;
        public List<(string, string, string)> setQuery;
        public List<string> newQuery;
        public int currIndx;
        public FindFieldOnName findFieldOnName;
        public Context(AllLists _allLists, List<T>_list)
        {
            allLists = _allLists;
            list = _list;
            elementsFlag = new bool[list.Count];
            fieldsName = T.GetFields();
            fieldsFlag = new bool[fieldsName.Length];
            whereQuery = new List<string>();
            fieldsQuery = new List<string>();
            setQuery = new List<(string, string, string)>();
            newQuery = new List<string>();
        }
    }


}
