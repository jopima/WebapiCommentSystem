using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Application.data
{
    public class PostRepository
    {
         private Connection conn;
         public PostRepository() { conn = new Connection(); }

         public PostThread GetPostByID(int PostID)
         {
             PostThread post = null;

             using (ForumDBContext dc = conn.GetContext())
             {
                 post = (from a in dc.PostThreads where a.PostID == PostID select a).FirstOrDefault();

             }
             return post;
         }

         public List<PostThread> GetAllPosts()
         {
             IEnumerable<PostThread> posts = null;

             List<PostThread> result = new List<PostThread>();

             using (ForumDBContext dc = conn.GetContext())
             {

                 posts = (from a in dc.PostThreads select a);

                 result = posts.ToList();
             }
             return result;
         }

         public List<PostThread> GetContainsPosts(List<int> postIDs)
         {
             IEnumerable<PostThread> posts = null;

             List<PostThread> result = new List<PostThread>();

             using (ForumDBContext dc = conn.GetContext())
             {

                 posts = (from a in dc.PostThreads where postIDs.Contains(a.PostID) select a);

                 result = posts.ToList();
             }
             return result;
         }

         public PostThread SavePost(PostThread post)
         {
             using (ForumDBContext dc = conn.GetContext())
             {
                 if (post.PostID > 0)
                 {
                     dc.PostThreads.Attach(new PostThread { PostID = post.PostID });
                     dc.PostThreads.ApplyCurrentValues(post);
                 }
                 else
                 {
                     dc.PostThreads.AddObject(post);
                 }
                 dc.SaveChanges();
             }

             return post;
         }

         public void DeletePost(PostThread post)
         {
             using (ForumDBContext dc = conn.GetContext())
             {
                 dc.PostThreads.DeleteObject(dc.PostThreads.Where
                                         (ac => ac.PostID.Equals(post.PostID)).FirstOrDefault());
                 dc.SaveChanges();
             }
         }

    }
}