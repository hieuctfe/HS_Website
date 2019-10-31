namespace RealEstate.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using RealEstate.Models;
    using RealEstate.Infrastructure;
    using RealEstate.Models.ComplexType;
    using RealEstate.App_Start;

    public class PropertyGeneration : DataGeneration
    {
        public PropertyGeneration(_Context context) : base(context)
        {
            Generate();
        }

        //0 - 10
        private string avatarImage = $"/UIAdmin/customs/images/RandomImage/PropertyAvatarImage/{{index}}.jpg";
        //0 - 10
        private string headerImage = $"/UIAdmin/customs/images/RandomImage/PropertyHeaderImage/{{index}}.jpg";

        private string[] _randomName = { "amet", "consectetur", "adipiscing", "elit", "morbi",
            "pretium", "libero", "porta", "est", "lacinia", "interdum", "fusce", "in", "semper", "orci", "pellentesque",
            "ullamcorper", "justo", "mauris", "pharetra", "lorem", "dignissim", "felis", "luctus", "eget", "auctor", "nulla",
            "consectetur", "quisque", "semper", "diam", "ut", "faucibus", "tristique", "nullam", "leo", "nunc", "condimentum",
            "tincidunt", "dolor", "in", "pellentesque", "interdum", "nunc", "mauris", "urna", "nunc", "dictum", "nec", "ultrices",
            "nec", "vestibulum", "a", "diam", "nulla", "porta", "ultricies", "odio", "quis", "fermentum", "turpis", "pellentesque",
            "eget", "etiam", "dictum", "ligula", "magna", "ac", "viverra", "erat", "pharetra", "vel", "duis", "tempus", "pretium",
            "dolor", "quis", "lobortis", "suspendisse", "rhoncus", "nunc", "quis", "sapien", "pellentesque", "et", "faucibus", "nulla",
            "venenatis", "nullam", "et", "ex", "quam", "ut", "eget", "eleifend", "nisl", "pellentesque", "lacinia", "interdum", "ultrices",
            "vivamus", "vel", "imperdiet", "tortor" };

        string[] addressName =
        {
            "JAMES", "JOHN", "ROBERT", "MICHAEL", "WILLIAM", "DAVID", "RICHARD",
            "CHARLES", "JOSEPH", "THOMAS", "CHRISTOPHER", "DANIEL", "PAUL", "MARK",
            "DONALD", "GEORGE", "KENNETH", "STEVEN", "EDWARD", "BRIAN", "RONALD",
            "ANTHONY", "KEVIN", "JASON", "MATTHEW", "GARY", "TIMOTHY", "JOSE",
            "LARRY", "JEFFREY", "FRANK", "SCOTT", "ERIC", "STEPHEN", "ANDREW",
            "RAYMOND", "GREGORY", "JOSHUA", "JERRY", "DENNIS", "WALTER", "PATRICK",
            "PETER", "HAROLD", "DOUGLAS", "HENRY", "CARL", "ARTHUR", "RYAN", "ROGER",
            "JUSTIN", "TERRY", "GERALD", "KEITH", "SAMUEL", "WILLIE", "RALPH", "LAWRENCE",
            "NICHOLAS", "ROY", "BENJAMIN", "BRUCE", "BRANDON", "ADAM", "HARRY", "FRED", "WAYNE",
            "BILLY", "STEVE", "LOUIS", "JEREMY", "AARON", "RANDY", "HOWARD", "EUGENE", "CARLOS",
            "RUSSELL", "BOBBY", "VICTOR", "MARTIN", "ERNEST", "PHILLIP", "TODD", "JESSE", "CRAIG",
            "ALAN", "SHAWN", "CLARENCE", "SEAN", "PHILIP", "CHRIS", "JOHNNY", "EARL", "JIMMY",
            "ANTONIO", "DANNY", "BRYAN", "TONY", "LUIS", "MIKE"
        };


        public override void Generate()
        {
            int propertyLength = _context.Properties.Count() < 50 ? 1000 : 0;
            Random rd = new Random();

            int minRoom = 1,
                maxRoom = 5;

            DateTimeOffset minDate = new DateTimeOffset(new DateTime(2018, 1, 1));
            DateTimeOffset maxDate = DateTimeOffset.Now;
            int diffDays = (maxDate - minDate).Days;

            decimal minPrice = 3_000_000M,
                maxPrice = 1_000_000_000M;
            decimal minArea = 30M,
                maxArea = 1_000M;

            IQueryable<City> cities = _context.Cities.Include(x => x.Districts);

            IQueryable<PropertyType> propertyType = _context.PropertyTypes;
            IQueryable<PropertyStatusCode> propertyStatus = _context.PropertyStatusCodes;

            for (int i = 0; i < propertyLength; i++)
            {
                DateTimeOffset startDate = minDate.AddDays(rd.Next(diffDays));
                string
                    //name = _randomName.RandomString(5, 2),
                    cityName = "Hồ Chí Minh",
                    districtName = "Quận 1";
                bool hasCarGarage = rd.Next() % 2 == 0;

                City city = cities.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (city != null)
                {
                    cityName = city.Name;
                    districtName = city.Districts.OrderBy(x => Guid.NewGuid()).FirstOrDefault().Name;
                }

                Property prop = new Property
                {
                    //Code = string.Empty.RandomString(8),
                    //Name = name.ToUperTitle(),
                    //AvatarName = "default-image",
                    //AvatarPath = avatarImage.Replace($"{{index}}", $"{rd.Next(0, 11)}"),
                    //ShortDescription = _randomName.RandomString(20, 5),
                    //DetailDescription = _randomName.RandomString(1000, 100),
                    //Price = minPrice.RandomInRange(maxPrice),
                    //Area = minArea.RandomInRange(maxArea),
                    //NumberOfBathRoom = minRoom.RandomInRange(maxRoom),
                    //NumberOfBedRoom = minRoom.RandomInRange(maxRoom),
                    NumberOfGarage = hasCarGarage ? 1 : 0,
                    IsFeatured = rd.Next() % 2 == 0 && rd.Next() % 2 == 0,
                    HasSwimming = rd.Next() % 2 == 0,
                    HasGarden = rd.Next() % 2 == 0,
                    HasCarGarage = hasCarGarage,
                    City = cityName,
                    District = districtName,
                    AddressLine = $"{rd.Next(10, 99)}/{rd.Next(10, 99)}, đường {addressName[rd.Next(addressName.Length)]} {addressName[rd.Next(addressName.Length)]}",
                    PropertyStatusCodeId = propertyStatus.OrderBy(x => Guid.NewGuid()).Select(x => x.Id).FirstOrDefault(),
                    PropertyTypeId = propertyType.OrderBy(x => Guid.NewGuid()).Select(x => x.Id).FirstOrDefault(),
                    PropertyImages = new List<PropertyImage>()
                    {
                        new PropertyImage { Name = "default-image", Path = headerImage.Replace($"{{index}}", $"{rd.Next(0, 11)}") },
                        new PropertyImage { Name = "default-image", Path = headerImage.Replace($"{{index}}", $"{rd.Next(0, 11)}") },
                        new PropertyImage { Name = "default-image", Path = headerImage.Replace($"{{index}}", $"{rd.Next(0, 11)}") },
                    },
                    ActivityLog = new ActivityLog
                    {
                        CreatedOn = startDate,
                        ModifiedOn = startDate.AddDays(rd.Next(1, 1000))
                    },
                    UIOption = new UIOption()
                    {
                        DisplayOrder = i + 1,
                        IsDisplay = rd.Next() % 2 == 0
                    },
                    Seo = new Seo()
                    {
                        //AliasName = name,
                        //Title = name.ToUperTitle(),
                        //FriendlyUrl = name.RewriteUrl()
                    }
                };

                _context.Properties.AddOrUpdate(p => p.Name, prop);
            }
            _context.SaveChanges();
        }
    }
}