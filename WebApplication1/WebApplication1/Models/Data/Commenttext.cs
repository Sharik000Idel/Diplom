using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Data
{
    public partial class Commenttext
    {
        public Commenttext()
        {
            Comments = new HashSet<Comment>();
            Routes = new HashSet<Route>();
            Users = new HashSet<User>();
        }

        public int IdCommentText { get; set; }
        public string? Text { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
