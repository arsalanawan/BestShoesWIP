using BestPowerShoesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPowerShoesCommon
{
    public class PlanVM
    {
        public List<Plan> PlanObj { get; set; }
        public List<Statuses> Statuses { get; set; }
        public List<Process> Processes { get; set; }
    }
}
