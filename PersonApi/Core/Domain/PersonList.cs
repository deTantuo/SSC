﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Core.Domain
{
    [ModelMetadataType(typeof(Person))]
    public class PersonList
    {
        public string name { get; set; }
        public string state { get; set; }
        public DateTime birthDate { get; set; }

    }
}
