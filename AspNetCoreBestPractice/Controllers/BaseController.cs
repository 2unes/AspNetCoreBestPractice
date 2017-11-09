using Microsoft.AspNetCore.Mvc;
using Shared;
using AspNetCoreBestPractice.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreBestPractice.Controllers
{
	[Route("[controller]")]
	public class BaseController<REPO, MODEL> : BaseController where REPO : IRepository<MODEL>
    {
        private readonly REPO repo;

        public REPO Repo { get => repo; }

        public BaseController(REPO repo){
            this.repo = repo;
        }
        [HttpGet]
        public virtual IActionResult New() {
            return View("Edit");
        }

        [HttpPost]
        public virtual IActionResult Create(MODEL model){
            Repo.Insert(model);
            if (Request.IsAjaxRequest()) return Json(new { Success = true, Message = "Record saved" });
            TempData["Info"] = "Record saved";
            return RedirectToAction("List");
        }

        [HttpPut]
		[Route("{id}")]
        public virtual IActionResult Update(int id, [FromBody]MODEL model){
            Repo.Update(model);
            if (Request.IsAjaxRequest()) return Json(new { Success = true, Message = "Record updated" });
            TempData["Info"] = "Record updated";
            return RedirectToAction("List");
        }
        [HttpGet]
        [Route("{id}")]
        public virtual IActionResult Edit(int id){
            var model = Repo.Get(id);
            return View(model);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int id){
            var model = Repo.Get(id);
            if (model != null) Repo.Delete(model);
            if (Request.IsAjaxRequest()){
                return Json(new {Success = true, Message= "Record Removed"});
            }
            TempData["Info"] = "Record removed";
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("List")]
        public virtual IActionResult List(){
            var result = repo.FindAll();
            if (Request.IsAjaxRequest()){
                return  Json(result);
            }
            return View(result);
        }


    }

    public class BaseController : Controller
    {
        public override JsonResult Json(object data)
        {
            var settings = new JsonSerializerSettings { 
                ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
            return base.Json(data, settings);
        }
    }
}
