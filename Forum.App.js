// Require.js allows us to configure shortcut alias
require.config({
    // The shim config allows us to configure dependencies for
    // scripts that do not call define() to register a module
    shim: {
        'underscore': {
            exports: '_'
        },
        'backbone': {
            deps: [
				'underscore',
				'jquery'
            ],
            exports: 'Backbone'
        }
    },        
    paths: {
        jquery : '/Lib/jquery-1.7',
        underscore : '/Lib/underscore-min',
        backbone : '/Lib/backbone-0.92',
        text: '/Lib/text',
        common: '/js/Socialstream/common',
        paging: '/js/Socialstream/paging',
        handlebars : '/Lib/handlebars-1.0.0.beta.6'
    }
});

require([
    'jquery',
    'underscore',
    'backbone',
	'js/Socialstream/ForumViews',
	'js/Socialstream/ForumCollections',
    'js/Socialstream/ForumModels',
    'common',
    'paging'

], function ($,_, Backbone, views, collections,Models,common,paging) {
   
    var PostCollection = new collections.PostCollection();
    var pagingCollectionHelper = new collections.PagingCollectionHelper({ pageIndex: paging.currentIndex.get(), pageSize: paging.pageSize.get() });
    pagingCollectionHelper.fetch().done(function () {
        $.each(pagingCollectionHelper.models, function (ind, model) {
            PostCollection.add(model);
        });

        var post_list_view = new views.PostMasterView({ el: $('#content'), model: PostCollection });
        post_list_view.render();
    });

    var IntializeEvents = new views.PostBodyEvents();
    IntializeEvents.start(PostCollection, paging,common);
   
});
