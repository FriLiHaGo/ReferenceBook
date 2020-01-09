using System.Data.Entity;

namespace ReferenceBook
{
    public class ReferenceBookContext : DbContext
    {
        public ReferenceBookContext() :
            base("DataBase")
        { }

        public DbSet<Product> Products { get; set; }
    }
}
