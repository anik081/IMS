using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QtImsDAL;
using QtImsEntity;
using System.Text;

namespace WEB.Controllers
{
    public class CourseRegistrationController : Controller
    {
        // GET: CourseRegistration
        public JsonResult Get()
        {
            try
            {
                var list = Facade.TRN_CourseRegistration.Get();
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

                var list = Facade.TRN_CourseRegistration.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public string Post(TRN_CourseRegistration obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                obj.RegStatusId =1;
                obj.UpdateBy = 1;
                obj.UpdateDate = DateTime.Now;
                obj.Counseledby = 1;
                obj.RegistrationDate = DateTime.Now;
                obj.Remarks = string.IsNullOrEmpty(obj.Remarks) ? string.Empty : obj.Remarks;

                ret = Facade.TRN_CourseRegistration.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}