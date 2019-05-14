declare var Twitch: any;

$(function () {
    const twitchEmbeds = $('.twitch-embed');
    $(twitchEmbeds).each(function (t, twitchEmbed) {
        const id = $(twitchEmbed).attr('id');
        const videoId = $(twitchEmbed).data('video-id');
        const collectionId = $(twitchEmbed).data('collection-id');
        const channelId = $(twitchEmbed).data('channel-id');

        const params: any = {
            width: 560,
            height: 315,
            layout: 'video',
            autoplay: false
        };
        params.videoId = videoId;
        params.channel = channelId;
        params.collection = collectionId;
        const player = new Twitch.Player(id, params);
        player.addEventListener(Twitch.Player.READY, () => {
            $(twitchEmbed).addClass('loaded');
        });
    });

});
