using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Autopraonica.Model;
using System.Data.Entity;

namespace Autopraonica.Web.Controllers
{
    public class ReservationController : BaseController
    {
        //Reservation
        public ActionResult Index()
        {
            var model = dbContext.Reservations.ToList();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = dbContext.Reservations.First(p => p.ID == id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = dbContext.Reservations.First(p => p.ID == id);
            FillDropdowns();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Reservation model)
        {
            if (ModelState.IsValid)
            {
                
                dbContext.Reservations.Attach(model);

                var entry = dbContext.Entry(model);
                entry.State = EntityState.Modified;

                entry.Property(p => p.Length).IsModified = true;
                entry.Property(p => p.VehicleType).IsModified = true;
                entry.Property(p => p.CompanyID).IsModified = true;
                entry.Property(p => p.ClientID).IsModified = true;

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            FillDropdowns();
            return View(model);
        }

        public ActionResult Create()
        {
            FillDropdowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Reservation model)
        {
            if(ModelState.IsValid)
            {
                dbContext.Reservations.Add(model);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            FillDropdowns();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var model = dbContext.Reservations.First(p => p.ID == id);

            dbContext.Reservations.Remove(model);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //Client
        public ActionResult ClientIndex()
        {
            var model = dbContext.Clients.ToList();
            return View(model);
        }

        public ActionResult ClientDetails(int id)
        {
            var model = dbContext.Clients.First(p => p.ID == id);
            return View(model);
        }

        public ActionResult ClientDelete(int id)
        {
            var lista = dbContext.Reservations.Where(p => p.ClientID == id).ToList();
            foreach (var reservation in lista)
            {
                dbContext.Reservations.Attach(reservation);

                var entry = dbContext.Entry(reservation);
                entry.State = EntityState.Modified;

                entry.Property(p => p.ClientID).CurrentValue = 1;
                entry.Property(p => p.ClientID).IsModified = true;

                dbContext.SaveChanges();
            }

            var model = dbContext.Clients.First(p => p.ID == id);
            dbContext.Clients.Remove(model);
            dbContext.SaveChanges();

            return RedirectToAction("ClientIndex");
        }

        public ActionResult ClientCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientCreate(Client model)
        {
            if (ModelState.IsValid)
            {
                model.DateCreated = DateTime.Now;
                dbContext.Clients.Add(model);
                dbContext.SaveChanges();

                return RedirectToAction("ClientIndex");
            }
            return View(model);
        }

        public ActionResult ClientEdit(int id)
        {
            var model = dbContext.Clients.First(p => p.ID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ClientEdit(Client model)
        {
            if (ModelState.IsValid)
            {
                dbContext.Clients.Attach(model);

                var entry = dbContext.Entry(model);
                entry.State = EntityState.Modified;

                entry.Property(p => p.Name).IsModified = true;
                entry.Property(p => p.Surname).IsModified = true;
                entry.Property(p => p.Email).IsModified = true;


                dbContext.SaveChanges();
                return RedirectToAction("ClientIndex");
            }
            return View(model);
        }

        private void FillDropdowns()
        {
            var possibleCompanies = dbContext.Companies.ToList();

            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = " grad ";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var company in possibleCompanies)
            {
                listItem = new SelectListItem();
                listItem.Text = company.Name;
                listItem.Value = company.ID.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
            ViewBag.PossibleCompanies = selectItems;


            var possibleClients = dbContext.Clients.ToList();

            var selectItems2 = new List<SelectListItem>();

            var listItem2 = new SelectListItem();
            listItem2.Text = " Klijenti ";
            listItem2.Value = "";
            selectItems2.Add(listItem2);

            foreach (var client in possibleClients)
            {
                listItem2 = new SelectListItem();
                listItem2.Text = client.Name + " " + client.Surname;
                listItem2.Value = client.ID.ToString();
                listItem2.Selected = false;
                selectItems2.Add(listItem2);
            }
            ViewBag.PossibleClients = selectItems2;
        }
    }
}