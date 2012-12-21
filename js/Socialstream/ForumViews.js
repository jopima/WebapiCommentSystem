/// <reference path="../../lib/jquery-1.7.js" />

define([
	'jquery',
	'underscore',
	'backbone',
	'text!/js/Socialstream/tmpl/comment.tmpl.htm',
    'text!/js/Socialstream/tmpl/postedThread.tmpl.htm',
    'text!/js/Socialstream/tmpl/PostMaster.tmpl.htm',
    'text!/js/Socialstream/tmpl/postSummary.tmpl.htm',
    'js/Socialstream/ForumModels',
    'js/Socialstream/ForumCollections',
    'common',
    'handlebars'
], function ($, _, Backbone, commentTmpl, postTmpl, postMasterTmpl, postBriefTmpl, Models, collections, common, Handlebars) {

    _.templateSettings.escape = _.templateSettings = {interpolate: /\{\{([\s\S]+?)\}\}/g}; //common.templateSettings;


   var PostMasterView = Backbone.View.extend({
        tagName: 'div',
        className: 'thread_list_view',
        initialize: function () {
            _.bindAll(this, 'render', 'render_post_summary', 'on_submit', 'on_post_created', 'on_error', 'render_comments_view');
            this.model.bind('reset', this.render);
            this.model.bind('change', this.render);
            this.model.bind('add', this.render_comments_view);
        },

        template: _.template(postMasterTmpl),

        render: function () {
            $(this.el).html(this.template());
            this.model.forEach(this.render_comments_view);
            return $(this.el).html();
        },

        render_post_summary: function (post) {
            var post_summary_view = new PostSummaryView({ model: post });
            this.$('ul.thread_list').prepend($(post_summary_view.render()));
        },
        render_comments_view: function (post) {
            var post_View = new PostView({ model: post });
            this.$("div.post_list").prepend($(post_View.render()));
            $.each(post.get('comments'), function (index, comment) {
                var commentModel = new Models.CommentModel(comment);
                post_View.render_comment(commentModel);
            });
        },
        events: {
            'keypress input[type=text]': 'on_submit',
        },

        on_submit: function (e) {
            var keycode = (e.keyCode ? e.keyCode : e.which);
            if (keycode === 13) {
              
            }

        },

        on_post_created: function (post, response) {
            this.model.add(post, { at: 0 });                   
        },

        on_error: function (model, response) {
            var error = $.parseJSON(response.responseText);
            this.$('.error_message').html(error.message);
        },
    });

    

   var PostSummaryView = Backbone.View.extend({
        tagName: 'li',
        className: 'thread_summary_view',
        initialize: function () {
            _.bindAll(this, 'render', 'on_click');
            this.model.bind('change', this.render);
        },

        template: _.template(postBriefTmpl),
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

  var  PostView = Backbone.View.extend({
        tagName: 'div',
        className: 'thread_view',
        initialize: function () {
            _.bindAll(this, 'render', 'render_comment', 'on_submit');
            this.model.bind('change', this.render);
            this.model.bind('reset', this.render);
            this.model.bind('change:comments', this.render_comment);
        },

        template: _.template(postTmpl),

        render: function () {
            return $(this.el).html(this.template(this.model.toJSON()));
        },

        render_comment: function (comment) {
            var comment_view = new CommentView({ model: comment });
            this.$('div.message_list').append($(comment_view.render()));

        },

        events: {
            'click #btnAddComment': 'on_submit',
        },
        save_comment: function (comment) {
           
        },
        on_submit: function (e) {
            var self = this;
            var currentModel = this.model;
            var new_comment = new Models.CommentModel({
                CommentAuthor: this.$('.new_message_author').val(),
                CommentContent: this.$('.new_message_text').val(),
                PostID: this.model.get("PostID")
            });

            new_comment.save({}, {
                success: function (data) {
                    var currentCommentList = currentModel.get('comments');
                    currentCommentList.push(data);
                    currentModel.set({ comments: currentCommentList });
                    self.render_comment(data);
                                      
                },
                error: function () { console.log("Errors Encountered") },
            });
        },
    });

   var CommentView = Backbone.View.extend({
        tagName: 'div',
        className: 'message_view',
        initialize: function () {
            _.bindAll(this, 'render');
            this.model.bind('change', this.render);
        },

        template: _.template(commentTmpl),

        render: function () {
            return $(this.el).html(this.template(this.model.toJSON()));
        },
    });

    var PostBodyEvents = function () {

        this.start = function (PostCollection, paging,common) {

            $('#new-Post').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode === 13) {
                    var post = { PostAuthor: common.DefaultUser, PostContent: $("#new-Post").val()};
                    var postModel = new Models.PostModel(post);
                    console.log(postModel.toJSON())
                    postModel.urlRoot = common.postModelURL;
                    postModel.save({}, {
                        success: function (data) {                                                                                   
                            postModel = new Models.PostModel(data);
                            postModel.set({ comments: []});
                            console.log(postModel.toJSON());
                            PostCollection.add(postModel);
                        },
                        error: function () { console.log("Errors Encountered") }
                    });

                }
            });

            $("#btnShowNext").live('click', function () {
                var pagingCollectionHelper = new collections.PagingCollectionHelper({ pageIndex: paging.nextIndex.get(), pageSize: paging.pageSize.get() });

                pagingCollectionHelper.fetch().done(function () {

                    console.log(pagingCollectionHelper.length)
                    if (pagingCollectionHelper.length > 0) {
                        $("#btnShowNext").css({ display: 'block' });
                        $.each(pagingCollectionHelper.models, function (ind, model) {
                            PostCollection.add(model);
                            console.log("index : " + ind + " Value : " + JSON.stringify(model));
                        });
                    } else {
                        $("#btnShowNext").css({display:'none'});
                    }
                });
            });

        }

    }    

    return { CommentView: CommentView, PostView: PostView, PostSummaryView: PostSummaryView, PostMasterView: PostMasterView, PostBodyEvents: PostBodyEvents };
});
