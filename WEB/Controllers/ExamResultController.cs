using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using QtImsDAL;
using QtImsEntity;

namespace WEB.Controllers
{
    public class ExamResultController : Controller
    {
        // GET: ExamResult
        public JsonResult Get()
        {
            try
            {
                var list = Facade.TRN_CourseMark.Get();
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

                var list = Facade.TRN_CourseMark.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(TRN_CourseMark obj, List<TRN_CourseMarkDetail> lst, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                obj.UpdateBy = 1;
                obj.UpdateDate = DateTime.Now;
                obj.MarkingDate = DateTime.Now;
                ret = Facade.TRN_CourseMark.Post(obj, transactionType);
                if (ret.Contains("successfully"))
                {
                    if (transactionType == "INSERT")
                    {
                        string[] retArr = ret.Split(':');
                        Int64 courseMarkId = Convert.ToInt64(retArr[1]);

                        foreach (TRN_CourseMarkDetail item in lst)
                        {
                            item.CourseMarkId = courseMarkId;
                            Facade.TRN_CourseMarkDetail.Post(item, "INSERT");
                        }
                    }
                    else if (transactionType == "UPDATE")
                    {
                        foreach (TRN_CourseMarkDetail item in lst)
                        {
                            string trnType = item.CourseMarkDetailId == 0 ? "INSERT" : "UPDATE";
                            item.CourseMarkId = obj.CourseMarkId;
                            Facade.TRN_CourseMarkDetail.Post(item, trnType);
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