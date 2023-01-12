using LaMiaPizzeriaF.Models;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeriaF.Database
{
    public class PizzaContext: DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Database=Pizzeria;" +"Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
