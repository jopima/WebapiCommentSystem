
define([
	'underscore',	
    'backbone',
    'common'
], function (_, Backbone,common) {


    CommentModel = Backbone.Model.extend({
        idAttribute: 'CommentID',
        urlRoot: common.comentModelURL,
    });


    PostModel = Backbone.Model.extend({
        urlRoot: common.postModelURL,
        idAttribute: 'PostID',        
        initialize: function () {            
        }
    });

    return { CommentModel: CommentModel, PostModel: PostModel };
});

