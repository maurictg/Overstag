﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public int Cost { get; set; }
        public ActivityType Type { get; set; }
    }
}
