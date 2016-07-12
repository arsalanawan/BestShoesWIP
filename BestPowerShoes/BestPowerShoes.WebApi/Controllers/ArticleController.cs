using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BestPowerShoesBLL;
using System.Xml.Serialization;
using System.IO;
using BestPowerShoesEntities;

namespace BestPowerShoes.WebApi.Controllers
{
    public class ArticleController : ApiController
    {
       // private static readonly ILog Logger = LogManager.GetLogger(typeof(ArticleBLL));
        public IHttpActionResult GetArticles()
        {
            try
            {
                var articles = new ArticleBLL().GetArticles();
                if (articles!=null)
                {
                    return Ok(articles);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        public IHttpActionResult SaveArticle(ArticleRate artRate)
        {
            try
            {
                Article art = new Article();
                art.ArticleNo = artRate.ArticleObj.ArticleNo;
                art.ArticleName = artRate.ArticleObj.ArticleName;
                art.Size = artRate.ArticleObj.Size;
                art.ArticleId = artRate.ArticleObj.ArticleId;
                List<Process> lstprocess = new List<Process>();
                foreach (var item in artRate.ProcessObj)
                {
                    var temp = new Process { ProcessId=item.ProcessId, InRate=item.InRate, OutRate=item.OutRate };
                    lstprocess.Add(temp);

                }
                string xml = XMLSerializeData(lstprocess);                
                string Msg = null;
                var articles = new ArticleBLL().SaveArticle(art, xml);
                if (articles==null)
                {
                    Msg = "Warning: Error occur while excuting, Please Insert correct Information";
                }
                else
                {
                    Msg = "Conratulations: Party Added Successfully!";
                }

                return Ok(Msg);
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        public IHttpActionResult ArticleProfile(Article art) 
        {
            try
            {
                
                var articles = new ArticleBLL().GetArticleProfile(art);
                if (articles!=null)
                {
                    return Ok(articles);
                }
            }
            catch (Exception ex)
            {
             
                //Logger.Debug("Method in Context: ArticleProfile() " + ex.Message);
            }
            return BadRequest();
        }
        public static string XMLSerializeData(object objXMLSerializeInfo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(objXMLSerializeInfo.GetType());
            string xmlString;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, objXMLSerializeInfo);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }
    }
}
