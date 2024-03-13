function handleDropdownChange() {
    var selectedValue = document.getElementById("boss").value;
    var postname = document.getElementById("Name").value;
    var concatinated = selectedValue + "," + postname.value;
    document.getElementById("concatenatedValue").innerText = concatinated;
    document.getElementById("postName").innerHTML = concatinated;
    console.log("concatinated : " + concatinated);
    
    console.log("dropdown : " + selectedValue);
    
    console.log("postname : " + postname);
}
document.getElementById("Name").oninput = handleDropdownChange();
document.getElementById("boss").onchange = handleDropdownChange();