using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;

namespace DoctorAppointment.RazorPages.Areas.Admin.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [MaxLength(256)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public static implicit operator UserModel(ApplicationUser user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Password = null // Never map password from DB
            };
        }

        public static implicit operator ApplicationUser(UserModel model)
        {
            return new ApplicationUser
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
                // Password is passed to UserManager separately
            };
        }
    }
}
