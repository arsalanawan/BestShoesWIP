namespace BestPowerShoesEntities
{
    public class Plan
    {
        public int PlanId { get; set; }
        public int PlanNo { get; set; }
        public PlanProcess PlanProcessObj { get; set; }
        public Article ArticleObj { get; set; }
        public Party PartyObj { get; set; }
        public Color ColorObj { get; set; }
        public Statuses StatusesObj { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
