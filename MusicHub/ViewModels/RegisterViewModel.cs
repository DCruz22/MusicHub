﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Foolproof;

namespace MusicHub.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [Editable(false)]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [Required]
        [Display(Name = "Username")]
        [MaxLength(15)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "About me")]
        [MaxLength(300)]
        public string AboutMe { get; set; }

        public string PhotoUrl { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}