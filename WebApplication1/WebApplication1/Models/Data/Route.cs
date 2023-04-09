using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Data
{
    public partial class Route
    {
        public Route()
        {
            Userroutes = new HashSet<Userroute>();
        }

        public int IdRout { get; set; }
        public int IdUser { get; set; }
        public string? BeginRoute { get; set; }
        public string EndRoute { get; set; } = null!;
        public DateTime DataTimeStart { get; set; }
        public int? Cost { get; set; }
        public int? IdCommentText { get; set; }
        public int? CountPassagir { get; set; }
        public int? IdStatusRoute { get; set; }

        public virtual Commenttext? IdCommentTextNavigation { get; set; }
        public virtual Stausroute? IdStatusRouteNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<Userroute> Userroutes { get; set; }
    }
}
