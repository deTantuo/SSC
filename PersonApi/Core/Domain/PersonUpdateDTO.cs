using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Core.Domain
{
    [ModelMetadataType(typeof(Person))]
    public class PersonUpdateDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
    }
}
