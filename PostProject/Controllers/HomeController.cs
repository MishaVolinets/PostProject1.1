using PostProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PostProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
          return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Post()
        {
            var temp = db.PostModel.ToList();
            return View("Post",temp);
        }
        [HttpPost]
        public ActionResult SentPost(PostModel model)
        {
            if (model == null)
                return View("Post");
            model.Id = Guid.NewGuid().ToString();
            model.DateOfCreate = DateTime.Now;
            db.PostModel.Add(model);
            db.SaveChanges();
            return ShowAllPosts();
        }
        [HttpPost]
        public void DeletePost(string id)
        {
            PostModel model = db.PostModel.Find(id);
            db.PostModel.Remove(model);
            db.SaveChanges();
        }
        //Enable-Migrations
        //add-migration init
        //update-database
        [HttpPost]
        public ActionResult EditPost(string id)
        {
            PostModel model = db.PostModel.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SavePost(PostModel model)
        {
            PostModel lastModel = db.PostModel.Find(model.Id);
            lastModel.Title = model.Title;
            lastModel.Description = model.Description;
            return RedirectToAction("Post", Post());
        }
        [HttpPost]
        public ActionResult ShowAllPosts()
        {
            var list = db.PostModel.ToList().OrderByDescending(x => x.DateOfCreate);
            return PartialView("PostPartial", list);
        }
    }
}