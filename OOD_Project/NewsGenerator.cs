using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class NewsGenerator
    {
        public List<Visitor> mediaList;
        public List<IReportable> reportedList;
        private int currentMediaNum, currentObjectNum;
        public NewsGenerator(List<Visitor> mediaList, List<IReportable> reportedList)
        {
            this.mediaList = mediaList;
            this.reportedList = reportedList;
            this.currentObjectNum = 0;
            this.currentMediaNum = 0;
        }

        public string? GenerateNextNews()
        {
            if (currentObjectNum>=reportedList.Count)
            {
                currentObjectNum = 0;
                currentMediaNum++;
                if (currentMediaNum >= mediaList.Count)
                {
                    currentMediaNum = 0;
                    return null;
                }
            }
            
            return reportedList[currentObjectNum++].Accept(mediaList[currentMediaNum]);
        }
    }
}
