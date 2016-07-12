using BestPowerShoesDAL.Base;
using BestPowerShoesEntities;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BestPowerShoesDAL
{
    public class PartyDAL : SQLDatabase.Execute
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PartyDAL));
        public DataSet GetParties()
        {
            var command = new SqlCommand();
            try
            {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getAllParties";
                return ExecuteDataset(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetParties() " + ex.Message);
            }
            return null;
        }

        public object SaveParty(Party p)
        {
            var command = new SqlCommand();
            try
            {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "saveAndUpdateParty";
               // command.Parameters.AddWithValue("@PartyID",p.PartyId);
                command.Parameters.AddWithValue("@PartyName",p.PartyName);
                command.Parameters.AddWithValue("@ProcessID",p.CategoryObj.CategoryId);
                command.Parameters.AddWithValue("@CellNo", p.Mobile);
                command.Parameters.AddWithValue("@PartyAddress",p.Address);               
                return ExecuteScalar(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: SaveParty() " + ex.Message);
            }
            return null;
        }
        public DataSet GetPartyMetaInfo()
        {

            var command = new SqlCommand();
            try
            {
                
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPlanMetaInfo";
                return ExecuteDataset(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetPartyMetaInfo() " + ex.Message);
            }
            return null;
        }

    }
}
