using AptinetDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.DataAccess
{
    public class AP_DBContext : DbContext
    {
        public AP_DBContext(DbContextOptions<AP_DBContext> options) : base(options)
        {

        }
        public virtual DbSet<AdminUser> AdminUser { get; set; }
        public virtual DbSet<Factor> Factor { get; set; }
        public virtual DbSet<FactorList> FactorList { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Suggestion> Suggestion { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Versions> Versions { get; set; }
        public virtual DbSet<Weights> Weights { get; set; }
    }
}
