namespace RealEstate.Models.Identity
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class Account : IdentityUser
    {
        [Required, MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        public string Avatar { get; set; }
    }
}