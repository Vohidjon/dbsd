using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(1)]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }

    }
}