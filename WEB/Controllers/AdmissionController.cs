using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QtImsEntity;
using QtImsDAL;
using System.Text;
using System.Transactions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace WEB.Controllers
{
    public class AdmissionController : Controller
    {
        static HttpPostedFileBase imageFile;
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
        public JsonResult EducationGetDynamic(string where, string orderBy)
        {
            try
            {

                var list = Facade.LU_StudentEducation.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public string DeleteEducationList(List<string> educationDeleteList, string transactionType)
        {
            //string ret = string.Empty;
            
            try
            {
                int Length_A = educationDeleteList.Count;
                if (Length_A > 0)
                {
                    foreach (string item in educationDeleteList)
                    {
                        string where = item;
                        Facade.LU_StudentEducation.StudentEducationDelete(where);
                    }
                }
                
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string ImageFile()
        {
            imageFile = Request.Files["HelpSectionImages"];
            return "";
        }

        [HttpPost]
        public string Post(LU_Student obj, string transactionType, List<LU_StudentEducation> educationList)
        {
            string ret = string.Empty;
            string tempFilePath = string.Empty;
            string actualFilePath = string.Empty;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (imageFile != null)
                    {
                        string FolderPath = "~\\img";

                        // create folder directory info
                        DirectoryInfo FolderDir = new DirectoryInfo(Server.MapPath(FolderPath));

                        // check if folder directory not exist
                        if (!FolderDir.Exists)
                        {
                            // create directory
                            FolderDir.Create();
                        }
                        //string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                        string extension = Path.GetExtension(imageFile.FileName);
                        //fileName = obj.PlayerName + "_" + obj.CountryName.Substring(0, 3) + extension;
                        string fileName = obj.FullName + extension;
                        obj.ImgaeLocation = "/img/" + fileName;
                        //tempFilePath = Path.Combine(Server.MapPath("~/imgTemp/"), fileName);
                        actualFilePath = Path.Combine(Server.MapPath("~/img/"), fileName);

                        imageFile.SaveAs(actualFilePath);
                        imageFile = null;
                    }

                    obj.UpdateBy = 1;
                    obj.CampusId = 1;
                    obj.IsActive = true;
                    obj.StudentStatus = "Admitted";
                    obj.UpdateDate = DateTime.Now;
                    ret = Facade.LU_Student.Post(obj, transactionType);
                    if(transactionType == "DELETE")
                    {
                        foreach (LU_StudentEducation item in educationList)
                        {
                            Facade.LU_StudentEducation.Post(item, "DELETE");
                        }
                    }
                    if (ret.Contains("successfully") && (transactionType == "INSERT" || transactionType == "UPDATE"))
                    {
                        string[] retArr = ret.Split(':');
                        int studentId = Convert.ToInt32(retArr[1]);

                        foreach (LU_StudentEducation item in educationList)
                        {
                            obj.UpdateDate = DateTime.Now;
                            item.StudentId = studentId;
                            if(transactionType == "INSERT")
                            {
                                Facade.LU_StudentEducation.Post(item, "INSERT");

                            }
                            else
                            {
                                Facade.LU_StudentEducation.Post(item, "UPDATE");

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