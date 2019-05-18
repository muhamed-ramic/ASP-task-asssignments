using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSPIS.Web.Models
{
    public class KontaktOsoba
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Ulica { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public virtual Mjesto Mjesto { get; set; }
        public int MjestoId { get; set; }
    }
}
