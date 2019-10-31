namespace RealEstate.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using RealEstate.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<RealEstate.Models._Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RealEstate.Models._Context";
        }

        protected override void Seed(RealEstate.Models._Context context)
        {
            List<DataGeneration> seedData = new List<DataGeneration>()
            {
                //new CityGeneration(context),
                //new DistrictGeneration(context),
                //new PropertyTypeGeneration(context),
                //new PropertyStatusGeneration(context),
                //new PropertyGeneration(context),

                //new LabelGeneration(context),
                //new CategoryGeneration(context),
                //new PostGeneration(context),
                //new MenuGeneration(context)
                //new AuthorizeGeneration(context)
            };
        }
    }
}