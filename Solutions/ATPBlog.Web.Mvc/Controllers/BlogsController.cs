using ATPBlog.Domain;
using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATPBlog.Web.Mvc.Controllers
{
    public class BlogsController :   Controller
    {
        private readonly INHibernateRepository<Blog > blogRepository;
        public BlogsController(INHibernateRepository<Blog > blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        [Transaction]
        [HttpGet]
        public ActionResult GetAllBlogs()
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
        public ActionResult UpdateBlog(Blog  blogData)
        {
            blogData.BlogTime = DateTime.Now;
            this.blogRepository.SaveOrUpdate(blogData );
            return GetAllBlogs();
         }

        [Transaction]

        [HttpPost]
        public ActionResult DeleteBlog(int id)
        {

            this.blogRepository.Delete(id);
            return GetAllBlogs();
        }

    }
}