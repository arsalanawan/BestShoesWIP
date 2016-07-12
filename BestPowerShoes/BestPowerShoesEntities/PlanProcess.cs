using System;

namespace BestPowerShoesEntities
{
    public class PlanProcess
    {
        public int PlanProcessId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Day { get; set; }
    }
}
