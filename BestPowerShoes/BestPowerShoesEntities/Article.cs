using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPowerShoesEntities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int ArticleNo { get; set; }
        public string ArticleName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
