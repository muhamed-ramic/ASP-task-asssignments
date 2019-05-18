using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSPIS.Web.Models
{
    public class FizickoLice:KontaktOsoba
    {
        public int Id { get; set; }
        public string JMBG { get; set; }
        public string TajniBroj { get; set; }
    }
}
