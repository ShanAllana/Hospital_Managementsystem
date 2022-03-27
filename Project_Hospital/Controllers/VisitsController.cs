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
    public class VisitsController : Controller
    {

        //private HospitalDatabaseEntities1 db = new HospitalDatabaseEntities1();
        private HospitalDatabaseEntities db = new HospitalDatabaseEntities();

        // GET: Visits
        public ActionResult Index()
        {
            //return View(db.Visits.ToList());

            List<Project_Hospital.Models.Visits> objVisits = new List<Models.Visits>();
            //var data = db.Visits.ToList();

            var results = from c in db.Visits
                          join cn in db.Patients on c.patientid equals cn.ID
                          join ct in db.Doctors on c.doctorid equals ct.ID
                          //where (c.CountryID == cn.ID) && (c.CityID == ct.ID) && (c.SectorID == company.SectorID) && (company.SectorID == sect.ID)
                          select new { Id =c.ID, DoctorID =c.doctorid, PatientID=c.patientid, DateOfVisit =c.dateOfvisit, Complaint  = c.complaint, DoctorName =ct.name, PatientName =cn.name};

            var data =results.ToList(); 

            foreach (var d in data)
            {
                objVisits.Add(new Models.Visits() { Id = Convert.ToInt32(d.Id), DoctorID  = Convert.ToInt32(d.DoctorID), PatientID = Convert.ToInt32(d.PatientID), DateOfVisit = Convert.ToDateTime(d.DateOfVisit),Complaint = d.Complaint.ToString(), DoctorName = d.DoctorName, PatientName = d.PatientName});
            }

            return View(objVisits);
        }

        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project_Hospital.Visit d = db.Visits.Find(id);
            Project_Hospital.Models.Visits objVisits = new Models.Visits() { Id = Convert.ToInt32(d.ID), DoctorID = Convert.ToInt32(d.doctorid), PatientID = Convert.ToInt32(d.patientid), DateOfVisit = Convert.ToDateTime(d.dateOfvisit), Complaint = d.complaint.ToString() };

            if (objVisits == null)
            {
                return HttpNotFound();
            }
            return View(objVisits);


         
        }

        // GET: Visits/Create
        public ActionResult Create()
        {
            var selectListDoc = db.Doctors
                      .Select(x =>
                              new SelectListItem
                              {
                                  Value = x.ID.ToString(),
                                  Text = x.name
                              });


            var selectList = new SelectList(selectListDoc, "Value", "Text");
            ViewData["People"] = selectList;

            var selectListPatient = db.Patients
                     .Select(x =>
                             new SelectListItem
                             {
                                 Value = x.ID.ToString(),
                                 Text = x.name
                             });


            var selectList1 = new SelectList(selectListPatient, "Value", "Text");
            ViewData["People1"] = selectList1;

            
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DoctorID,PatientID,DateOfVisit,Complaint")] Project_Hospital.Visit visits, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                String strDDLselValueDoc = form["ddDoctor"].ToString();
                String strDDLselValuePat = form["ddPatient"].ToString();
                visits.doctorid = Convert.ToInt32(strDDLselValueDoc);
                visits.patientid = Convert.ToInt32(strDDLselValuePat);
                db.Visits.Add(visits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visits);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            var selectListDoc = db.Doctors
                      .Select(x =>
                              new SelectListItem
                              {
                                  Value = x.ID.ToString(),
                                  Text = x.name
                              });


           

            var selectListPatient = db.Patients
                     .Select(x =>
                             new SelectListItem
                             {
                                 Value = x.ID.ToString(),
                                 Text = x.name
                             });

            



            Project_Hospital.Visit d = db.Visits.Find(id);
            Project_Hospital.Models.Visits objvisits =  new Models.Visits() { Id = Convert.ToInt32(d.ID), DoctorID = Convert.ToInt32(d.doctorid), PatientID = Convert.ToInt32(d.patientid), DateOfVisit = Convert.ToDateTime(d.dateOfvisit), Complaint = d.complaint.ToString() };


            var selectList = new SelectList(selectListDoc, "Value", "Text", objvisits.DoctorID);
            ViewData["People"] = selectList;

            var selectList1 = new SelectList(selectListPatient, "Value", "Text", objvisits.PatientID);
            ViewData["People1"] = selectList1;


            if (objvisits == null)
            {
                return HttpNotFound();
            }
            return View(objvisits);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DoctorID,PatientID,DateOfVisit,Complaint")] Project_Hospital.Visit visits, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                String strDDLselValueDoc = form["ddDoctor"].ToString();
                String strDDLselValuePat = form["ddPatient"].ToString();
                visits.doctorid= Convert.ToInt32(strDDLselValueDoc);
                visits.patientid = Convert.ToInt32(strDDLselValuePat);
                db.Entry(visits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visits);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Visits visits = db.Visits.Find(id);
            //Project_Hospital.Visit visits = db.Visits.Find(id);

            Project_Hospital.Visit d = db.Visits.Find(id);
            Project_Hospital.Models.Visits objvisits = new Models.Visits() { Id = Convert.ToInt32(d.ID), DoctorID = Convert.ToInt32(d.doctorid), PatientID = Convert.ToInt32(d.patientid), DateOfVisit = Convert.ToDateTime(d.dateOfvisit), Complaint = d.complaint.ToString() };

            if (objvisits == null)
            {
                return HttpNotFound();
            }
            return View(objvisits);

        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project_Hospital.Visit visits = db.Visits.Find(id);
            db.Visits.Remove(visits);
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
