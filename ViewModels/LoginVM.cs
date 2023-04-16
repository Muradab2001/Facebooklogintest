using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.ViewModels
{
    public class LoginVM
    {
        [Required, StringLength(25)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
