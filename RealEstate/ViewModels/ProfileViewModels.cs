namespace RealEstate.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using RealEstate.ViewModels.Identity;

    public class ProfileViewModels
    {
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress, MaxLength(255)]
        public string EmailAddress { get; set; }

        [Required]
        [Phone, MaxLength(255)]
        public string PhoneNumber { get; set; }

        public ICollection<_ImageCropper> Avatar { get; set; }

        public string AvatarUpload
            => Avatar.Count > 0 ? JsonConvert.SerializeObject(Avatar) : string.Empty;

        public ChangePasswordViewModels Password { get; set; }
    }
}