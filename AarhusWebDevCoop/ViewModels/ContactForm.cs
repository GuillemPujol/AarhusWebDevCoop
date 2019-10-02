using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage ="Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [Display(Name ="Email")]
        [RegularExpression(@"^ ([a - zA - Z0 - 9_\-\.] +)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a - zA - Z0 - 9\-]+\.)+))([a - zA - Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter a correct email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter a message")]
        public string Message { get; set; }
    }
}