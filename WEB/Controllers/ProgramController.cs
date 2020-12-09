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
    public class ProgramController : Controller
    {
        // GET: Program
        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_Program.Get();
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
                
                var list = Facade.LU_Program.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDynamicEmployee(string where, string orderBy)
        {
            try
            {

                var list = Facade.LU_Employee.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult ProgramTypeGetDynamic(string where, string orderBy)
        //{
        //    try
        //    {

        //        var list = Facade.LU_Program.GetDynamic(where, orderBy);
        //        string contentType = "application/json";
        //        return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}
        [HttpPost]
        public string Post(LU_Program obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                obj.UpdateBy = 1;
                obj.UpdateDate = DateTime.Now;

                ret = Facade.LU_Program.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}