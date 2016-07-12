using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPowerShoesEntities
{
    public class Process
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int InRate { get; set; }
        public int OutRate { get; set; }
    }
}
