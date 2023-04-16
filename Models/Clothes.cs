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
    public class Clothes:BaseEntity
    {
        public string Image { get; set; }
        [Required, StringLength(maximumLength: 25)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Text { get; set; }
        public string Desc { get; set; }
        public int InformationId{ get; set; }
        public ClothesInformation ClothesInformation { get; set; }
        public List<ClothesCategory> ClothesCategories { get; set; }
         [NotMapped]
        public List<int> CategoryIds { get; set; }
        public List<ClothesImage> ClothesImages { get; set; }
        [NotMapped]
        public List<int> ImageId { get; set; }
        [NotMapped]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }

        //public List<ClothesColor> ClothesColors { get; set; }
        //public List <ClothesSizes> ClothesSizes { get; set; }
    }
}
