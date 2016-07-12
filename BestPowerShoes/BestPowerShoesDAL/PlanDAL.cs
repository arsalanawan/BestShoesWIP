using BestPowerShoesDAL.Base;
using BestPowerShoesEntities;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestPowerShoesDAL
{
    public class PlanDAL : SQLDatabase.Execute
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlanDAL));
        public DataSet GetPlans(Plan plan)
        {
            var command = new SqlCommand();
            try
            {
                // Create two DataTable instances.
                //DataTable table1 = new DataTable("Plans");
                //table1.Columns.Add("PlanID");
                //table1.Columns.Add("StartDate");
                //table1.Columns.Add("DName");
                //table1.Columns.Add("ArticleNo");
                //table1.Columns.Add("ArticleName");
                //table1.Columns.Add("PartyName");
                //table1.Columns.Add("ColorName");
                //table1.Columns.Add("Quantity");
                //table1.Columns.Add("ArticleSize");
                //table1.Columns.Add("PlanProcessStatus");
                //table1.Rows.Add(1, "12/12/1991", "Monday", 275, "Sandle", "Ali", "Blue", 100, "7/10", "InProgress");

                ////Create a DataSet and put both tables in it.
                //DataSet set = new DataSet("office");
                //set.Tables.Add(table1);
                //return set;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPlans";
                command.Parameters.AddWithValue("@PlanStatusID", plan.StatusesObj.StatusId);
                command.Parameters.AddWithValue("@ProcessID", plan.PlanProcessObj.PlanProcessId);
                command.Parameters.AddWithValue("@StartDate", plan.PlanProcessObj.StartDate);
                command.Parameters.AddWithValue("@EndDate", plan.PlanProcessObj.EndDate);
                return ExecuteDataset(command);
                
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetPlans() " + ex.Message);
            }
            return null;
        }
        public DataSet GetPlanMetaInfo()
        {

            var command = new SqlCommand();
            try
            {
                //DataTable table2 = new DataTable("Statuses");
                //table2.Columns.Add("StatusID");
                //table2.Columns.Add("StatusDesc");
                //table2.Rows.Add(1, "InProgress");
                //table2.Rows.Add(2, "Completed");

                //DataTable table3 = new DataTable("Processes");
                //table3.Columns.Add("ProcessID");
                //table3.Columns.Add("ProcessName");
                //table3.Rows.Add(1, "Cutting");
                //table3.Rows.Add(2, "Stiching");

                //DataSet set = new DataSet("PlanMetaInfo");
                //set.Tables.Add(table2);
                //set.Tables.Add(table3);
                //return set;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPlanMetaInfo";
                return ExecuteDataset(command);
                
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetPlanMetaInfo() " + ex.Message);
            }
            return null;
        }
        public DataSet GetArticle(int articleNo)
        {
            var command = new SqlCommand();
            try
            {
                // Create two DataTable instances.
                //DataTable table1 = new DataTable("Article");
                //table1.Columns.Add("ArticleID");
                //table1.Columns.Add("ArticleNo");
                //table1.Columns.Add("ArticleName");
                //table1.Columns.Add("ArticleSize");
                //table1.Columns.Add("Quantity");
                
                //table1.Rows.Add(1, 572, "Sandle", "7/10", 100);

                ////Create a DataSet and put both tables in it.
                //DataSet set = new DataSet("Articles");
                //set.Tables.Add(table1);
                //return set;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getArticleProfile";
                command.Parameters.AddWithValue("@ArticleID", articleNo);
                return ExecuteDataset(command);
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetArticle() " + ex.Message);
            }
            return null;
        }
        public DataSet GetColorsAndPlanNo()
        {
            var command = new SqlCommand();
            try
            {
                // Create two DataTable instances.
                //DataTable table1 = new DataTable("Color");
                //table1.Columns.Add("ColorID");
                //table1.Columns.Add("ColorName");

                //table1.Rows.Add(1, "Blue");

                //DataTable table2 = new DataTable("Plan");
                //table1.Columns.Add("PlanID");

                //table1.Rows.Add(10);

                ////Create a DataSet and put both tables in it.
                //DataSet set = new DataSet("Colors");
                //set.Tables.Add(table1);
                //set.Tables.Add(table2);
                //return set;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getSavePlanMetaInfo";
                //command.Parameters.AddWithValue("@TableName", "Contact");
                //command.Parameters.AddWithValue("@ID", contact.ContactId);
                return ExecuteDataset(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetColorsAndPlanNo() " + ex.Message);
            }
            return null;
        }
        public bool AddPlan(Plan plan)
        {
            var command = new SqlCommand();
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "savePlan";
                command.Parameters.AddWithValue("@ArticleID", plan.ArticleObj.ArticleId);
                command.Parameters.AddWithValue("@ColorID", plan.ColorObj.ColorId);
                command.Parameters.AddWithValue("@StartDate", plan.PlanProcessObj.StartDate);
                command.Parameters.AddWithValue("@Quantity", plan.Quantity);
                command.Parameters.AddWithValue("@IsDeleted", 0);
                ExecuteDataset(command);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Debug("" + ex.Message);
            }
            return false;
        }
    }
}
