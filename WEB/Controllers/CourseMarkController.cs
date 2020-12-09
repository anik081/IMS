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
    public class CourseMarkController : Controller
    {
        // GET: CourseMark
        public JsonResult Get()
        {
            try
            {
                var list = Facade.TRN_CourseMarkDetail.Get();
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

                var list = Facade.TRN_CourseMarkDetail.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(TRN_CourseMarkDetail obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                ret = Facade.TRN_CourseMarkDetail.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}