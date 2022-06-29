using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenERP.ViewModels
{

    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Message is too long, please shorten to 1000 characters")]
        public string Message { get; set; }
    }

}