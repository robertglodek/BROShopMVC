﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Category
{
    public class DeleteCategoryCommand:ICommand
    {
        public Guid Id { get; set; }
    }
}
