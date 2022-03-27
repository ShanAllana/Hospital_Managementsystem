using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Hospital.Models;

namespace Project_Hospital
{
    public class DoctorsController : Controller
    {

        //private HospitalDatabaseEntities1 db = new HospitalDatabaseEntities1();
        private HospitalDatabaseEntities db = new HospitalDatabaseEntities();

        // GET: Doctors
        public ActionResult Index()
        {
            List<Project_Hospital.Models.Doctor> objDoctor = new List<Models.Doctor>();
            var data = db.Doctors.ToList();

            foreach (var d in data)
            {
                objDoctor.Add(new Models.Doctor() { Id = Convert.ToInt32(d.ID), Name = d.name, Office = d.office, Email = d.email, telephone = d.telephone.ToString(), address = d.address });
               
            }

            return View(objDoctor);


        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project_Hospital.Doctor d = db.Doctors.Find(id);
            Project_Hospital.Models.Doctor objDoc = new Models.Doctor() { Id = Convert.ToInt32(d.ID), Name = d.name, Office = d.office, Email = d.email, telephone = d.telephone.ToString(), address = d.address };

            if (objDoc == null)
            {
                return HttpNotFound();
            }
            return View(objDoc);

            
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Office,Email,telephone,address")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ///Doctor doctor = db.Doctors.Find(id);
            Project_Hospital.Doctor d = db.Doctors.Find(id);
            Project_Hospital.Models.Doctor objDoc = new Models.Doctor() { Id = Convert.ToInt32(d.ID), Name = d.name, Office = d.office, Email = d.email, telephone = d.telephone.ToString(), address = d.address };

            if (objDoc == null)
            {
                return HttpNotFound();
            }
            return View(objDoc);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Office,Email,telephone,address")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Doctor doctor = db.Doctors.Find(id);
            Project_Hospital.Doctor d = db.Doctors.Find(id);
            Project_Hospital.Models.Doctor objDoc = new Models.Doctor() { Id = Convert.ToInt32(d.ID), Name = d.name, Office = d.office, Email = d.email, telephone = d.telephone.ToString(), address = d.address };

            if (objDoc == null)
            {
                return HttpNotFound();
            }
            return View(objDoc);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
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
