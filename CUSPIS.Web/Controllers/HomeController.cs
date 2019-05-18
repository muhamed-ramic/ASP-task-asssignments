using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CUSPIS.Web.Models;
using CUSPIS.Web.EF;
using Microsoft.EntityFrameworkCore;
using CUSPIS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CUSPIS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CuspisDbContext _db;
        public HomeController(CuspisDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index(string filterByRole = "",string firstLetter="")
        {
            //Get all persons and filter.
            List<HomeIndexVM> viewModel = new List<HomeIndexVM>();

            viewModel = _db.FizickaLica.Include(x => x.Mjesto)
                                .Select(x => new HomeIndexVM
                                {
                                    Id = x.Id,
                                    Discriminator = "Fizicka",
                                    Email = x.Email,
                                    Fax = x.Fax,
                                    NazivMjesta = x.Mjesto.Naziv,
                                    Naziv = x.Naziv,
                                    TajniBroj = x.TajniBroj,
                                    Ulica = x.Ulica,
                                    Telefon = x.Telefon
                                }).ToList();

            if (filterByRole != "" && filterByRole == "F")
                return PartialView(viewModel);

            var legalPersons = _db.PravnaLica.Include(x => x.Mjesto)
                .Select(x => new HomeIndexVM
                {
                    Id = x.Id,
                    Discriminator = "Pravna",
                    Email = x.Email,
                    Fax = x.Fax,
                    Naziv = x.Naziv,
                    NazivMjesta = x.Mjesto.Naziv,
                    Telefon = x.Telefon,
                    Ulica = x.Ulica,
                    PIB = x.PIB
                }).ToList();

            if (string.IsNullOrEmpty(filterByRole))
            {
                foreach (var person in legalPersons)
                {
                    viewModel.Add(person);
                }
                if (firstLetter != "")
                {
                    viewModel = viewModel.Where(x => x.Naziv.StartsWith(firstLetter)).ToList();
                }
                return View(viewModel);
            }
            else if(filterByRole!="" && filterByRole=="P")
            {
                viewModel = new List<HomeIndexVM>();
                viewModel = legalPersons;
            }
            return PartialView(viewModel);
        }
        public IActionResult DeletePerson(int personId, string Discriminator)
        {
            if (Discriminator == "Pravna")
            {
                PravnoLice legalPerson = _db.PravnaLica.Find(personId);
                _db.PravnaLica.Remove(legalPerson);
            }
            else
            {
                FizickoLice person = _db.FizickaLica.Find(personId);
                _db.FizickaLica.Remove(person);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditPerson(HomeIndexVM model)
        {
            if (model.Discriminator == "Pravna")
            {
                PravnoLice legalPerson = _db.PravnaLica.Find(model.Id);
                legalPerson.Email = model.Email;
                legalPerson.PIB = model.PIB;
                legalPerson.Naziv = model.Naziv;
                legalPerson.Ulica = model.Ulica;
                legalPerson.Telefon = model.Telefon;
                legalPerson.Fax = model.Fax;
                _db.PravnaLica.Update(legalPerson);
            }
            else
            {
                FizickoLice person = _db.FizickaLica.Find(model.Id);
                person.Email = model.Email;
                person.Fax = model.Fax;
                person.Naziv = model.Naziv;
                person.TajniBroj = model.TajniBroj;
                person.Ulica = model.Ulica;
                person.Telefon = model.Telefon;
                _db.FizickaLica.Update(person);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddNewPerson()
        {
            HomeAddNewPersonVM viewmodel = new HomeAddNewPersonVM();
            viewmodel.Cities = _db.Mjesta.Select(x => new SelectListItem
            {
                Text = x.Naziv,
                Value = x.Id.ToString()
            }).ToList();
            viewmodel.TypeOfPerson.Add(new SelectListItem { Text = "Fizicko lice", Value = "1" });
            viewmodel.TypeOfPerson.Add(new SelectListItem { Text = "Pravno lice", Value = "2" });

            return View(viewmodel);
        }
        [HttpPost]
        public IActionResult AddNewPerson(HomeAddNewPersonVM viewmodel)
        {
            if (viewmodel.TypeOfPersonId == 1)
            {
                FizickoLice person = new FizickoLice
                {
                    Email = viewmodel.Email,
                    Fax = viewmodel.Fax,
                    MjestoId = viewmodel.CitieId,
                    Naziv = viewmodel.Naziv,
                    TajniBroj = viewmodel.TajniBroj,
                    Telefon = viewmodel.Telefon,
                    Ulica = viewmodel.Ulica
                };
                _db.FizickaLica.Add(person);
            }
            else
            {
                PravnoLice legalPerson = new PravnoLice
                {
                    Email = viewmodel.Email,
                    Fax = viewmodel.Fax,
                    MjestoId = viewmodel.CitieId,
                    Naziv = viewmodel.Naziv,
                    Telefon = viewmodel.Telefon,
                    Ulica = viewmodel.Ulica,
                    MaticniBroj = viewmodel.MaticniBroj,
                    PIB = viewmodel.PIB
                };
                _db.PravnaLica.Add(legalPerson);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
