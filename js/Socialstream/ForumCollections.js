define([
	'underscore',
	'backbone',	
	'js/Socialstream/ForumModels',
    'common'
], function (_, Backbone, Models,common) {

   var PostCollection = Backbone.Collection.extend({
        url: common.postModelURL,
        model: Models.PostModel
    });

   var PagingCollectionHelper = Backbone.Collection.extend({
       model: Models.PostModel,
       url: common.postModelURL,
        initialize: function (init) {
            var pagingURL = "?pageSize=" + init.pageSize + '&pageIndex=' + init.pageIndex;
            this.url = common.postModelURL + pagingURL;
        }
    });

    return { PostCollection: PostCollection, PagingCollectionHelper : PagingCollectionHelper };
});
