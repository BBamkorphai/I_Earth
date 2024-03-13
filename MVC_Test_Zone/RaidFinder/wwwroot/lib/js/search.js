document.addEventListener('DOMContentLoaded', function () {

    function displayResults(results) {
        const parent = document.getElementsByClassName("grid-container")[0];
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

                // Create the box item
                const box_item = document.createElement("div");
                box_item.classList.add("box-item");

                //if (name == "Boss1") { return System.IO.File.ReadAllBytes(@"wwwroot/image/DEILJHO.jpg"); }
                //else if (name == "Boss2") { return System.IO.File.ReadAllBytes(@"wwwroot/image/KULVE_TAROTH.jpg"); }
                //else if (name == "Boss3") { return System.IO.File.ReadAllBytes(@"wwwroot/image/KUSHALA_DAORA.jpg"); }
                //else if (name == "Boss4") { return System.IO.File.ReadAllBytes(@"wwwroot/image/LUNASTRA.jpg"); }
                //else if (name == "Boss5") { return System.IO.File.ReadAllBytes(@"wwwroot/image/NERGIGANTE.jpg"); }
                //return System.IO.File.ReadAllBytes(@"wwwroot/image/DEILJHO.jpg");
                var imageLink = "";
                if (result.name.split(',')[0] == "boss1") {
                    imageLink = "image/DEILJHO.jpg"
                } else if (result.name.split(',')[0] == "boss2") {
                    imageLink = "image/KULVE_TAROTH.jpg"
                } else if (result.name.split(',')[0] == "boss3") {
                    imageLink = "image/KUSHALA_DAORA.jpg"
                } else if (result.name.split(',')[0] == "boss4") {
                    imageLink = "image/LUNASTRA.jpg"
                } else if (result.name.split(',')[0] == "boss5") {
                    imageLink = "image/NERGIGANTE.jpg"
                }
                // Add onclick event
                box_item.onclick = function () {
                    GoToRoom(result.postId);
                };

                // Create and configure elements
                const img_box = document.createElement("div");
                img_box.classList.add("img-box");

                const score = document.createElement("div");
                score.classList.add("score");

                const level = document.createElement("div");
                level.classList.add("level");

                const line = document.createElement("div");
                line.classList.add("line-block");

                const data = document.createElement("div");
                data.classList.add("data");

                var currentPageUrl = window.location.href;
                var fullImageUrl = new URL(imageLink, currentPageUrl).href;

                const img = document.createElement("img");
                img.src = fullImageUrl;

                const box = document.createElement("div");
                box.classList.add("box");

                const span = document.createElement("span");

                const number = document.createElement("div");
                number.classList.add("number");

                const topElement = document.createElement("b");
                topElement.classList.add("top");
                topElement.setAttribute('style', "--i:0;");
                topElement.innerHTML = result.partyList.length; // Assuming Post.PartyList.Count() is valid JavaScript code

                const bot = document.createElement("b");
                bot.classList.add("bot");
                bot.setAttribute('style', "--i:1;");
                bot.innerHTML = result.maxSize;

                const power = document.createElement("h3");
                power.innerHTML = "Power Level";

                const powervalue = document.createElement("h2");
                powervalue.innerHTML = result.powerLevel;

                const name = document.createElement("h2");
                name.classList.add("name");
                name.innerHTML = result.name;

                const info = document.createElement("h3");
                info.classList.add("info");
                info.innerHTML = result.description;

                const date = document.createElement("small");
                date.classList.add("date");
                date.innerHTML = timeString; // Assuming temp is a valid variable containing date information

                const icon = document.createElement("div");
                icon.classList.add("icon");

                const i = document.createElement("i");
                i.classList.add("fa", "fa-plus-circle");

                // Construct the structure
                img_box.appendChild(img);
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

                box_item.appendChild(img_box);
                box_item.appendChild(score);
                box_item.appendChild(level);
                box_item.appendChild(line);
                box_item.appendChild(data);

                // Append box_item to parent
                if (willRender == True) {
                    parent.appendChild(box_item);
                }
                
            });
        } else {
            parent.innerHTML = '<li>No results found</li>';
        }
    }

    document.getElementById('search').addEventListener('input', function () {
        var query = this.value;
        //if (query === '') {
        //    var xhr = new XMLHttpRequest();
        //    xhr.open('POST', searchUrl);
        //    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        //    xhr.onload = function () {
        //        if (xhr.status === 200) {
        //            console.log("Before parse" + xhr.responseText);
        //            var data = JSON.parse(xhr.responseText);
        //            console.log("-------------------");
        //            console.log("After parse" + data);
        //            displayResults(data);
        //        }
        //    };
        //}
        /*        if (query != null) {*/
        const parent = document.getElementsByClassName("grid-container")[0];
        parent.innerHTML = "";
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
        /*        }*/
    });
});