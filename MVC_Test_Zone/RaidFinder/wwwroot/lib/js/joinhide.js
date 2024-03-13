var joinbutton = document.getElementById("join");
for (var i = 0; i < Model.Post.Partylist.length; i++) {
	var user = Model.Post.Partylist[i];
	if (user.UserId == userid) {
		joinButton.style.display = "none";
		console.log("here display none");
		break;
	}
}