using CUSPIS.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSPIS.Web.EF
{
    public class CuspisDbContext:DbContext
    {
        public CuspisDbContext(DbContextOptions<CuspisDbContext>options):base(options)
        {
        }
        public DbSet<KontaktOsoba> KontaktOosobe { get; set; }
        public DbSet<FizickoLice> FizickaLica { get; set; }
        public DbSet<PravnoLice> PravnaLica { get; set; }
        public DbSet<Mjesto> Mjesta { get; set; }
    }
}
