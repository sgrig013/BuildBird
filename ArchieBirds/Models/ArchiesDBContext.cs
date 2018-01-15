using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArchieBirds.Models
{
    public class ArchiesDBContext : DbContext
    {
        public DbSet<Archie> Archies { get; set; }
    }
}