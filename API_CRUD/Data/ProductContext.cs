using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CRUD.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
    }
}
