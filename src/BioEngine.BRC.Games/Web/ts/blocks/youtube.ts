import $ = require('jquery');

$(function () {
    $('.youtube-video').on('click', function () {
        let url = $(this).data('embed-url');
        let html = `
         <iframe 
         width="560" 
         height="315" 
         src="${url}?autoplay=1" 
         frameborder="0" 
         allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" 
         allowfullscreen>
         </iframe>
        `;
        $(this).html(html).addClass('init');
    });
});
