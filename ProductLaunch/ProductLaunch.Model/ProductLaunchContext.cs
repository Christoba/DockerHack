using kCura.Hack.Data;
using System.Data.Entity;

namespace ProductLaunch.Model
{
    public class ProductLaunchContext : DbContext
    {
        public ProductLaunchContext() : base("ProductLaunchDb") { }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CustodianType> Roles { get; set; }

        public DbSet<Custodian> Prospects { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Country>().HasKey(c => c.CountryCode);
            builder.Entity<CustodianType>().HasKey(r => r.TypeCode);
            builder.Entity<Custodian>().HasOptional<Country>(p => p.Country);
            builder.Entity<Custodian>().HasOptional<CustodianType>(p => p.CustodianType);            
        }        
    }
}
