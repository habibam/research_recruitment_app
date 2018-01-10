using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace loginregister.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The Name needs to be at least 3 or more characters long!")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Oops, the Password Confirm did not match the Password!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLengthAttribute(10, ErrorMessage = "Your Research Role needs to be at least 10 or more characters long!")]
        public string ResearchRole { get; set; }


        public List<Study> Studies { get; set; }

        public User()
        {
            Studies = new List<Study>();
        }
    }

    public class LoginUser : BaseEntity
    {


        [Key]
        public int UserId { get; set; }



        [Required]
        [EmailAddress]
        public string LogEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword { get; set; }

 [Required]
        public string ConfirmLogPassword { get; set; }

    }

    public abstract class BaseEntity
    {

    }
}