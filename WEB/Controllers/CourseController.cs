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
    public class CourseController : Controller
    {
        // GET: Course
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

                var list = Facade.LU_Course.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult courseProgramGetDynamic(string where, string orderBy)
        {
            try
            {

                var list = Facade.LU_CourseProgram.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult coursePrereqGetDynamic(string where, string orderBy)
        {
            try
            {

                var list = Facade.LU_CoursePrerequisite.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(LU_Course obj, string transactionType, string programIds, string prerequisitIds)
        {
            string ret = string.Empty;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    obj.UpdateBy = 1;
                    obj.UpdateDate = DateTime.Now;

                    ret = Facade.LU_Course.Post(obj, transactionType);

                    if (ret.Contains("successfully"))
                    {
                        if (transactionType == "INSERT" || transactionType == "UPDATE")
                        {
                            string[] retArr = ret.Split(':');
                            int courseId = Convert.ToInt32(retArr[1]);

                            string[] programIdArray = programIds.Split(',');

                            for (int i = 0; i < programIdArray.Count(); i++)
                            {
                                LU_CourseProgram program = new LU_CourseProgram();
                                program.ProgramId = Convert.ToInt32(programIdArray[i]);
                                program.CourseId = courseId;
                                
                                Facade.LU_CourseProgram.Post(program, "INSERT");
                            }

                            //Same for loop for prerequisit
                            if (!string.IsNullOrEmpty(prerequisitIds))
                            {
                                string[] prerequisitArray = prerequisitIds.Split(',');

                                for (int i = 0; i < prerequisitArray.Count(); i++)
                                {
                                    LU_CoursePrerequisite prerequisit = new LU_CoursePrerequisite();
                                    prerequisit.PrerequisiteCourseId = Convert.ToInt32(prerequisitArray[i]);
                                    prerequisit.CourseId = courseId;

                                    Facade.LU_CoursePrerequisite.Post(prerequisit, "INSERT");

                                }
                            }  
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