﻿using System.ComponentModel.DataAnnotations;

namespace Scalemodels.Models
{
    public class WishList : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        public int ManifacturerId { get; set; }
        [Required]
        public Manifacturer Manifacturer { get; set; }

        [Required]
        public string FactoryNumber { get; set; }
    }
}
