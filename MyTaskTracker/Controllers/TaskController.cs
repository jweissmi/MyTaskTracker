using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTaskTracker;
using System.Data.Entity.Core.Objects;

namespace MyTaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private TaskTrackerEntities db = new TaskTrackerEntities();

        // GET: Task
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        // GET: Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,TaskName,DueDate,TaskText,TaskComplete")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,TaskName,DueDate,TaskText,TaskComplete")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult SelectTaskStatus()
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    items.Add(new SelectListItem { Text = "Due Today" });
        //    items.Add(new SelectListItem { Text = "Past Due" });
        //    items.Add(new SelectListItem { Text = "Due in Future" });
        //    items.Add(new SelectListItem { Text = "Complete" });
        //    items.Add(new SelectListItem { Text = "Not Complete" });

        //    ViewBag.SelectTaskStatus = items;
        //    return View();
        //}

        public ActionResult DueToday()
        {
            var tasks = (from t in db.Tasks
                         where t.DueDate.Year == DateTime.Now.Year
                         && t.DueDate.Month == DateTime.Now.Month
                         && t.DueDate.Day == DateTime.Now.Day
                         select t).ToList();
            return View(tasks);
        }

        public ActionResult DueSoon()
        {
            var tasks = (from t in db.Tasks
                         where DbFunctions.TruncateTime(t.DueDate) > DbFunctions.TruncateTime(DateTime.Now)
                         select t).ToList();
            return View(tasks);
        }

        public ActionResult PastDue()
        {
            var tasks = (from t in db.Tasks
                         where DbFunctions.TruncateTime(t.DueDate) > DbFunctions.TruncateTime(DateTime.Now)
                         select t).ToList();
            return View(tasks);
        }

        public ActionResult Complete()
        {
            var tasks = (from t in db.Tasks
                         where t.TaskComplete == true
                         select t).ToList();
            return View(tasks);
        }

        public ActionResult NotComplete()
        {
            var tasks = (from t in db.Tasks
                         where t.TaskComplete == false
                         select t).ToList();
            return View(tasks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
