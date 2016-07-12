using BestPowerShoesDAL;
using BestPowerShoesEntities;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using BestPowerShoesCommon;
using log4net;

namespace BestPowerShoesBLL
{
    public class PlanBLL
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlanBLL));
        public PlanVM GetPlans(Plan plan)
        {
            try
            {
                var PlanVm = new PlanVM();
                var dsPlans = new PlanDAL().GetPlans(plan);
                int count = dsPlans.Tables[0].Rows.Count;
                var plans = new List<Plan>();
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var v = new Plan
                        {
                            PlanId = Convert.ToInt32(dsPlans.Tables[0].Rows[i]["PlanID"]),
                            PlanProcessObj = new PlanProcess
                            {
                            StartDate = Convert.ToDateTime(dsPlans.Tables[0].Rows[i]["StartDate"]),
                            Day = dsPlans.Tables[0].Rows[i]["DName"].ToString(),
                            },
                            ArticleObj = new Article
                            {
                                ArticleNo = Convert.ToInt32(dsPlans.Tables[0].Rows[i]["ArticleNo"]),
                                ArticleName = dsPlans.Tables[0].Rows[i]["ArticleName"].ToString(),
                                Size=dsPlans.Tables[0].Rows[i]["ArticleSize"].ToString()
                            },
                            PartyObj = new Party
                            {
                                PartyName = dsPlans.Tables[0].Rows[i]["PartyName"].ToString()
                            },
                            ColorObj = new Color
                            {
                                //ColorId = Convert.ToInt16(dsPlans.Tables[0].Rows[i]["ColorID"]),
                                ColorName = dsPlans.Tables[0].Rows[i]["ColorName"].ToString(),
                            },
                            StatusesObj = new Statuses
                            {
                                StatusDesc = dsPlans.Tables[0].Rows[i]["PlanProcessStatus"].ToString()
                            }
                            //v.IsDeleted = (bool)dsPlans.Tables[0].Rows[i]["IsDeleted"];  
                        };
                        plans.Add(v);
                    }
                   
                }
                var dsMetaInfo = new PlanDAL().GetPlanMetaInfo();

                //var lstStatuses = dsMetaInfo.Tables[0].AsEnumerable().Select(x => new Statuses
                //{
                //    StatusId = (int)x.Field<byte>("StatusID"),
                //    StatusDesc = x.Field<string>("StatusDesc")
                //}).ToList();

                // *********** Without Database************
                var lstStatuses = dsMetaInfo.Tables[0].Rows.Cast<object>().Select((t, i) => new Statuses
                {
                    StatusId = Convert.ToInt32(dsMetaInfo.Tables[0].Rows[i]["StatusID"]),
                    StatusDesc = dsMetaInfo.Tables[0].Rows[i]["StatusDesc"].ToString()
                }).ToList();

                //List<Process> lstProcess = dsMetaInfo.Tables[1].AsEnumerable().Select(x => new Process
                //{
                //    ProcessId = (int)x.Field<byte>("ProcessID"),
                //    ProcessName = x.Field<string>("ProcessName")
                //}).ToList();

                // *********** Without Database************
                var lstProcess = dsMetaInfo.Tables[1].Rows.Cast<object>().Select((t, i) => new Process
                {
                    ProcessId = Convert.ToInt32(dsMetaInfo.Tables[1].Rows[i]["ProcessID"]),
                    ProcessName = dsMetaInfo.Tables[1].Rows[i]["ProcessName"].ToString()
                }).ToList();

                PlanVm.PlanObj = plans;
                PlanVm.Statuses = lstStatuses;
                PlanVm.Processes = lstProcess;
                return PlanVm;
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetPlans() " + ex.Message);
            }
            return null;
        }
        public Article GetArticle(int articleNo)
        {
            try
            {
                var dsArticle = new PlanDAL().GetArticle(articleNo);
                int count = dsArticle.Tables[0].Rows.Count;
                if (count > 0)
                {
                    var article = new Article
                    {
                        ArticleId = Convert.ToInt32(dsArticle.Tables[0].Rows[0]["ArticleID"]),
                        ArticleNo = Convert.ToInt32(dsArticle.Tables[0].Rows[0]["ArticleNo"]),
                        ArticleName = dsArticle.Tables[0].Rows[0]["ArticleName"].ToString(),
                        Size=dsArticle.Tables[0].Rows[0]["ArticleSize"].ToString()
                    };
                    return article;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetArticle() " + ex.Message);
            }
            return null;
        }
        public ColorVM GetColorsAndPlanNO()
        {
            try
            {
                var colorVm = new ColorVM();
                var dsColors = new PlanDAL().GetColorsAndPlanNo();
                int count = dsColors.Tables[0].Rows.Count;
                int count1 = dsColors.Tables[1].Rows.Count;
                var colors = new List<Color>();
                if (count > 0)
                {
                    colorVm.PlanId = Convert.ToInt32(dsColors.Tables[0].Rows[0]["PlanID"]);
                }
                if (count1 > 0)
                {
                    for (int i = 0; i < count1; i++)
                    {
                        var color = new Color
                        {
                            ColorId = Convert.ToInt16(dsColors.Tables[1].Rows[i]["ColorID"]),
                            ColorName = dsColors.Tables[1].Rows[i]["ColorName"].ToString()
                        };
                        colors.Add(color);
                    }
                }
                colorVm.Colors = colors;
                return colorVm;
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetPlans() " + ex.Message);
            }
            return null;
        }
        public bool AddPlan(Plan plan)
        {
            try
            {
                return new PlanDAL().AddPlan(plan);
            }
            catch (Exception ex)
            {
                Logger.Debug("Method in Context: GetPlans() " + ex.Message);
            }
            return false;
        }
    }
}
