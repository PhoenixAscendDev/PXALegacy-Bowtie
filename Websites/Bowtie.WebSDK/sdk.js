(function(window, undefined) {
    var Bowtie = {};

    if (window.Bowtie)
        return;

    Bowtie.init = function(callback) {
        Bowtie.script("{{ service_url_for('example', chapter='08', name='web-service-api', file='lib.js') }}", function () {
            Bowtie._initializeLibrary(callback);
        });
    };

    window.Bowtie = Bowtie;
})(this);

// Embed the 'script' microlib into our initial script file for loading dependencies.

{% include 'script.js' %}

Bowtie.script = $script.noConflict();
