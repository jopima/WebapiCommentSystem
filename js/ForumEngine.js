/// <reference path="../lib/jquery-1.7.js" />

var ForumEngine = ForumEngine || {};
ForumEngine.CommentURL = '/api/Comment';
ForumEngine.PostURL = '/api/Forum';
ForumEngine.pageIndex = 0;
ForumEngine.pageSize = 2;
ForumEngine.DefaultUser = "Opima Jude";

ForumEngine.common = {
    CommentURL: '/api/Comment',
    PostURL: '/api/Forum',
    pageIndex: 0,
   
    currentPage: 1,
    previousPage: false,
    DefaultUser: 'Opima Jude'
    
};

ForumEngine.paging = {
    pageIndex: {
        pageIndex: 0, get: function ()
        {
            if (!this.Initialized)
            { this.Initialized = true; }
            return this.pageIndex;
        },
        set: function (index) { this.pageIndex = index; },
        Initialized: false
    },
    pageSize: { pageSize: 2, get: function () { return this.pageSize; }, set: function (pagesize) { this.pageSize = pagesize; } },
    currentIndex: { get: function () { return ForumEngine.paging.pageIndex.get(); }, set: function (index) { ForumEngine.paging.pageIndex.set(index); } },    
    nextIndex: {
        get: function () {
            if (!ForumEngine.paging.pageIndex.Initialized) {                
                return ForumEngine.paging.pageIndex.get();
            } else { ForumEngine.paging.pageIndex.set(ForumEngine.paging.currentIndex.get() + 1); return ForumEngine.paging.pageIndex.get(); }
            
        }        
    }
};

