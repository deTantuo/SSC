using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Core.Domain
{
    public class Person
    {
        [Key]
        public int id { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime birthDate { get; set; }

        [MaxLength(200)]
        [Display(Name = "Street Address")]
        public string streetAddress { get; set; }

        [MaxLength(100)]
        [Display(Name = "City")]
        public string city { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [MaxLength(10)]
        [Display(Name = "Postal Code")]
        public string postalCode { get; set; }

        public bool isDeleted { get; set; }
    }
}
