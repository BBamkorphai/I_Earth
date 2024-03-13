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
                const parent = document.getElementsByClassName("grid-container")[0];

                // Create the box item
                const box_item = document.createElement("div");
                box_item.classList.add("box-item");

                // Add onclick event
                box_item.onclick = function () {
                    GoToRoom(Post.PostId);
                };

                // Create and configure elements
                const score = document.createElement("div");
                score.classList.add("score");

                const level = document.createElement("div");
                level.classList.add("level");

                const line = document.createElement("div");
                line.classList.add("line-block");

                const data = document.createElement("div");
                data.classList.add("data");

                const box = document.createElement("div");
                box.classList.add("box");

                const span = document.createElement("span");

                const number = document.createElement("div");
                number.classList.add("number");

                const topElement = document.createElement("b");
                topElement.classList.add("top");
                topElement.setAttribute('style', "--i:0;");
                topElement.innerHTML = Post.PartyList.Count(); // Assuming Post.PartyList.Count() is valid JavaScript code

                const bot = document.createElement("b");
                bot.classList.add("bot");
                bot.setAttribute('style', "--i:1;");
                bot.innerHTML = Post.MaxSize;

                const power = document.createElement("h3");
                power.innerHTML = "Power Level";

                const powervalue = document.createElement("h2");
                powervalue.innerHTML = Post.PowerLevel;

                const name = document.createElement("h2");
                name.classList.add("name");
                name.innerHTML = Post.Name;

                const info = document.createElement("h3");
                info.classList.add("info");
                info.innerHTML = Post.Description;

                const date = document.createElement("small");
                date.classList.add("date");
                date.innerHTML = temp; // Assuming temp is a valid variable containing date information

                const icon = document.createElement("div");
                icon.classList.add("icon");

                const i = document.createElement("i");
                i.classList.add("fa", "fa-plus-circle");

                // Construct the structure
                box.appendChild(span);
                number.appendChild(topElement);
                number.appendChild(bot);
                box.appendChild(number);
                score.appendChild(box);

                level.appendChild(power);
                level.appendChild(powervalue);

                data.appendChild(name);
                data.appendChild(info);
                data.appendChild(date);
                icon.appendChild(i);
                data.appendChild(icon);
                box_item.appendChild(score);
                box_item.appendChild(level);
                box_item.appendChild(line);
                box_item.appendChild(data);

                // Append box_item to parent
                parent.appendChild(box_item);
            });
        } else {
            resultsList.innerHTML = '<li>No results found</li>';
        }
    }

    document.getElementById('search').addEventListener('input', function () {
        var query = this.value.trim();
        if (query === '') {
            document.getElementById('grid-container').innerHTML = '';
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
