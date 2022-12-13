using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Please enter first name.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [MinLength(8, ErrorMessage = "Password must have at least 8 characters.")]
        public string password { get; set; }


        public string country { get; set; }


        //[Compare( ErrorMessage = "You must accept the terms and conditions")]
      
        [EnforceTrue(ErrorMessage = @"You must accept the terms and conditions")]
        public bool AcceptsTerms { get; set; } = false;
        public Customer(string firstName, string lastName, string emailAddress, string password, string country)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.password = password;
            this.country = country;
        }
        public Customer()
        {

        }
    }
}

public class EnforceTrueAttribute : ValidationAttribute, IClientValidatable
{
    public override bool IsValid(object value)
    {
        if (value == null) return false;
        if (value.GetType() != typeof(bool)) throw new InvalidOperationException("can only be used on boolean properties.");
        return (bool)value == true;
    }

    public override string FormatErrorMessage(string name)
    {
        return "The " + name + " field must be checked in order to continue.";
    }

    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
        yield return new ModelClientValidationRule
        {
            ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
            ValidationType = "enforcetrue"
        };
    }
}