namespace RealEstate.Data
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RealEstate.Models;
    using RealEstate.Models.Identity;
    using RealEstate.App_Start;
    using RealEstate.App_GlobalResources;

    public class AuthorizeGeneration : DataGeneration
    {
        public AuthorizeGeneration(_Context context) : base(context)
        {
            Generate();
        }


        public override void Generate()
        {
            AccountService accountService = new AccountService(new UserStore<Account>(_context));
            RoleService roleService = new RoleService(new RoleStore<IdentityRole>(_context));

            var role = new IdentityRole(AuthorResource.Administrator);
            roleService.Create(role);
            role = new IdentityRole(AuthorResource.Customer);
            roleService.Create(role);

            var user = new Account
            {
                UserName = "admin01",
                Email = "ttsang1801@gmail.com",
                FullName = "Trương Tấn Sang",
                Avatar = "/UIAdmin/customs/images/users/default_user.png",
                PhoneNumber = "0777961413"
            };
            accountService.Create(user, "111222");
            accountService.AddToRole(user.Id, AuthorResource.Administrator);
        }
    }
}