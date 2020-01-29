using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crud_test_week.Models;

namespace Crud_test_week.Controllers
{
    public class userstblsController : Controller
    {
        private dbUserEntities db = new dbUserEntities();

        // GET: userstbls
        public async Task<ActionResult> Index()
        {
            return View(await db.userstbl.ToListAsync());
        }

        // GET: userstbls/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userstbl userstbl = await db.userstbl.FindAsync(id);
            if (userstbl == null)
            {
                return HttpNotFound();
            }
            return View(userstbl);
        }

        // GET: userstbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userstbls/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "username,password,country,complete_name")] userstbl userstbl)
        {
            if (ModelState.IsValid)
            {
                db.userstbl.Add(userstbl);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userstbl);
        }

        // GET: userstbls/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userstbl userstbl = await db.userstbl.FindAsync(id);
            if (userstbl == null)
            {
                return HttpNotFound();
            }
            return View(userstbl);
        }

        // POST: userstbls/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "username,password,country,complete_name")] userstbl userstbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userstbl).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userstbl);
        }

        // GET: userstbls/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userstbl userstbl = await db.userstbl.FindAsync(id);
            if (userstbl == null)
            {
                return HttpNotFound();
            }
            return View(userstbl);
        }

        // POST: userstbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            userstbl userstbl = await db.userstbl.FindAsync(id);
            db.userstbl.Remove(userstbl);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
