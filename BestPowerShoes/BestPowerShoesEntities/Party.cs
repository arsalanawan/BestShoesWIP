using System.Security.AccessControl;

namespace BestPowerShoesEntities
{
    public class Party
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public Category CategoryObj { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
