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
    public class ArticleDAL : SQLDatabase.Execute
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ArticleDAL));

        public DataSet GetArticles()
        {
         
            var command = new SqlCommand();
            try
            {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getAllArticles";
                return ExecuteDataset(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetArticles() " + ex.Message);
            }
            return null;
        }

        public object SaveArticle(Article art, string xml)
        {
            var command = new SqlCommand();
            try
            {                
                
                command.CommandText = "saveAndUpdateArticle";
                if (art.ArticleId != 0)
                {
                    command.Parameters.AddWithValue("@ArticleID", art.ArticleId);
                }
                command.Parameters.AddWithValue("@ArticleNo", art.ArticleNo);
                command.Parameters.AddWithValue("@ArticleName", art.ArticleName);
                command.Parameters.AddWithValue("@ArticleSize", art.Size);
                command.Parameters.AddWithValue("@ArticleXML", xml);
                return ExecuteScalar(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: SaveArticle() " + ex.Message);
            }
            return null;
        
        }

        public DataSet GetArticleProfile(Article art) 
        {
            var command = new SqlCommand();
            try
            {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getArticleProfile";
                command.Parameters.AddWithValue("@ArticleID", art.ArticleId);
                return ExecuteDataset(command);

            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetArticleProfile() " + ex.Message);
            }
            return null;
        }
    }

    }

