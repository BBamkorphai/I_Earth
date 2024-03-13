
function test() {
    var selectedValue = document.getElementById("boss").value;
    var postname = document.getElementById("Name").value;
    var concatinated = selectedValue + "," + postname;
    document.getElementById("concatenatedValue").innerText = concatinated;
    document.getElementById("postName").value = concatinated;
    console.log("concatinated : " + concatinated);

    console.log("dropdown : " + selectedValue);

    console.log("postname : " + postname);
}

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById("Name").addEventListener('input', test);
    document.getElementById("boss").addEventListener('change', test);

});

