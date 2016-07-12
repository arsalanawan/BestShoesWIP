using BestPowerShoesDAL;
using BestPowerShoesEntities;
using log4net;
using System;
using System.Collections.Generic;

namespace BestPowerShoesBLL
{
    public class ArticleBLL
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ArticleBLL));
        public List<Article> GetArticles()
        {
           
          try
            {
                
                var dsArticles = new ArticleDAL().GetArticles();
                int count = dsArticles.Tables[0].Rows.Count;
                if (count>0)
                {
                    var articleslst = new List<Article>();
                    for (int i = 0; i < count; i++)
			        {
                        var temp = new Article {
                            ArticleId = Convert.ToInt32(dsArticles.Tables[0].Rows[i]["ArticleID"]),
                            ArticleNo = Convert.ToInt32(dsArticles.Tables[0].Rows[i]["ArticleNo"]),
                            ArticleName = dsArticles.Tables[0].Rows[i]["ArticleName"].ToString(),
                            Size= dsArticles.Tables[0].Rows[i]["ArticleSize"].ToString()
                                };

                        articleslst.Add(temp);
			        }

                    return articleslst;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetArticles() " + ex.Message);
            }
            return null;                    
        }

        public object SaveArticle(Article art, string xml) {

            try
            {
                return new ArticleDAL().SaveArticle(art,xml);
            }
            catch (Exception ex)
            {
                
                Logger.Debug("Method in Context: SaveArticle() " + ex.Message);
            }
        return null;
        }

        public List<ArticleRate> GetArticleProfile(Article art)
        {

            try
            {

                var dsArticles = new ArticleDAL().GetArticleProfile(art);
                int count = dsArticles.Tables[0].Rows.Count;
                int count2 = dsArticles.Tables[1].Rows.Count;
                if (count > 0)
                {
                    var articleslst = new List<ArticleRate>();
                    for (int i = 0; i < count; i++)
                    {
                        List<Process> tempProcess = new List<Process>();
                       
                        for (int j = 0; j < count2; j++)
                        {
                           
                            tempProcess.Add(new Process
                            {

                                ProcessId = Convert.ToInt32(dsArticles.Tables[1].Rows[j]["ProcessID"]),
                                ProcessName =dsArticles.Tables[1].Rows[j]["ProcessName"].ToString(),
                                InRate = Convert.ToInt32(dsArticles.Tables[1].Rows[j]["InRate"]),
                                OutRate =Convert.ToInt32(dsArticles.Tables[1].Rows[j]["OutRate"]),

                            }); 
                             
                               
                                
                        }

                        var temp = new ArticleRate
                        {
                            ArticleObj = new Article
                            {
                                ArticleId = Convert.ToInt32(dsArticles.Tables[0].Rows[i]["ArticleID"]),
                                ArticleNo = Convert.ToInt32(dsArticles.Tables[0].Rows[i]["ArticleNo"]),
                                ArticleName = dsArticles.Tables[0].Rows[i]["ArticleName"].ToString(),
                                Size = dsArticles.Tables[0].Rows[i]["ArticleSize"].ToString()
                            },
                            ProcessObj = tempProcess
                           

                        };



                        articleslst.Add(temp);
                    }

                    return articleslst;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetArticles() " + ex.Message);
            }
            return null;
        }
    }
}
