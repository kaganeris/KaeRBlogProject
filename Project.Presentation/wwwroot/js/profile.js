window.onload = function () {
    getProfilePosts()
}


function getProfilePosts() {
    $.ajax({
        url: "/Post/ProfilePosts",
        type: "GET",
        success: function (response) {
            $("#profile-post").html(response);
        }
    })
}

function BeAuthor(appUserId) {
    let profileData = {
        appUserId: appUserId
    }
    $.ajax({
        url: "/Profile/BeAuthor",
        type: "POST",
        data: profileData,
        success: function () {
            window.location.reload()
        }
    })
}