(function () {
    'use strict';

 
    // Models //////////////////////////////////////////////////////////////

    $('#new-Post').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode === 13) {
            var post = { PostAuthor: ForumEngine.DefaultUser, PostContent: $("#new-Post").val() };
            var postModel = new ForumEngine.PostModel(post);
            postModel.urlRoot = ForumEngine.PostURL;
            postModel.save({}, {
                success: function (data) {
                    console.log(JSON.stringify(data));
                    PostCollection.add(data);
                    $("#new-Post").val('');
                },
                    error: function () { console.log("Errors Encountered")}
            });
           
        }	
});

    $("#btnShowNext").live('click', function () {
        var pagingCollectionHelper = new ForumEngine.PagingCollectionHelper({ pageIndex: paging.nextIndex.get(), pageSize: paging.pageSize.get() });

        pagingCollectionHelper.fetch().done(function () {
            $.each(pagingCollectionHelper.models, function (ind, model) {
                PostCollection.add(model);
                console.log("index : " + ind + " Value : " + JSON.stringify(model));
            });
        });

    });

    
    ForumEngine.CommentModel = Backbone.Model.extend({
        idAttribute: 'CommentID',
        urlRoot: ForumEngine.CommentURL,
    });

    
    ForumEngine.PostModel = Backbone.Model.extend({
        urlRoot: ForumEngine.PostURL,
        idAttribute: 'PostID',       
        initialize: function () {
          //  console.log(this.urlRoot);
        }
    });

    ForumEngine.PagingModelHelper = Backbone.Model.extend({
        urlRoot: ForumEngine.PostURL,
        idAttribute: 'PostID'       
    });

    ForumEngine.PostCollection = Backbone.Collection.extend({
        url: ForumEngine.PostURL,
        model: ForumEngine.PostModel
    });

    ForumEngine.PagingCollectionHelper = Backbone.Collection.extend({        
        model: ForumEngine.PostModel,
        url: ForumEngine.PostURL,
        initialize: function (init) {
            var pagingURL = "?pageSize=" +init.pageSize + '&pageIndex=' + init.pageIndex;           
            this.url = ForumEngine.PostURL + pagingURL;                      
        }
    });

    // Enable collection pagination 

   

    // Views ///////////////////////////////////////////////////////////////

    // Post list //

    ForumEngine.PostListView = Backbone.View.extend({
        tagName: 'div',
        className: 'thread_list_view',
        initialize: function () {
            _.bindAll(this, 'render', 'render_post_summary', 'on_submit', 'on_post_created', 'on_error', 'render_comments_view');
            this.model.bind('reset', this.render);
            this.model.bind('change', this.render);
            this.model.bind('add', this.render_comments_view);
        },

        template: Handlebars.compile($('#tpl_thread_list').html()),

        render: function () {
            $(this.el).html(this.template());
            this.model.forEach(this.render_comments_view);
            return $(this.el).html();
        },

        render_post_summary: function (post) {            
            var post_summary_view = new ForumEngine.PostSummaryView({ model: post });
            this.$('ul.thread_list').prepend($(post_summary_view.render()));
        },
        render_comments_view: function (post) {
            var post_View = new ForumEngine.PostView({ model: post });            
            this.$("div.post_list").prepend($(post_View.render()));           
            $.each(post.get('comments'), function (index, comment) {
            var commentModel = new ForumEngine.CommentModel(comment);
                post_View.render_comment(commentModel);
               
            });
           
        },
        events: {            
            'keypress input[type=text]': 'on_submit',
        },

        on_submit: function (e) {
            var keycode = (e.keyCode ? e.keyCode : e.which);
            if (keycode === 13) {
                var post = new ForumEngine.PostModel({ PostContent: this.$('.new_thread_title').val() });
                console.log("Atempting to save an empty post");
                console.log(JSON.stringify(post));
                $("#new-Post").val('');
               
            }
           
        },

        on_post_created: function (post, response) {
            this.model.add(post, { at: 0 });
            var comment = new ForumEngine.CommentModel({
                author: this.$('.new_message_author').val(),
                text: this.$('.new_message_text').val(),
                PostThread : post.get('PostID')
            });
           
        },

        on_error: function (model, response) {
            var error = $.parseJSON(response.responseText);
            this.$('.error_message').html(error.message);
        },
    });

    // Thread //

    ForumEngine.PostSummaryView = Backbone.View.extend({
        tagName: 'li',
        className: 'thread_summary_view',
        initialize: function () {
            _.bindAll(this, 'render', 'on_click');
            this.model.bind('change', this.render);
        },

        template: Handlebars.compile($('#tpl_thread_summary').html()),
        render: function () {           
            return $(this.el).html(this.template(this.model.toJSON()));
        },
        events: {
            'click': 'on_click',
        },

        on_click: function (e) {
            $("#content").html("<p>Opima Jude Sucks</p>");           
        },
    });

    ForumEngine.PostView = Backbone.View.extend({
        tagName: 'div',
        className: 'thread_view',
        initialize: function () {
            _.bindAll(this, 'render', 'render_comment', 'on_submit');
            this.model.bind('change', this.render);
            this.model.bind('reset', this.render);
            this.model.bind('add:comments', this.render_comment);
        },

        template: Handlebars.compile($('#tpl_thread').html()),

        render: function () {            
            return $(this.el).html(this.template(this.model.toJSON()));
        },

        render_comment: function (comment) {
            
            var comment_view = new ForumEngine.CommentView({ model: comment });
            this.$('div.message_list').append($(comment_view.render()));
            
        },

        events: {
            'click #btnAddComment': 'on_submit',
        },
        save_comment: function (comment) {
            $.ajax({
                url: ForumEngine.CommentURL,
                data: comment,
                type: "POST",
                contentType: "application/json;charset=utf-8",
            }).then(function (data) {
                console.log("Post Returned Data");
                console.log(data);
            });
        },
        on_submit: function (e) {

            var currentModel = this.model;
            var new_comment = new ForumEngine.CommentModel({
                CommentAuthor: this.$('.new_message_author').val(),
                CommentContent : this.$('.new_message_text').val(),
                PostID : this.model.get("PostID")
            });
            
            new_comment.save({}, {
                success: function (data) {                               
                    currentModel.get("comments").add(data);                    
                },
                error: function () { console.log("Errors Encountered")},
            });
        },
    });

    // Message //

    ForumEngine.CommentView = Backbone.View.extend({
        tagName: 'div',
        className: 'message_view',
        initialize: function () {
          //  _.bindAll(this, 'render');
          //  this.model.bind('change', this.render);
        },

        template: Handlebars.compile($('#tpl_message').html()),

        render: function () {
            return $(this.el).html(this.template(this.model.toJSON()));
        },
    });



    // App /////////////////////////////////////////////////////////////////

    var PostCollection = null;    
    var paging = null;
    ForumEngine.bootstrap = function () {
        this.Start = function () {
            paging = ForumEngine.paging;
            PostCollection = new ForumEngine.PostCollection();
            var pagingCollectionHelper = new ForumEngine.PagingCollectionHelper({ pageIndex: paging.currentIndex.get(), pageSize: paging.pageSize.get() });
            pagingCollectionHelper.fetch().done(function () {
                $.each(pagingCollectionHelper.models, function (ind, model) {
                    PostCollection.add(model);
                });

                    var post_list_view = new ForumEngine.PostListView({ el: $('#content'), model: PostCollection });
                    post_list_view.render();
            });

           
        }
    }


}());