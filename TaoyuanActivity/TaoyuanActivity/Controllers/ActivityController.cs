using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaoyuanActivity.Models;
//using TaoyuanActivity.Service;

namespace TaoyuanActivity.Controllers
{
    public class ActivityController : Controller
    {
        Models.CodeService codeService = new Models.CodeService();
        Models.ActivityService activityService = new Models.ActivityService();
        //private ICodeService CodeService { get; set; }

        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Site()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Model()
        {
            return View();
        }

        /// <summary>
        /// 抓預設值
        /// </summary>
        [HttpPost()]
        public JsonResult GetActDataById(string actId, string weight)
        {
            List<Sum> actModelData = this.activityService.GetActDataById(actId, weight);
            return Json(actModelData);
        }

        [HttpPost]
        public JsonResult GetAgeData()
        {
            List<SelectListItem> ageData = this.codeService.GetAgeData();
            return Json(ageData);
        }

        [HttpPost]
        public JsonResult GetCmdActTypeData(string Age_Id)
        {
            List<SelectListItem> cmdActTypeData = this.codeService.GetCmdActTypeData(Age_Id);
            return Json(cmdActTypeData);
        }

        [HttpPost]
        public JsonResult GetOthActTypeData(string Age_Id)
        {
            List<SelectListItem> otherActTypeData = this.codeService.GetOthActTypeData(Age_Id);
            return Json(otherActTypeData);
        }

        [HttpPost]
        public JsonResult GetAllActTypeData()
        {
            List<SelectListItem> allActTypeData = this.codeService.GetAllActTypeData();
            return Json(allActTypeData);
        }

        [HttpPost]
        public JsonResult GetTypeActivity(string Act_Type_Id)
        {
            List<Models.Activity> actData = this.codeService.GetTypeActivity(Act_Type_Id);
            return Json(actData);
        }

        [HttpPost]
        public JsonResult GetSiteData(string Ping_Id, string In_Out_Id)
        {
            List<Models.Activity> siteData = this.activityService.GetSiteData(Ping_Id, In_Out_Id);
            return Json(siteData);
        }
    }
}