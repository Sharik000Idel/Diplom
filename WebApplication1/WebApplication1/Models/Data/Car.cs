﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Data
{
    public partial class Car
    {
        public Car()
        {
            Users = new HashSet<User>();
        }

        public int IdCar { get; set; }
        public string? GosNumber { get; set; }
        public string? NameCar { get; set; }
        public int? IdCommentCar { get; set; }

        public virtual Commenttext? IdCommentCarNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
