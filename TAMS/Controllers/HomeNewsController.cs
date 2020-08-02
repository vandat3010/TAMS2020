using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.DAL;
using TAMS.Entity;

namespace TAMS.Controllers
{
    public class HomeNewsController : Controller
    {
        public ActionResult Index()
        {
            NewsContext newsContext = new NewsContext();
            ViewData["ListNews"] = newsContext.GetNewsOfPage(13, 1, 6).Item1;
            CategoryContext categoryContext = new CategoryContext();
            ViewData["ListCategory"] = categoryContext.GetAllCategories();
            return View();
        }
        [HttpGet]
        public ActionResult GetPageNewsofCategory(int CategoryId = 13)
        {
            NewsContext newsContext = new NewsContext();
            CategoryContext categoryContext = new CategoryContext();
            var dataSet = newsContext.GetNewsOfPage(CategoryId, 1, 6);
            int PageIndex = dataSet.Item2 % 6;
            if (PageIndex > 0) PageIndex = dataSet.Item2 / 6 + 1;
            Tuple<List<News>, int, int> dataReturn = Tuple.Create(dataSet.Item1, PageIndex, CategoryId);
            ViewData["ListNews"] = dataReturn;
            ViewData["ListCategory"] = categoryContext.GetAllCategories();
            return View();
        }
        [HttpGet]
        public IEnumerable GetNewsOfPageCategory(int CategoryId, int Page)
        {
            NewsContext newsContext = new NewsContext();
            var dataSet = newsContext.GetNewsOfPage(CategoryId, Page, 6);
            int PageIndex = dataSet.Item2 % 6;
            if (PageIndex > 0) PageIndex = dataSet.Item2 / 6 + 1;
            Tuple<List<News>, int> dataReturn = Tuple.Create(dataSet.Item1, PageIndex);
            return JsonConvert.SerializeObject(dataReturn);
        }
        /*public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/
    }
}