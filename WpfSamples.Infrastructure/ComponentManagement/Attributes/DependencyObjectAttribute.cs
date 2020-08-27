﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSamples.Infrastructure.ComponentManagement.Attributes
{
    public class DependencyObjectAttribute : Attribute
    {
        public bool Transactional { get; set; }
    }
}
