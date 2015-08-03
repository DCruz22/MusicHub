using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foolproof;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(15)]
        public string UserName { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Editable(false)]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public int GenderId { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        public string AboutMe { get; set; }

        public string PhotoUrl { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }

    public class PasswordReset
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}