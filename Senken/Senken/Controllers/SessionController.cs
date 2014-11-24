﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Senken.DAL;
using Senken.Models;

namespace Senken.Controllers
{
    public class SessionController : Controller
    {
        private SenkenContext db = new SenkenContext();

        // GET: Session
        public ActionResult Index()
        {
            var sessions = db.Sessions.Include(s => s.InstrumentSetup).Include(s => s.Musician);
            return View(sessions.ToList());
        }

        // GET: Session/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Session/Create
        public ActionResult Create()
        {
            ViewBag.InstrumentSetupID = new SelectList(db.InstrumentSetups, "InstrumentSetupID", "Name");
            ViewBag.MusicianID = new SelectList(db.Musicians, "ID", "LastName");
            return View();
        }

        // POST: Session/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionID,InstrumentSetupID,MusicianID,Rating,OscIFrequency,OscType,LFOIFrequency,LFOIScale,LFOType,CompressorRatio,CompressorKnee,CompressorThreshold,MasterGain")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstrumentSetupID = new SelectList(db.InstrumentSetups, "InstrumentSetupID", "Name", session.InstrumentSetupID);
            ViewBag.MusicianID = new SelectList(db.Musicians, "ID", "LastName", session.MusicianID);
            return View(session);
        }

        // GET: Session/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstrumentSetupID = new SelectList(db.InstrumentSetups, "InstrumentSetupID", "Name", session.InstrumentSetupID);
            ViewBag.MusicianID = new SelectList(db.Musicians, "ID", "LastName", session.MusicianID);
            return View(session);
        }

        // POST: Session/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionID,InstrumentSetupID,MusicianID,Rating,OscIFrequency,OscType,LFOIFrequency,LFOIScale,LFOType,CompressorRatio,CompressorKnee,CompressorThreshold,MasterGain")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstrumentSetupID = new SelectList(db.InstrumentSetups, "InstrumentSetupID", "Name", session.InstrumentSetupID);
            ViewBag.MusicianID = new SelectList(db.Musicians, "ID", "LastName", session.MusicianID);
            return View(session);
        }

        // GET: Session/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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
