$(function () {
    window['twttr'] = (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0],
            t = window['twttr'] || {};
        if (d.getElementById(id)) {
            return t;
        }
        js = d.createElement(s);
        js.id = id;
        js.src = 'https://platform.twitter.com/widgets.js';
        fjs.parentNode.insertBefore(js, fjs);

        t._e = [];
        t.ready = function (f) {
            t._e.push(f);
        };

        return t;
    }(document, 'script', 'twitter-wjs'));

    const tweets = $('.twitter-tweet');

    $(tweets).each(function (t, tweet) {

        const id = $(tweet).data('tweet-id');
        twttr.ready(() => {
            twttr.widgets.createTweet(
                id, tweet,
                {
                    conversation: 'all',    // or all
                    cards: 'visible',  // or visible 
                    linkColor: '#cc0000', // default is blue
                    theme: 'light', // or dark
                    align: 'center'
                }).then(() => {
                $(tweet).addClass('loaded');
            });
        });
    });
});
