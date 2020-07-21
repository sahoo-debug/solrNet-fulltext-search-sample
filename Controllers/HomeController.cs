using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolrNet;
using SolrNetSample.Models;
using SolrNetSample.Services;

namespace SolrNetSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ISolrOperations<SearchResultItem> _solr;
        public HomeController(ILogger<HomeController> logger, ISolrOperations<SearchResultItem> solr)
        {
            _logger = logger;
            _solr = solr;
        }

        public IActionResult Index()
        {
            var result = new SolrService().SolrSearchData(_solr,null);
            return View(result);
        }

        public PartialViewResult SearchEmployee(string search)
        {
            var result = new SolrService().SolrSearchData(_solr, search);
            return PartialView("~/Views/Partial/_SearchEmployeePartial.cshtml", result);
        }

        public ViewResult SaveEmployee(string id = null)
        {
            EmployeeModel emp;
            if(id != null)
            {
                emp = new SolrService().GetEmployeeDetail(_solr, id);
            }
            else
            {
                emp = new EmployeeModel();
            }
            return View("SaveEmployee", emp);
        }

        [HttpPost]
        public JsonResult CommitEmployee(EmployeeModel model)
        {
            
            bool result;
            if (model.Id == null)
            {
                //new record
                model.Id = Guid.NewGuid().ToString();
            }
            result = new SolrService().SaveData(_solr, model);
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteEmployee(string id)
        {
            var result = new SolrService().DeleteData(_solr, id);
            return Json(result);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
