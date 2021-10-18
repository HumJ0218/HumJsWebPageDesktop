const uri = 'https://live.bilibili.com';
const cover = true;

if (window.location.href == 'about:blank') {
    window.location.href = uri;
} else {
    var video;
    var interval = setInterval(() => {
        var v = document.querySelector('video');
        if (v) {
            video = v;
            clearInterval(interval);
            main();
        }
    });

    function main() {
        try {
            video.muted = true;
            document.body.innerHTML = '';
            document.body.appendChild(video);

            video.style.background = 'black';
            video.style.position = 'absolute';
            video.style.zIndex = 1000000;
            document.body.style.overflow = 'hidden';

            var oldWidth = 0;
            var oldHeight = 0;

            function resize() {
                var windowWidth = window.innerWidth;
                var windowHeight = window.innerHeight;
                var windowRatio = windowWidth / windowHeight;

                if (windowWidth != oldWidth || windowHeight != oldHeight) {
                    oldWidth = windowWidth;
                    oldHeight = windowHeight;

                } else {
                    return;
                }

                var videoWidth = video.videoWidth;
                var videoHeight = video.videoHeight;
                var videoRatio = videoWidth / videoHeight;

                if (cover) {
                    if (windowRatio > videoRatio) {
                        var width = windowWidth;
                        var height = windowWidth / videoRatio;
                        var top = (windowHeight - height) / 2;

                        video.style.left = '0';
                        video.style.top = top + 'px';
                        video.style.width = width + 'px';
                        video.style.height = height + 'px';
                    } else if (windowRatio < videoRatio) {
                        var width = windowHeight * videoRatio;
                        var height = windowHeight;
                        var left = (windowWidth - width) / 2;

                        video.style.left = left + 'px';
                        video.style.top = '0';
                        video.style.width = width + 'px';
                        video.style.height = height + 'px';
                    } else {
                        video.style.left = '0';
                        video.style.top = '0';
                        video.style.width = windowWidth + 'px';
                        video.style.height = windowHeight + 'px';
                    }
                } else {
                    video.style.left = '0';
                    video.style.top = '0';
                    video.style.width = windowWidth + 'px';
                    video.style.height = windowHeight + 'px';
                }
            };

            window.addEventListener('resize', resize);
            resize();
        } catch (ex) {
            alert(ex)
        }
    }
}