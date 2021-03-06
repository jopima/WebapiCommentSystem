﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("ForumDBModel", "Post_Comment_Link", "PostThread", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Forum.Application.data.PostThread), "Comment", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Forum.Application.data.Comment), true)]

#endregion

namespace Forum.Application.data
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ForumDBContext : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ForumDBContext object using the connection string found in the 'ForumDBContext' section of the application configuration file.
        /// </summary>
        public ForumDBContext() : base("name=ForumDBContext", "ForumDBContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ForumDBContext object.
        /// </summary>
        public ForumDBContext(string connectionString) : base(connectionString, "ForumDBContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ForumDBContext object.
        /// </summary>
        public ForumDBContext(EntityConnection connection) : base(connection, "ForumDBContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Comment> Comments
        {
            get
            {
                if ((_Comments == null))
                {
                    _Comments = base.CreateObjectSet<Comment>("Comments");
                }
                return _Comments;
            }
        }
        private ObjectSet<Comment> _Comments;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<PostThread> PostThreads
        {
            get
            {
                if ((_PostThreads == null))
                {
                    _PostThreads = base.CreateObjectSet<PostThread>("PostThreads");
                }
                return _PostThreads;
            }
        }
        private ObjectSet<PostThread> _PostThreads;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Comments EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToComments(Comment comment)
        {
            base.AddObject("Comments", comment);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the PostThreads EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPostThreads(PostThread postThread)
        {
            base.AddObject("PostThreads", postThread);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ForumDBModel", Name="Comment")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Comment : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Comment object.
        /// </summary>
        /// <param name="commentID">Initial value of the CommentID property.</param>
        public static Comment CreateComment(global::System.Int32 commentID)
        {
            Comment comment = new Comment();
            comment.CommentID = commentID;
            return comment;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CommentID
        {
            get
            {
                return _CommentID;
            }
            set
            {
                if (_CommentID != value)
                {
                    OnCommentIDChanging(value);
                    ReportPropertyChanging("CommentID");
                    _CommentID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("CommentID");
                    OnCommentIDChanged();
                }
            }
        }
        private global::System.Int32 _CommentID;
        partial void OnCommentIDChanging(global::System.Int32 value);
        partial void OnCommentIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PostID
        {
            get
            {
                return _PostID;
            }
            set
            {
                OnPostIDChanging(value);
                ReportPropertyChanging("PostID");
                _PostID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PostID");
                OnPostIDChanged();
            }
        }
        private Nullable<global::System.Int32> _PostID;
        partial void OnPostIDChanging(Nullable<global::System.Int32> value);
        partial void OnPostIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CommentAuthor
        {
            get
            {
                return _CommentAuthor;
            }
            set
            {
                OnCommentAuthorChanging(value);
                ReportPropertyChanging("CommentAuthor");
                _CommentAuthor = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CommentAuthor");
                OnCommentAuthorChanged();
            }
        }
        private global::System.String _CommentAuthor;
        partial void OnCommentAuthorChanging(global::System.String value);
        partial void OnCommentAuthorChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CommentContent
        {
            get
            {
                return _CommentContent;
            }
            set
            {
                OnCommentContentChanging(value);
                ReportPropertyChanging("CommentContent");
                _CommentContent = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CommentContent");
                OnCommentContentChanged();
            }
        }
        private global::System.String _CommentContent;
        partial void OnCommentContentChanging(global::System.String value);
        partial void OnCommentContentChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> CommentDate
        {
            get
            {
                return _CommentDate;
            }
            set
            {
                OnCommentDateChanging(value);
                ReportPropertyChanging("CommentDate");
                _CommentDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CommentDate");
                OnCommentDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _CommentDate;
        partial void OnCommentDateChanging(Nullable<global::System.DateTime> value);
        partial void OnCommentDateChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ForumDBModel", "Post_Comment_Link", "PostThread")]
        public PostThread PostThread
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<PostThread>("ForumDBModel.Post_Comment_Link", "PostThread").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<PostThread>("ForumDBModel.Post_Comment_Link", "PostThread").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<PostThread> PostThreadReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<PostThread>("ForumDBModel.Post_Comment_Link", "PostThread");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<PostThread>("ForumDBModel.Post_Comment_Link", "PostThread", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ForumDBModel", Name="PostThread")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class PostThread : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new PostThread object.
        /// </summary>
        /// <param name="postID">Initial value of the PostID property.</param>
        public static PostThread CreatePostThread(global::System.Int32 postID)
        {
            PostThread postThread = new PostThread();
            postThread.PostID = postID;
            return postThread;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 PostID
        {
            get
            {
                return _PostID;
            }
            set
            {
                if (_PostID != value)
                {
                    OnPostIDChanging(value);
                    ReportPropertyChanging("PostID");
                    _PostID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("PostID");
                    OnPostIDChanged();
                }
            }
        }
        private global::System.Int32 _PostID;
        partial void OnPostIDChanging(global::System.Int32 value);
        partial void OnPostIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PostAuthor
        {
            get
            {
                return _PostAuthor;
            }
            set
            {
                OnPostAuthorChanging(value);
                ReportPropertyChanging("PostAuthor");
                _PostAuthor = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PostAuthor");
                OnPostAuthorChanged();
            }
        }
        private global::System.String _PostAuthor;
        partial void OnPostAuthorChanging(global::System.String value);
        partial void OnPostAuthorChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PostContent
        {
            get
            {
                return _PostContent;
            }
            set
            {
                OnPostContentChanging(value);
                ReportPropertyChanging("PostContent");
                _PostContent = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PostContent");
                OnPostContentChanged();
            }
        }
        private global::System.String _PostContent;
        partial void OnPostContentChanging(global::System.String value);
        partial void OnPostContentChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> PostDate
        {
            get
            {
                return _PostDate;
            }
            set
            {
                OnPostDateChanging(value);
                ReportPropertyChanging("PostDate");
                _PostDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PostDate");
                OnPostDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _PostDate;
        partial void OnPostDateChanging(Nullable<global::System.DateTime> value);
        partial void OnPostDateChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ForumDBModel", "Post_Comment_Link", "Comment")]
        public EntityCollection<Comment> Comments
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Comment>("ForumDBModel.Post_Comment_Link", "Comment");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Comment>("ForumDBModel.Post_Comment_Link", "Comment", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
