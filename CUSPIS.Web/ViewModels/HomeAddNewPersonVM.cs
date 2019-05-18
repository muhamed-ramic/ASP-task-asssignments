using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUSPIS.Web.ViewModels
{
    public class HomeAddNewPersonVM
    {
        public HomeAddNewPersonVM()
        {
            TypeOfPerson = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
        }
        public List<SelectListItem> TypeOfPerson { get; set; }
        public int TypeOfPersonId { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public int CitieId { get; set; }
        public string Naziv { get; set; }
        public string Ulica { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string PIB { get; set; }
        public string TajniBroj { get; set; }
        public string MaticniBroj { get; set; }
    }
}
