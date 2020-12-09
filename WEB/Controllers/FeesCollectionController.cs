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
    public class FeesCollectionController : Controller
    {
        // GET: FeesCollection
        public JsonResult Get()
        {
            try
            {
                var list = Facade.TRN_StudentPayment.Get();
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

                var list = Facade.TRN_StudentPayment.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(TRN_StudentPayment obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                obj.UpdateBy = 1;
                obj.ReceivedById = 1;
                obj.UpdateDate = DateTime.Now;
                ret = Facade.TRN_StudentPayment.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}