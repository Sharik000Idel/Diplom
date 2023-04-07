using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Data
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public int? IduserLeaveReview { get; set; }
        public int? IdUserComment { get; set; }
        public decimal? Estimation { get; set; }
        public int? IdCommentText { get; set; }
        public DateOnly? Date { get; set; }

        public virtual Commenttext? IdCommentTextNavigation { get; set; }
    }
}
