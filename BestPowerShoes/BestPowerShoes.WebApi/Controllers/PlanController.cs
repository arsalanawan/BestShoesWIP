using BestPowerShoesBLL;
using BestPowerShoesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BestPowerShoes.WebApi.Controllers
{
    public class PlanController : ApiController
    {
        //private static readonly ILog Logger = LogManager.GetLogger(typeof(PlanController));
        [HttpPost]
        public IHttpActionResult GetPlans(Plan plan)
        {
            try
            {
                var plans = new PlanBLL().GetPlans(plan);
                if (plans != null)
                {
                    return Ok(plans);
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        public IHttpActionResult GetArticle(int articleNo)
        {
            try
            {
                var article = new PlanBLL().GetArticle(articleNo);
                if (article != null)
                {
                    return Ok(article);
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        public IHttpActionResult GetColorsAndPlanNO()
        {
            try
            {
                var colorsAndPlan = new PlanBLL().GetColorsAndPlanNO();
                if (colorsAndPlan != null)
                {
                    return Ok(colorsAndPlan);
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult AddPlan(Plan plan)
        {
            try
            {
                var addPlan = new PlanBLL().AddPlan(plan);
                if (addPlan == true)
                {
                    return Ok(addPlan);
                }
            }
            catch (Exception ex)
            {
            }
            return BadRequest();
        }
    }
}