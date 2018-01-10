using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace loginregister.Models
{
    public class Study : BaseEntity
    {
        [Key]
        public int StudyId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The Study Name needs to be at least 3 or more characters long!")]
        public string StudyName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The Name of the Principle Investigator needs to be at least 3 or more characters long!")]
        public string PrincipleInvestigator { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [ValidateDate]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLengthAttribute(10, ErrorMessage = "The description needs to be at least 10 or more characters long!")]
        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public List<Participant> participants { get; set; }
        public Study()
        {
            participants = new List<Participant>();

        }



    }
    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime Today = DateTime.Now;
            if (value is DateTime)
            {
                DateTime InputDate = (DateTime)value;
                if (InputDate > Today)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Cannot have your study start date in the past");
                }
            }
            return new ValidationResult("Please enter valid date");
        }
    }

}