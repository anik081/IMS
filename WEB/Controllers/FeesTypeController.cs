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
    public class FeesTypeController : Controller
    {
        // GET: FeesType
        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_Fees.Get();
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

                var list = Facade.LU_Fees.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(LU_Fees obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                obj.UpdateBy = 1;
                obj.InstituteId = 1;
                obj.UpdateDate = DateTime.Now;
                obj.ApplyAt = obj.FeesName;
                ret = Facade.LU_Fees.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}