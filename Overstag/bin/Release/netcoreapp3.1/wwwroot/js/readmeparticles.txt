<script src="~/js/particles.min.js"></script>
<script>
    particlesJS.load('particles-js', '/js/particlesjs-config.json', function() {
        console.log('callback - particles.js config loaded');
    });
</script>

<style>
    body, html {
      height: 100%
    }

    #particles-js canvas {
        display: block;
    }

    #particles-js {
        width: 100%;
        height: 100%;
        position: fixed;
        z-index: -10;
        top: 0;
        left: 0
    }
</style>

onder div met content:
<div id="particles-js"></div>
