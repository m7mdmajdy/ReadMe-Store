﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Booky_Store.Models
{
    public class  ApplicationUser : IdentityUser
    {
        [Required,MaxLength(100),Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required,MaxLength(100), Display(Name = "Last Name")]
        public string LastName { get; set; }

        public byte[]? ProfilePicture { get; set; }
        public List<Book>? Books { get; set; }
    }
}
