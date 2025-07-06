using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Models;


namespace DAL.EF
{
    internal class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    }
}
