document.addEventListener('DOMContentLoaded', function () {

    function displayResults(results) {
        var resultsList = document.getElementById('results');
        resultsList.innerHTML = '';
        if (results && results.length > 0) {
            results.forEach(function (result) {
                var TimeOut = new Date(result.timeOut);
                //console.log(typeof result.timeOut + " is a type of " + result.timeOut);
                //console.log("----------*");
                //console.log(TimeOut.getTime());
                //console.log("----------*");
                var currentTime = new Date();
                var diff = TimeOut.getTime() - currentTime.getTime();
                /*console.log(diff);*/
                var willRender = true;
                if (diff <= 0) {
                    willRender = false;
                }
                /*console.log(willRender);*/
                var hourLeft = Math.floor(diff / (1000 * 60 * 60));
                diff -= hourLeft * (1000 * 60 * 60);
                var minuteLeft = Math.floor(diff / (1000 * 60));

                var timeString = hourLeft + " Hour " + minuteLeft + " Minute Left"
                /*console.log(timeString);*/
            });
        } else {
            resultsList.innerHTML = '<li>No results found</li>';
        }
    }

    document.getElementById('search').addEventListener('input', function () {
        var query = this.value.trim();
        if (query === '') {
            document.getElementById('results').innerHTML = '';
            return;
        }
        if (query != null) {
            var xhr = new XMLHttpRequest();
            xhr.open('POST', searchUrl);
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            xhr.onload = function () {
                if (xhr.status === 200) {
                    console.log("Before parse" + xhr.responseText);
                    var data = JSON.parse(xhr.responseText);
                    console.log("-------------------");
                    console.log("After parse" + data);
                    displayResults(data);
                }
            };
            xhr.send('query=' + encodeURIComponent(query));
        }
    });
});
