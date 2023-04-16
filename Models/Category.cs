using Microsoft.AspNetCore.Http;
using MultiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Models
{
    public class Category : BaseEntity
    {
        public string Image { get; set; }
        [Required, StringLength(maximumLength: 25)]
        public string Name { get; set; }
        public List<ClothesCategory> ClothesCategory { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
