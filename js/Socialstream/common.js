define([], function () {

    var common = {
        comentModelURL: '/api/Comment',
        postModelURL: '/api/Forum',
        templateSettings: '/\{\{-(.*?)\}\}/g',
        ENTER_KEY: 13,        
        DefaultUser: 'Opima Jude'
    };
       
    return common;
});
