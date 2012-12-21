using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Application.data
{
    public class CommentRepository
    {
         private Connection conn;
         public CommentRepository() { conn = new Connection(); }

         public Comment GetCommentByID(int CommentID)
         {
             Comment comment = null;

             using (ForumDBContext dc = conn.GetContext())
             {
                 comment = (from a in dc.Comments where a.CommentID == CommentID select a).FirstOrDefault();

             }
             return comment;
         }


         public List<Comment> GetCommentByPostID(int PostID)
         {
             IEnumerable<Comment> comments = null;

             List<Comment> result = new List<Comment>();


             using (ForumDBContext dc = conn.GetContext())
             {

                 comments = (from a in dc.Comments where a.PostID == PostID select a);

                 result = comments.ToList();
             }
             return result;
         }



         public Comment SaveComment(Comment comment)
         {
             using (ForumDBContext dc = conn.GetContext())
             {
                 if (comment.CommentID > 0)
                 {
                     dc.Comments.Attach(new Comment { CommentID = comment.CommentID });
                     dc.Comments.ApplyCurrentValues(comment);
                 }
                 else
                 {
                     dc.Comments.AddObject(comment);
                 }
                 dc.SaveChanges();
             }

             return comment;
         }

         public void DeleteComment(Comment comment)
         {
             using (ForumDBContext dc = conn.GetContext())
             {
                 dc.Comments.DeleteObject(dc.Comments.Where
                                         (ac => ac.CommentID.Equals(comment.CommentID)).FirstOrDefault());
                 dc.SaveChanges();
             }
         }


    }
}