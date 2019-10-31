namespace RealEstate.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using RealEstate.App_Start;
    using RealEstate.Models;
    using RealEstate.Infrastructure;
    using RealEstate.Models.ComplexType;

    public class PostGeneration : DataGeneration
    {
        public PostGeneration(_Context context) : base(context)
        {
            Generate();
        }

        //1 - 10
        private string avatarImage = $"/UIAdmin/customs/images/RandomImage/PostAvatarImage/{{index}}.jpg";
        
        //1 - 11
        private string headerImage = $"/UIAdmin/customs/images/RandomImage/PostHeaderImage/{{index}}.jpg";

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
            int postLength = _context.Posts.Count() < 50 ? 100 : 0;
            var categories = _context.PostCategories.ToList();
            var labels = _context.PostLabels.ToList();

            int categoryLength = categories.Count;
            int labelLength = labels.Count;

            Random rd = new Random();

            DateTimeOffset minDate = new DateTime(2018, 1, 1);
            DateTimeOffset maxDate = DateTimeOffset.Now;
            int diffDays = (maxDate - minDate).Days;

            for (int i = 0; i < postLength; i++)
            {
                int labelOne = rd.Next(1, labelLength);
                int labelTwo = labelOne;
                while(labelOne == labelTwo)
                {
                    labelTwo = rd.Next(1, labelLength);
                }

                DateTimeOffset startDate = minDate.AddDays(rd.Next(diffDays));
                //string name = _randomName.RandomString(5, 2);

                Post post = new Post
                {
                    //Name = name.ToUperTitle(),
                    //Description = _randomName.RandomString(20, 5),
                    //Content = _randomName.RandomString(1000, 100),
                    AvatarName = "default-image",
                    AvatarPath = avatarImage.Replace($"{{index}}", $"{rd.Next(1, 11)}"),
                    HeaderImageName = "default-image",
                    HeaderImagePath = headerImage.Replace($"{{index}}", $"{rd.Next(1, 12)}"),
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
                    },
                    PostCategoryId = categories.ElementAt(rd.Next(1, categoryLength)).Id,

                    PostLabelDatas = new List<PostLabelData>
                    {
                        new PostLabelData { PostLabelId = labelOne },
                        new PostLabelData { PostLabelId = labelTwo }
                    },
                    ViewCount = rd.Next(200)
                };

                _context.Posts.AddOrUpdate(p => p.Name, post);
            }
            _context.SaveChanges();
        }
    }
}