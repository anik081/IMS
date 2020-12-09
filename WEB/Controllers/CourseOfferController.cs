using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QtImsDAL;
using QtImsEntity;
using System.Text;
using System.Transactions;

namespace WEB.Controllers
{
    public class CourseOfferController : Controller
    {
        // GET: CourseOffer
        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_Course.Get();
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDynamic(string where, string orderBy)
        {
            try
            {

                var list = Facade.TRN_CourseOffer.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public string Post(List<TRN_CourseOffer> lstCourseOffer, List<TRN_CourseOfferSchedule> lstCourseOfferSchedule, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                foreach (TRN_CourseOffer item in lstCourseOffer)
                {
                    item.UpdateBy = 1;
                    item.UpdateDate = DateTime.Now;
                    ret = Facade.TRN_CourseOffer.Post(item, transactionType);

                    if (!string.IsNullOrEmpty(ret) && ret.Contains("successfully"))
                    {
                        Int64 courseOfferId = 0;
                        courseOfferId = item.CourseOfferId > 0 ? item.CourseOfferId : Convert.ToInt64(ret.Split(':')[1]);

                        List<TRN_CourseOfferSchedule> lstCourseOfferCourseSchedule = lstCourseOfferSchedule.Where(s => s.CourseId == item.CourseId).ToList();
                        foreach (TRN_CourseOfferSchedule schedule in lstCourseOfferCourseSchedule)
                        {
                            schedule.CourseOfferId = courseOfferId;
                            Facade.TRN_CourseOfferSchedule.Post(schedule,"INSERT");

                        }
                    }
                }

                

                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    
    }
}
