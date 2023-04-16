using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.ViewModels
{
    public class RegisterVM
    {
        [Required, StringLength(35)]
        public string FirstName { get; set; }
        [Required, StringLength(30)]
        public string LastName { get; set; }
        [Required, StringLength(25)]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool Terms { get; set; }
    }
}
