﻿using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Test.Unit.Models
{
    public class ProductProxy:Product
    {
        public ProductProxy(string name)
        {
            this.Name = name;

        }
    }
}
