using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSPIS.Web.ViewModels
{
    public class HomeIndexVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Ulica { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Discriminator { get; set; }
        public string NazivMjesta { get; set; }
        public string MjestoId { get; set; }
        public string PIB { get; set; }
        public string TajniBroj { get; set; }
    }
}
