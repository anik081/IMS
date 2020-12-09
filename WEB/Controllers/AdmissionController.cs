using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QtImsEntity;
using QtImsDAL;
using System.Text;
using System.Transactions;

namespace WEB.Controllers
{
    public class AdmissionController : Controller
    {
        // GET: Course
        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_Student.Get();
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

                var list = Facade.LU_Student.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public string Post(LU_Student obj, string transactionType, List<LU_StudentEducation> educationList)
        {
            string ret = string.Empty;

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    obj.UpdateBy = 1;
                    obj.CampusId = 1;
                    obj.IsActive = true;
                    obj.StudentStatus = "Admitted";
                    obj.UpdateDate = DateTime.Now;
                    ret = Facade.LU_Student.Post(obj, transactionType);

                    if (ret.Contains("successfully") && (transactionType == "INSERT" || transactionType == "UPDATE"))
                    {
                        string[] retArr = ret.Split(':');
                        int studentId = Convert.ToInt32(retArr[1]);

                        foreach (LU_StudentEducation item in educationList)
                        {
                            obj.UpdateDate = DateTime.Now;
                            item.StudentId = studentId;
                            Facade.LU_StudentEducation.Post(item, "INSERT");
                        }
                    }
                    ts.Complete();
                    return ret;

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }



    }
}