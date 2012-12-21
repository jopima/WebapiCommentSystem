using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Forum.Application.WebApi
{
    public class ForumController : ApiController
    {
        data.PostRepository postFactory = new data.PostRepository();
        data.CommentRepository commentFactory = new data.CommentRepository();


        [Queryable]
        public IQueryable<Posts> Get()
        {
            IEnumerable<data.PostThread> posts = postFactory.GetAllPosts();
            List<Posts> _postView = new List<Posts>();
            
            foreach (data.PostThread data in posts) {
                List<data.Comment> _comments = commentFactory.GetCommentByPostID(data.PostID);
                List<Comments> _commentView = new List<Comments>();
                foreach (data.Comment item in _comments) {
                    _commentView.Add(new Comments
                    {
                        CommentID = item.CommentID.ToString(),
                        CommentAuthor = item.CommentAuthor,
                     CommentContent = item.CommentContent, PostID = item.PostID.ToString()});                
                }
                

                _postView.Add(new Posts { PostAuthor = data.PostAuthor, PostContent = data.PostContent, 
                    PostID = data.PostID.ToString(), comments = _commentView});
            
            }

            return _postView.AsQueryable();
        }

        public IEnumerable<Posts> GetComments(int pageIndex, int pageSize)
        {
            IEnumerable<data.PostThread> posts = postFactory.GetAllPosts();
            List<Posts> _postView = new List<Posts>();

            foreach (data.PostThread data in posts)
            {
                List<data.Comment> _comments = commentFactory.GetCommentByPostID(data.PostID);
                List<Comments> _commentView = new List<Comments>();

                if (_comments != null || _comments.Count != 0)
                {
                    foreach (data.Comment item in _comments)
                    {
                        _commentView.Add(new Comments
                        {
                            CommentID = item.CommentID.ToString(),
                            CommentAuthor = item.CommentAuthor,
                            CommentContent = item.CommentContent,
                            PostID = item.PostID.ToString()
                        });
                    }


                    _postView.Add(new Posts
                    {
                        PostAuthor = data.PostAuthor,
                        PostContent = data.PostContent,
                        PostID = data.PostID.ToString(),
                        comments = _commentView
                    });

                }
            }

            _postView.OrderBy(a => a.PostID);

            return _postView.Skip(pageIndex * pageSize).Take(pageSize); 
           
        }

        [HttpGet]
        public IEnumerable<data.PostThread> getContains(string giveMe)
        {
            List<int> postIDs = new List<int>();
            postIDs.Add(2);
            postIDs.Add(4);
            postIDs.Add(6);

            List<data.PostThread> threads = postFactory.GetContainsPosts(postIDs);

            List<data.PostThread> post = new List<data.PostThread>();

            foreach (data.PostThread item in threads) {

                post.Add(new data.PostThread { PostID = item.PostID, PostAuthor = item.PostAuthor, PostContent = item.PostContent });
            }
            
            return post;
            
        
        }
        // GET api/<controller>/5
        public Posts Get(int PostID)
        {
            data.PostThread posts = postFactory.GetPostByID(PostID);
            Posts _postView = new Posts();

            List<data.Comment> _comments = commentFactory.GetCommentByPostID(posts.PostID);
                List<Comments> _commentView = new List<Comments>();
                foreach (data.Comment item in _comments)
                {
                    _commentView.Add(new Comments
                    {
                        CommentID = item.CommentID.ToString(),
                        CommentAuthor = item.CommentAuthor,
                        CommentContent = item.CommentContent,
                        PostID = item.PostID.ToString()
                    });

                    _postView.PostAuthor = posts.PostAuthor;
                    _postView.PostContent = posts.PostContent;
                    _postView.PostID = posts.PostID.ToString();
                    _postView.comments = _commentView;
            }

            return _postView;
        }

        // POST api/<controller>
        public HttpResponseMessage PostThread(Posts post)
        {
            if (ModelState.IsValid)
            {
                data.PostThread postThread = new data.PostThread();
                postThread.PostDate = DateTime.Now;
                postThread.PostContent = post.PostContent;
                postThread.PostAuthor = post.PostAuthor;                
                postThread = postFactory.SavePost(postThread);

                post.PostID = postThread.PostID.ToString();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, post);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { PostID = post.PostID }));
                    return response;
            }
            else {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

       
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int PostID, data.PostThread post)
        {
            if (ModelState.IsValid && PostID == post.PostID)
            {
                post = postFactory.SavePost(post);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage PutComment(int CommentID, data.Comment comment)
        {
            if (ModelState.IsValid && CommentID == comment.CommentID)
            {
                comment = commentFactory.SaveComment(comment);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }

    public class Comments {
        public string CommentID { get; set; }
        public string PostID { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentContent { get; set; }
    }

    public class Posts {
        public string PostID { get; set; }
        public string PostAuthor { get; set; }
        public string PostContent { get; set; }
        public List<Comments> comments { get; set; }
    }
}