using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Data
{
    public partial class Userroute
    {
        public int IdUserroutes { get; set; }
        public int IdUser { get; set; }
        public int IdRout { get; set; }

        public virtual Route IdRoutNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
