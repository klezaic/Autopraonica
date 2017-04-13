using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Autopraonica.Model;
using Autopraonica.DAL;
using System.Data.Entity;

namespace Autopraonica.Web.Controllers
{
    public class CompanyController : BaseController
    {
        public ActionResult Index()
        {
            var model = dbContext.Companies.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            FillDropdowns();
            return View(new Company());
        }

        [HttpPost]
        public ActionResult Create(Company model)
        {
            if(ModelState.IsValid)
            {
                dbContext.Companies.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            FillDropdowns();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = dbContext.Companies.First(p => p.ID == id);
            FillDropdowns();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                model.Contacts = dbContext.Contacts.Where(p => p.CompanyID == model.ID).ToList();
                model.Reservations = dbContext.Reservations.Where(p => p.CompanyID == model.ID).ToList();
                dbContext.Companies.Attach(model);

                var entry = dbContext.Entry(model);
                entry.State = EntityState.Modified;

                entry.Property(p => p.Address).IsModified = true;
                entry.Property(p => p.CityID).IsModified = true;
                entry.Collection(p => p.Contacts).CurrentValue = model.Contacts;
                entry.Property(p => p.DateFrom).IsModified = true;
                entry.Property(p => p.Name).IsModified = true;
                entry.Collection(p => p.Reservations).CurrentValue = model.Reservations;

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            FillDropdowns();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = dbContext.Companies.First(p => p.ID == id);
            dbContext.Companies.Remove(model);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var lista = dbContext.Contacts.Where(p => p.CompanyID == id).ToList();
            return View(lista);
        }

        //CITY

        public ActionResult CityIndex()
        {
            var model = dbContext.Cities.ToList();
            return View(model);
        }

        public ActionResult CityCreate()
        {
            return View(new City());
        }

        [HttpPost]
        public ActionResult CityCreate(City model)
        {
            if (ModelState.IsValid)
            {
                dbContext.Cities.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("CityIndex");
            }

            FillDropdowns();
            return View(model);
        }

        public ActionResult CityEdit(int id)
        {
            var model = dbContext.Cities.First(p => p.ID == id);
            FillDropdowns();
            return View(model);
        }

        [HttpPost]
        public ActionResult CityEdit(City model)
        {
            if (ModelState.IsValid)
            {
                model.Companies = dbContext.Companies.Where(p => p.CityID == model.ID).ToList();
                dbContext.Cities.Attach(model);

                var entry = dbContext.Entry(model);
                entry.State = EntityState.Modified;

                entry.Property(p => p.Name).IsModified = true;
                entry.Property(p => p.PostalCode).IsModified = true;

                dbContext.SaveChanges();

                return RedirectToAction("CityIndex");
            }
            FillDropdowns();
            return View(model);
        }

        public ActionResult CityDelete(int id)
        {
            var lista = dbContext.Companies.Where(p => p.CityID == id).ToList();
            foreach (var company in lista)
            {
                dbContext.Companies.Attach(company);

                var entry = dbContext.Entry(company);
                entry.State = EntityState.Modified;

                entry.Property(p => p.CityID).CurrentValue = 1;
                entry.Property(p => p.CityID).IsModified = true;

                dbContext.SaveChanges();
            }



            var model = dbContext.Cities.First(p => p.ID == id);
            dbContext.Cities.Remove(model);
            dbContext.SaveChanges();
            return RedirectToAction("CityIndex");
        }

        //CONTACT

        public ActionResult ContactCreate(int id)
        {
            var model = dbContext.Companies.First(p => p.ID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactCreate(CompanyContact model, int ID)
        {
            if (ModelState.IsValid)
            {
                var company = dbContext.Companies.First(p => p.ID == ID);
                model.Company = company;
                model.CompanyID = company.ID;
                dbContext.Contacts.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            FillDropdowns();
            return View(model);
        }

        public ActionResult ContactEdit(int id)
        {
            var model = dbContext.Contacts.First(p => p.ID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactEdit(CompanyContact model)
        {
            if (ModelState.IsValid)
            {
                Company c = dbContext.Companies.First(p => p.ID == model.CompanyID);
                List<CompanyContact> lista = c.Contacts.Where(p => p.CompanyID == c.ID).ToList();

                foreach (var cc in lista)
                {
                    if(cc.ID == model.ID)
                    {
                        cc.Value = model.Value;
                        cc.ContactType = model.ContactType;
                    }
                }

                dbContext.Companies.Attach(c);

                var entry = dbContext.Entry(c);
                entry.State = EntityState.Modified;

                entry.Collection(p => p.Contacts).CurrentValue = lista;

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            FillDropdowns();
            return View(model);
        }

        public ActionResult ContactDelete(int id)
        {
            var oldModel = dbContext.Contacts.First(p => p.ID == id);

            var company = dbContext.Companies.First(p => p.ID == oldModel.CompanyID);

            List<CompanyContact> lista = new List<CompanyContact>();
            foreach (CompanyContact cc in company.Contacts.ToList())
            {
                if (cc.ID != oldModel.ID)
                    lista.Add(cc);
            }

            Company c = new Company();
            c.Name = company.Name;
            c.Address = company.Address;
            c.CityID = company.CityID;
            c.City = company.City;
            c.Contacts = lista;
            c.DateCreated = company.DateCreated;
            c.DateFrom = company.DateFrom;
            c.Reservations = company.Reservations;
            c.ID = company.ID;

            dbContext.Companies.Add(c);

            dbContext.Companies.Remove(company);

            company.Contacts.Remove(oldModel);

            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //

        private void FillDropdowns()
        {
            var possibleCities = dbContext.Cities.ToList();

            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = " Grad ";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var city in possibleCities)
            {
                listItem = new SelectListItem();
                listItem.Text = city.Name;
                listItem.Value = city.ID.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
            ViewBag.PossibleCities = selectItems;
        }
    }
}