(function(window, undefined) {
    var Bowtie = {};

    function loadScript(url, callback) {
        var script = document.createElement('script'); 

        script.type  = 'text/javascript';
        script.async = true;
        script.src   = url

        var entry = document.getElementsByTagName('script')[0];
        entry.parentNode.insertBefore(script, entry);

        if (script.addEventListener)
            script.addEventListener('load', callback, false);
        else {
            script.attachEvent('onreadystatechange', function() {
                if (/complete|loaded/.test(script.readyState))
                    callback();
            });
        }
    }

    Bowtie.instances = {};
    Bowtie.callbackQueue = {};
    Bowtie.init = function (version, callback) {
        console.log('bowtie.init....loading');
        if (typeof version === 'function') {
            callback = version;
            version = '1.0';
        }
        if (Bowtie.instances[version] !== undefined) {
            callback(Bowtie.instances[version]);
            return;
        } else if (Bowtie.callbackQueue[version] !== undefined) {
            Bowtie.callbackQueue.push(callback);
            return;
        }
        var file;
        switch (version) {
            case '1.0': file = 'lib.1_0.js'; break;
            case '1.1': file = 'lib.1_1.js'; break;
            default:
                throw "Unknown SDK version: " + version;
        }
        Bowtie.callbackQueue[version] = [callback];
        console.log("callbackQueue", Bowtie.callbackQueue);
        file = 'http://bowtie.jbsquared.com/sdk/' + file;
        loadScript(file, function() {
            for (var i = 0; i < Bowtie.callbackQueue; i++) {
                Bowtie.callbackQueue[i](Bowtie.instances[version]);
            }
        });
    };

    var _Bowtie = window.Bowtie;
    window.Bowtie = Bowtie;
    Bowtie.noConflict = function () {
        window.Bowtie = _Bowtie;
        return Bowtie;
    };
})(this);

// Embed the 'script' microlib into our initial script file for loading dependencies.
//{% include 'script.js' %}

//Bowtie.script = $script.noConflict();
