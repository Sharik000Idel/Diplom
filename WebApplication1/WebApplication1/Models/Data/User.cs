using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Data
{
    public partial class User
    {
        public User()
        {
            CommentIdUserCommentNavigations = new HashSet<Comment>();
            CommentIduserLeaveReviewNavigations = new HashSet<Comment>();
            Routes = new HashSet<Route>();
            Userroutes = new HashSet<Userroute>();
        }

        public int IdUsers { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; } = null!;
        public string? Login { get; set; }
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }
        public int? IdCommentText { get; set; }
        public decimal? Estimation { get; set; }
        public int? CarId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Commenttext? IdCommentTextNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Comment> CommentIdUserCommentNavigations { get; set; }
        public virtual ICollection<Comment> CommentIduserLeaveReviewNavigations { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Userroute> Userroutes { get; set; }

        public string Fulname
        {
            get { return Surname + " " + Name + " " + Lastname; }
        }

        public string Age { get { return (DateTime.Today.Year - Birthday.Year).ToString(); } }
        public string Status { get { return IdRoleNavigation.Role1; } }

    }
}
