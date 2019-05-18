using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSPIS.Web.Models
{
    public class PravnoLice:KontaktOsoba
    {
        public int Id { get; set; }
        public string PIB { get; set; }
        public string MaticniBroj { get; set; }
    }

}
