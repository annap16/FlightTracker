using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public interface IReportable
    {
        public string Accept(Visitor visitor);
    }
}
