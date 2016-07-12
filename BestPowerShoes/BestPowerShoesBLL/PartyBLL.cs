using BestPowerShoesDAL;
using BestPowerShoesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPowerShoesBLL
{
    public class PartyBLL
    {
        //private static readonly ILog Logger = LogManager.GetLogger(typeof(ArticleBLL));
        public List<Party> GetParties() {
            try
            {
                var dsParty = new PartyDAL().GetParties();
                int count = dsParty.Tables[0].Rows.Count;
                if (count>0)
                {
                    var lstparties = new List<Party>();
                    for (int i = 0; i < count; i++)
                    {
                        var temp = new Party
                        {
                            PartyId = Convert.ToInt32(dsParty.Tables[0].Rows[i]["PartyID"]),
                            PartyName = dsParty.Tables[0].Rows[i]["PartyName"].ToString(),
                            CategoryObj = new Category { CategoryName = dsParty.Tables[0].Rows[i]["ProcessName"].ToString() },
                             Mobile=dsParty.Tables[0].Rows[i]["CellNo"].ToString(),
                            Address = dsParty.Tables[0].Rows[i]["PartyAddress"].ToString()
                        };
                        lstparties.Add(temp);
                    }
                    return lstparties;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Logger.Debug("Method in Context: SaveArticle() " + ex.Message);
               
            }
            return null;
        }

        public object SaveParty(Party p)
        {
            try
            {
                return new PartyDAL().SaveParty(p);
            }
            catch (Exception ex)
            {

                // Logger.Debug("Method in Context: SaveArticle() " + ex.Message);
            }

            return null;

        }
    }
}
