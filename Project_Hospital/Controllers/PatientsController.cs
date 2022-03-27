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
    public class PatientsController : Controller
    {
        //private HospitalDatabaseEntities1 db = new HospitalDatabaseEntities1();
        private HospitalDatabaseEntities db = new HospitalDatabaseEntities();

        // GET: Patients
        public ActionResult Index()
        {
            List<Project_Hospital.Models.Patient> objPatient = new List<Models.Patient>();

            var results = from c in db.Patients
                          join ct in db.Doctors on c.doctorid equals ct.ID
                          select new { ID = c.ID, name = c.name, office = c.office, Email = c.email, telephone = c.telephone, address = c.address, DateOfBirth = c.dateOfbirth, doctorid = ct.ID , DoctorName= ct.name };

            var data = results.ToList();
            
            //var data = db.Patients.ToList();
            foreach (var d in data)
            {
                objPatient.Add(new Models.Patient() { Id = Convert.ToInt32(d.ID), Name = d.name.ToString(), Office = d.office.ToString(), Email = d.Email.ToString(), telephone = d.telephone.ToString(), address = d.address.ToString(), DateOfBirth = Convert.ToDateTime(d.DateOfBirth), DoctorID = Convert.ToInt32(d.doctorid), DoctorName = d.DoctorName });
            }

            return View(objPatient);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Patient d = db.Patients.Find(id);
            Project_Hospital.Models.Patient objPatient = new Models.Patient() { Id = Convert.ToInt32(d.ID), Name = d.name.ToString(), Office = d.office.ToString(), Email = d.office.ToString(), telephone = d.telephone.ToString(), address = d.address.ToString(), DateOfBirth = Convert.ToDateTime(d.dateOfbirth), DoctorID = Convert.ToInt32(d.doctorid) };

            if (objPatient == null)
            {
                return HttpNotFound();
            }
            return View(objPatient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            //List<SelectListItem> DoctorList = new List<SelectListItem>();
            //var data = db.Doctors.ToList();

            //var dbUserRoles = new DbUserRoles();
            var selectListDoc = db.Doctors
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.name
                                });


            var selectList = new SelectList(selectListDoc, "Value", "Text");
            ViewData["People"] = selectList;


           // var  = new SelectList(roles, "Value", "Text", objPatient.Id);
           // ViewData["PeopleCreate"] = selectList;

           // ViewData["DoctorList"] = roles;

            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Office,Email,telephone,address,DateOfBirth,DoctorID")] Patient patient, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string strDDLValue = form["ddDoctor"].ToString();
                patient.doctorid = Convert.ToInt32(strDDLValue);
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roles = db.Doctors
                      .Select(x =>
                              new SelectListItem
                              {
                                  Value = x.ID.ToString(),
                                  Text = x.name
                              });

           

            

            Patient d = db.Patients.Find(id);
            Project_Hospital.Models.Patient objPatient = new Models.Patient() { Id = Convert.ToInt32(d.ID), Name = d.name.ToString(), Office = d.office.ToString(), Email = d.office.ToString(), telephone = d.telephone.ToString(), address = d.address.ToString(), DateOfBirth = Convert.ToDateTime(d.dateOfbirth), DoctorID = Convert.ToInt32(d.doctorid) };

            var selectList = new SelectList(roles, "Value", "Text", objPatient.DoctorID);
            ViewData["People"] = selectList;

            if (objPatient == null)
            {
                return HttpNotFound();
            }
            return View(objPatient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Office,Email,telephone,address,DateOfBirth,DoctorID")] Patient patient, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string strDDLValue = form["ddDoctor"].ToString();
                patient.doctorid =  Convert.ToInt32(strDDLValue);
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Patient patient = db.Patients.Find(id);
            Patient d = db.Patients.Find(id);
            Project_Hospital.Models.Patient objPatient = new Models.Patient() { Id = Convert.ToInt32(d.ID), Name = d.name.ToString(), Office = d.office.ToString(), Email = d.office.ToString(), telephone = d.telephone.ToString(), address = d.address.ToString(), DateOfBirth = Convert.ToDateTime(d.dateOfbirth), DoctorID = Convert.ToInt32(d.doctorid) };

            if (objPatient == null)
            {
                return HttpNotFound();
            }
            return View(objPatient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
