namespace RealEstate.Models
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RealEstate.Models.UI;
    using RealEstate.Models.Identity;

    public class _Context : IdentityDbContext<Account>
    {
        public _Context() : base("connectionString") { }

        public DbSet<UICache> UICaches { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }

        public DbSet<PostLabel> PostLabels { get; set; }

        public DbSet<PostLabelData> PostLabelDatas { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyImage> PropertyImages { get; set; }

        public DbSet<PropertyStatusCode> PropertyStatusCodes { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Property>().Property(p => p.Area).HasPrecision(18, 2);
        }
    }
}