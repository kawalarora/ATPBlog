using ATPBlog.Domain;
using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATPBlog.Web.Mvc.Controllers
{
   
    
    public class AjaxActionReturn  {

        public bool ActionSuccess;
        public string ErrorMessage;
        public string  HtmlResult;
    }
    public class BlogsController :   Controller
    {
        private readonly INHibernateRepository<Blog > blogRepository;
        public BlogsController(INHibernateRepository<Blog > blogRepository)
        {
            this.blogRepository = blogRepository;
        }
        public   string RenderPartialViewToString( string viewName, object model)
        {
            Controller thisController =  this;
            // assign the model of the controller from which this method was called to the instance of the passed controller (a new instance, by the way)
            thisController.ViewData.Model = model;

            // initialize a string builder
            using (StringWriter sw = new StringWriter())
            {
                // find and load the view or partial view, pass it through the controller factory
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(thisController.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(thisController.ControllerContext, viewResult.View, thisController.ViewData, thisController.TempData, sw);

                // render it
                viewResult.View.Render(viewContext, sw);

                //return the razorized view/partial-view as a string
                return sw.ToString();
            }
        }
        [Transaction]
        [HttpGet]
        public JsonResult GetAllBlogs()
        {
            AjaxActionReturn ajaxActionReturn = new AjaxActionReturn();
            try
            {
                var blogs = this.blogRepository.GetAll().OrderByDescending(t => t.BlogTime);
                ajaxActionReturn.ActionSuccess = true;
                ajaxActionReturn.HtmlResult = RenderPartialViewToString("BlogsTable", blogs); 
            }
            catch (Exception ex)
            {
                ajaxActionReturn.ActionSuccess = false;
                ajaxActionReturn.ErrorMessage = ex.Message;

            } 
            return this.Json(ajaxActionReturn, JsonRequestBehavior.AllowGet);
         }

        [Transaction]
        [HttpGet]
        public ActionResult GetAllBlogs2()
        {
               var blogs = this.blogRepository.GetAll().OrderByDescending(t => t.BlogTime);
               return PartialView("BlogsTable", blogs);
        }
        public ActionResult Index()
        {

            var blog = this.blogRepository.Get(0);
            return View(blog);
        }

        [Transaction]

        [HttpPost]
        public JsonResult UpdateBlog(Blog blogData)
        {
            AjaxActionReturn ajaxActionReturn = new AjaxActionReturn();

            try
            {
                blogData.BlogTime = DateTime.Now;
                this.blogRepository.SaveOrUpdate(blogData);
                return GetAllBlogs();
                 

            }
            catch (Exception ex)
            {
                ajaxActionReturn.ActionSuccess = false ;
                ajaxActionReturn.ErrorMessage = ex.Message;
            }
            return this.Json(ajaxActionReturn, JsonRequestBehavior.AllowGet);
         }

        [Transaction]

        [HttpPost]
        public ActionResult DeleteBlog(int id)
        {

            AjaxActionReturn ajaxActionReturn = new AjaxActionReturn();

            try
            {

                this.blogRepository.Delete(id);
                return GetAllBlogs();


            }
            catch (Exception ex)
            {
                ajaxActionReturn.ActionSuccess = false;
                ajaxActionReturn.ErrorMessage = ex.Message;
            }
            return this.Json(ajaxActionReturn, JsonRequestBehavior.AllowGet);
        }

    }
}