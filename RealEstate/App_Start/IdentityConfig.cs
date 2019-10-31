namespace RealEstate.App_Start
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RealEstate.Models.Identity;

    public class AccountService : UserManager<Account>
    {
        public AccountService(IUserStore<Account> store) : base(store)
        {
            this.UserValidator = new UserValidator<Account>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
        }            
    }

    public class RoleService : RoleManager<IdentityRole>
    {
        public RoleService(IRoleStore<IdentityRole, string> roleStore) : base(roleStore) { }
    }
}