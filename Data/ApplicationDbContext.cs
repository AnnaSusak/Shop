using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Shop.Data
{
    public class ApplicationDbContext:IdentityDbContext // изменили наследование
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
       public DbSet<MyModel> MyModel { get; set; }
    
        public DbSet<Product> Product { get; set; }
    }
}
