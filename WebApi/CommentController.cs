using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Forum.Application.WebApi
{
    public class CommentController : ApiController
    {
        data.CommentRepository commentFactory = new data.CommentRepository();

        public IEnumerable<data.Comment> GetComments(int PostID) {

            return commentFactory.GetCommentByPostID(PostID);
        }
        // GET api/<controller>/5
        public data.Comment Get(int CommentID)
        {
            return commentFactory.GetCommentByID(CommentID);
        }

        // POST api/<controller>
        public HttpResponseMessage PostComment(data.Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentDate = DateTime.Now;
                comment = commentFactory.SaveComment(comment);
                Comments commentView = new Comments();
                commentView.CommentID = comment.CommentID.ToString();
                commentView.CommentContent = comment.CommentContent;
                commentView.CommentAuthor = comment.CommentAuthor;
                commentView.PostID = comment.PostID.ToString();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, commentView);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { CommentID = comment.CommentID }));
                return response;
            }
            else {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
             //   return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage PutComment(int CommentID, data.Comment comment)
        {
            if (ModelState.IsValid && CommentID == comment.CommentID)
            {
                comment = commentFactory.SaveComment(comment);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}